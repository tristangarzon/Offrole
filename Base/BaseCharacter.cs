using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class BaseCharacter : MonoBehaviour, IDamageable
{
    public string CharacterName;
    [SerializeField] BaseStat health;
    public BaseStat Resource;
    public BaseStat Level;
    public Text NameText;

    //Temp
    public List<BaseStat> Stats = new List<BaseStat>();
    public float CurrentSpeed = 10;
    public float ChangedSpeed = 1;
    public bool IsRooted;
    public bool IsStunned;

 

    
    public BaseStat Health
    {
        get
        {
            return health;
        }
    }


     public void LevelUp()
    {
        Level.Curr++;
        Debug.Log(string.Format("{0} has leveled up! ({1})", CharacterName, Level.Curr));
    }


    public void UpdateStats()
    {
        Health.UpdateStat();
        Level.UpdateStat();
        Resource.UpdateStat();

        UpdateRegen();

        if(NameText != null)
        NameText.text = CharacterName;
    }

     public void UpdateRegen()
    {
        if(Health.Curr < Health.Max)
        {
            Health.Curr += (Stats[GameConsts.STAT_HEALTH_REGEN].Curr /5) * Time.deltaTime;
            //Stats[GameConsts.STAT_HEALTH_REGEN].statTextUI.enabled = true;
           // Stats[GameConsts.STAT_HEALTH_REGEN].statText.enabled = true;
            //change back 2 StatTextUI if it doesnt work

        }
        //else
        //{
        //    Stats[GameConsts.STAT_HEALTH_REGEN].statTextUI.enabled = false;
        //}


        if (Resource.Curr < Resource.Max)
        {
            Resource.Curr += (Stats[GameConsts.STAT_RESOURCE_REGEN].Curr / 5) * Time.deltaTime;
            //Stats[GameConsts.STAT_RESOURCE_REGEN].statTextUI.enabled = true;
            //change back 2 StatTextUI if it doesnt work

        }
        //else
        //{
        //    Stats[GameConsts.STAT_RESOURCE_REGEN].statTextUI.enabled = false;
        //}
    }



    ////temp

    //public float CurrentAttack = 0;

    //public void UpdateAutoAttack()
    //{
    //    CurrentAttack += Time.deltaTime;

    //    if (CurrentAttack >= GameFuncs.GetAttackSpeed(Stats[GameConsts.STAT_ATTACK_SPEED].Curr))
    //    {
    //        //ATTACK
    //    }
    //}


    //Temp
    void IDamageable.RecieveStatusEffect(GameConsts.STATUS_EFFECT effect, float amount)
    {
        switch (effect)
        {
            case GameConsts.STATUS_EFFECT.NONE:
                {
                    throw new System.NotImplementedException();

                }
            case GameConsts.STATUS_EFFECT.ROOT:
                {
                    IsRooted = true;
                    break;
                }
            case GameConsts.STATUS_EFFECT.STUN:
                {
                    IsStunned = true;
                    break;
                }
            case GameConsts.STATUS_EFFECT.SPEED:
                {
                    ChangedSpeed = amount;
                    break;
                }
            default:
                {
                    throw new System.NotImplementedException();

                }
        }
    }

    //Temp
    void IDamageable.EndStatusEffect(GameConsts.STATUS_EFFECT effect)
    {
        switch (effect)
        {
            case GameConsts.STATUS_EFFECT.NONE:
                {
                    throw new System.NotImplementedException();

                }
            case GameConsts.STATUS_EFFECT.ROOT:
                {
                    IsRooted = false;
                    break;
                }
            case GameConsts.STATUS_EFFECT.STUN:
                {
                    IsStunned = false;
                    break;
                }
            case GameConsts.STATUS_EFFECT.SPEED:
                {
                    ChangedSpeed = 1;
                    break;
                }
            default:
                {
                    throw new System.NotImplementedException();

                }
        }

    }




    void IDamageable.ChangeHealth(float amount, GameConsts.ATTACK_TYPES attackType)
    {
        //temp
        float dmg = amount;


        if(dmg < 0)
        {
            dmg = Mathf.Abs(dmg);

            dmg = Mathf.Clamp(dmg, 0, Health.Max);

            Health.Curr -= dmg;
            return;

        }

        Health.Curr += dmg;

        //Main thing
       // Debug.Log(string.Format("{0} has taken {1} damage", CharacterName, amount));
        //Health.Curr += amount;
    }

}
