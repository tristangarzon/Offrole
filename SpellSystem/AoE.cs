using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class AoE : MonoBehaviour
{
    public Spell Ability;
    public Vector3 StartPos;
    public Vector3 CurrPos;
    public float Speed;
    public Rigidbody RBody;
    public string OwnerTag;
    public float CurrTime = 0;





    private void Start()
    {
        RBody = GetComponent<Rigidbody>();
        StartPos = transform.position;
    }

    private void Update()
    {

       

            CurrTime += Time.deltaTime;
            if (CurrTime >= Ability.SpellTime)
            {
                Destroy(gameObject);
            }

    }





    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "Ally":
                {

                    if (Ability.BaseDamage <= 0)
                        return;

                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(Ability.BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);

                    }
                    Destroy(gameObject);

                    break;
                }
            case "Enemy":
                {

                    if (Ability.BaseDamage >= 0)
                        return;
                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(Ability.BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);

                    }
                    Destroy(gameObject);

                    break;
                }
            case "RedTeam":
                {

                    if (Ability.BaseDamage <= 0)
                        return;
                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(Ability.BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);

                    }
                    Destroy(gameObject);

                    break;
                }
            case "BlueTeam":
                {

                    if (Ability.BaseDamage <= 0)
                        return;
                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(Ability.BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);

                    }
                    Destroy(gameObject);

                    break;
                }


            case "Ground":
                {
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
