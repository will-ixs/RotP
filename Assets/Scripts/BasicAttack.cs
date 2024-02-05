using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public float distanceFromPlayer;

    [SerializeField] private Transform _camera_transform;
    [SerializeField] private Transform _player_transform;
    [SerializeField] private Transform _weapon_transform;
    [SerializeField] private SpriteRenderer _sr;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(_camera_transform.position);
        mousePos.x -= cameraPos.x;
        mousePos.y -= cameraPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x);


        Vector3 weaponPos = _player_transform.position;
        weaponPos.x += distanceFromPlayer * Mathf.Cos(angle);
        weaponPos.y += distanceFromPlayer * Mathf.Sin(angle);
        _weapon_transform.position = weaponPos;
        _weapon_transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle * Mathf.Rad2Deg));
    }
}
