using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class BaseBuilding : MonoBehaviour, IDamageable
{
    public string BuildingName;
    [SerializeField] BaseStat health;
    public Text NameText;

    //temp 
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


    public void UpdateStats()
    {
        Health.UpdateStat();
        if (NameText != null)
            NameText.text = BuildingName;
    }


    void IDamageable.ChangeHealth(float amount, GameConsts.ATTACK_TYPES attackType)
    {
        Debug.Log(string.Format("{0} has taken {1} damage", BuildingName, amount));
        Health.Curr += amount;
    }



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
}
