using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public float distanceFromPlayer;

    public int basicDamage;
    public float basicAttackKnockbackSpeed;
    public float basicAttackStaggerTime;
    public float basicAttackCooldown;
    private float timeUntilBasicAttackAvailable;
    [SerializeField] ContactList basicAttack;

    public int lungeDamage;
    public float lungeCooldown;
    private float timeUntilLungeAvailable;
    [SerializeField] ContactList lungeAttack;

    [SerializeField] private Transform _camera_transform;
    [SerializeField] private Transform _weapon_transform;

    [SerializeField] private SpriteRenderer _weapon_sprite;

    private void Start()
    {
        timeUntilBasicAttackAvailable = 0.0f;
        timeUntilLungeAvailable = 0.0f;
    }
    void Update()
    {
        ReduceWeaponCooldown();
        SetWeaponAngleAndPosition();
        CheckHitInputs();
    }

    private void CheckHitInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Basic Attack / Spear
        {
            if (timeUntilBasicAttackAvailable <= 0.0f)
            {
                timeUntilBasicAttackAvailable = basicAttackCooldown;
                for(int i = 0; i < basicAttack.contactList.Count; i++)
                {
                    if (basicAttack.contactList[i].CompareTag("Boss"))
                    {
                        BossHealth hp = basicAttack.contactList[i].GetComponent<BossHealth>();
                        hp.TakeDamage(basicDamage);
                        if (hp.Dead)
                        {
                            basicAttack.contactList.Remove(basicAttack.contactList[i]);
                        }

                    }
                    else //gameobject should have enemy tag & component
                    {
                        GameObject e = basicAttack.contactList[i];
                        Enemy hp = e.GetComponent<Enemy>();
                        e.GetComponent<EnemyAI>().Knockback(basicAttackKnockbackSpeed, basicAttackStaggerTime);
                        hp.TakeDamage(basicDamage);
                        if(hp.Dead)
                        {
                            basicAttack.contactList.Remove(basicAttack.contactList[i]);
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) //Lunge temp
        {
            if(lungeCooldown <= 0.0f)
            {
                timeUntilLungeAvailable = lungeCooldown;
                foreach(GameObject enemy in lungeAttack.contactList)
                {

                }
            }
        }
    }

    private void SetWeaponAngleAndPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(_camera_transform.position);
        mousePos.x -= cameraPos.x;
        mousePos.y -= cameraPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x);


        Vector3 weaponPos = transform.position;
        weaponPos.x += distanceFromPlayer * Mathf.Cos(angle);
        weaponPos.y += distanceFromPlayer * Mathf.Sin(angle);
        _weapon_transform.position = weaponPos;
        _weapon_transform.rotation = Quaternion.Euler(0, 0, 90.0f + angle * Mathf.Rad2Deg);

        if (basicAttackCooldown - timeUntilBasicAttackAvailable < 0.25f)
        {
            _weapon_sprite.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        } else if (timeUntilBasicAttackAvailable > 0.0f)
        {
            _weapon_sprite.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        } else
        {
            _weapon_sprite.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        }
    }

    private void ReduceWeaponCooldown()
    {
        timeUntilBasicAttackAvailable -= Time.deltaTime;
        timeUntilLungeAvailable -= Time.deltaTime;
    }
}
