﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;

    public GameObject impactEffect;

    //
    public float BaseDamage;




    public void Seek (Transform _target)
    {
        target = _target;
    }


    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);


    }

    void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);


        Destroy(effectIns, 2f);
        
        Destroy(gameObject);

    }



    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "Ally":
                {

                    if (BaseDamage <= 0)
                        return;

                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);

                    }
                    Destroy(gameObject);

                    break;
                }
            case "Enemy":
                {

                    if (BaseDamage >= 0)
                        return;
                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);

                    }
                    Destroy(gameObject);

                    break;
                }


            case "RedTeam":
                {

                    if (BaseDamage >= 0)
                        return;
                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);

                    }
                    Destroy(gameObject);

                    break;
                }

            case "BlueTeam":
                {

                    if (BaseDamage >= 0)
                        return;
                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);

                    }
                    Destroy(gameObject);

                    break;
                }




            default:
                {
                    Destroy(gameObject);
                    break;
                }
        }
    }




}
