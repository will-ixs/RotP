using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private KeyCode keyCode;
    [SerializeField] private float cooldown;

    [SerializeField] private int damage;
    [SerializeField] private int cost;

    [SerializeField] private float knockbackImpulse;

    [SerializeField] private float normalStaggerTime;
    [SerializeField] private float bossStaggerTime;

    [SerializeField] private GameObject hitbox;

    private Vector3 _attack_direction;

    private Transform _camera_transform;

    private Transform _hitbox_transform;
    private ContactList _hitbox_contact_list;

    [SerializeField] private Timer _cooldown_timer;

    private float _base_hitbox_distance;
    private float _base_hitbox_rotation;

    // Start is called before the first frame update
    void Start()
    {
        _camera_transform = GameObject.FindObjectOfType<Camera>().GetComponent<Transform>();

        _hitbox_transform = hitbox.GetComponent<Transform>();
        _hitbox_contact_list = hitbox.GetComponent<ContactList>();

        if (_cooldown_timer == null)
        {
            _cooldown_timer = gameObject.AddComponent<Timer>();
        }

        _base_hitbox_distance = (_hitbox_transform.position - transform.position).magnitude;
        _base_hitbox_rotation = _hitbox_transform.rotation.z;

        updateHitbox();
    }

    private void updateHitbox()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(_camera_transform.position);
        _attack_direction = (mousePos - cameraPos).normalized;
        float hitbox_angle = Mathf.Atan2(_attack_direction.y, _attack_direction.x);

        Vector3 hitbox_position = this.transform.position;
        hitbox_position.x += _base_hitbox_distance * Mathf.Cos(hitbox_angle);
        hitbox_position.y += _base_hitbox_distance * Mathf.Sin(hitbox_angle);

        float hitbox_rotation = _base_hitbox_rotation + hitbox_angle * Mathf.Rad2Deg + 90.0f;

        _hitbox_transform.position = hitbox_position;
        _hitbox_transform.rotation = Quaternion.Euler(0, 0, hitbox_rotation);

        if (cooldown - _cooldown_timer.getTimeRemaining() < 0.25f)
        {
            hitbox.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        else if (!_cooldown_timer.isReady())
        {
            hitbox.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        }
        else
        {
            hitbox.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
    }

    private void performAttack()
    {
        PlayerHealth health = GetComponent<PlayerHealth>();
        if (health != null)
        {
            if (health.curHealth < cost)
            {
                return;
            }
            else
            {
                health.TakeDamage(cost);
            }
        }
        for (int i = _hitbox_contact_list.contactList.Count - 1; i >= 0; i--)
        {
            GameObject target = _hitbox_contact_list.contactList[i];
            MovementController movementController = target.GetComponent<MovementController>();
            bool dead;
            if (target.CompareTag("Boss"))
            {
                BossHealth hp = target.GetComponent<BossHealth>();
                hp.TakeDamage(damage);
                movementController.knockback(_attack_direction, knockbackImpulse);
                movementController.disableMovement(bossStaggerTime);
                dead = hp.Dead;
            } else
            {
                Enemy hp = target.GetComponent<Enemy>();
                hp.TakeDamage(damage);
                movementController.knockback(_attack_direction, knockbackImpulse);
                movementController.disableMovement(normalStaggerTime);
                dead = hp.Dead;
            }
            if (dead)
            {
                _hitbox_contact_list.contactList.Remove(target);
            }
        }
        _cooldown_timer.begin(cooldown);
    }

    // Update is called once per frame
    void Update()
    {
        updateHitbox();
        if (Input.GetKeyDown(keyCode) && _cooldown_timer.isReady())
        {
            performAttack();
        }
    }
}
