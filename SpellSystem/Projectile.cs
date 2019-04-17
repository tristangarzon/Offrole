using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Projectile : MonoBehaviour
{
    public Spell Ability;
    public Vector3 StartPos;
    public Vector3 CurrPos;
    public float Speed;
    public Rigidbody RBody;
    public string OwnerTag;

    private void Start()
    {
        RBody = GetComponent<Rigidbody>();
        StartPos = transform.position;
    }

    private void Update()
    {
        RBody.velocity = transform.forward * Speed;
        CurrPos = transform.position;
        float dist = Vector3.Distance(StartPos, CurrPos);



        if(Ability.Effect == GameConsts.EFFECT.PROJECTILE)
        {
            if (dist >= Ability.Range.y + 10)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(dist >= Ability.Range.x + 10)
            {
                Destroy(gameObject);
            }
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
                        if (Ability.StatusEffect.HasStatus)
                            Ability.StatusEffect.OnStart(other.gameObject.GetComponent<BaseCharacter>());

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
                        if (Ability.StatusEffect.HasStatus)
                            Ability.StatusEffect.OnStart(other.gameObject.GetComponent<BaseCharacter>());

                    }
                    Destroy(gameObject);

                    break;
                }


            case "RedTeam":
                {

                    if (Ability.BaseDamage >= 0)
                        return;
                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(Ability.BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);
                        if (Ability.StatusEffect.HasStatus)
                            Ability.StatusEffect.OnStart(other.gameObject.GetComponent<BaseCharacter>());

                    }
                    Destroy(gameObject);

                    break;
                }

            case "BlueTeam":
                {

                    if (Ability.BaseDamage >= 0)
                        return;
                    Component comp = other.gameObject.GetComponent(typeof(IDamageable));

                    if (comp)
                    {
                        (comp as IDamageable).ChangeHealth(Ability.BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);
                        if (Ability.StatusEffect.HasStatus)
                            Ability.StatusEffect.OnStart(other.gameObject.GetComponent<BaseCharacter>());
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
