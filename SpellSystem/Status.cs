using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[System.Serializable]
public class Status 
{
    public bool Active = false;
    public bool HasStatus = false;
    public GameConsts.STATUS_EFFECT StatusEffect;
    public GameConsts.STAT Stat;
    public GameConsts.DURATION Duration;
    public GameConsts.ATTACK_TYPES AttackType;
    public float Amount;
    public float EffectDuration;
    public float TickTimer = 5;
    public Stopwatch Timer = new Stopwatch();



    public void OnStart(BaseCharacter bc)
    {
        Active = true;
        bc.StartCoroutine(OnUpdate(bc));


        if (Duration == GameConsts.DURATION.STATIC)
        {
            if (StatusEffect != GameConsts.STATUS_EFFECT.NONE)
            {
                Component component = bc.GetComponent(typeof(IDamageable));
                (component as IDamageable).RecieveStatusEffect(StatusEffect, Amount);
            }
        }
    }

    IEnumerator OnUpdate(BaseCharacter bc)
    {
        Timer.Start();
        Component component = bc.GetComponent(typeof(IDamageable));
        if (Duration == GameConsts.DURATION.ONUSE)
        {
            (component as IDamageable).ChangeHealth(Amount, AttackType);
        }
        else
        {
            while (Timer.Elapsed.TotalSeconds <= EffectDuration)
            {
                if (Duration == GameConsts.DURATION.DYNMAIC)
                {
                    if (Stat == GameConsts.STAT.HEALTH)
                    {
                        yield return new WaitForSecondsRealtime(TickTimer);
                        (component as IDamageable).ChangeHealth(Mathf.Floor(Amount / EffectDuration) * TickTimer, AttackType);
                    }
                }
                else
                {
                    yield return new WaitForSecondsRealtime(1);
                }
            }
        }

        if (Duration == GameConsts.DURATION.STATIC)
        {
            if (StatusEffect != GameConsts.STATUS_EFFECT.NONE)
            {
                (component as IDamageable).EndStatusEffect(StatusEffect);
            }
        }


        Timer.Stop();
        Timer.Reset();
        Active = false;

    }

}
