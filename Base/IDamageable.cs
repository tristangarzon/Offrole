using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    BaseStat Health { get; }
    void ChangeHealth(float amount, GameConsts.ATTACK_TYPES attackType);
    void RecieveStatusEffect(GameConsts.STATUS_EFFECT effect, float amount);
    void EndStatusEffect(GameConsts.STATUS_EFFECT effect);
}
