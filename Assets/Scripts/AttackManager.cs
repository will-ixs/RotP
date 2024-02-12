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
    private float basicCooldown;
    [SerializeField] ContactList basicAttack;

    public int lungeDamage;
    private float lungeCooldown;
    [SerializeField] ContactList lungeAttack;

    [SerializeField] private Transform _camera_transform;
    [SerializeField] private Transform _weapon_transform;

    private void Start()
    {
        lungeCooldown = 0.0f;
        basicCooldown = 0.0f;
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
            if (basicCooldown <= 0.0f)
            {
                basicCooldown = 1.0f;
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
                        Enemy hp = basicAttack.contactList[i].GetComponent<Enemy>();
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
                lungeCooldown = 5.0f;
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
        _weapon_transform.rotation = Quaternion.Euler(0, 0, 270.0f + angle * Mathf.Rad2Deg);
    }

    private void ReduceWeaponCooldown()
    {
        basicCooldown -= Time.deltaTime;
        lungeCooldown -= Time.deltaTime;
    }
}
