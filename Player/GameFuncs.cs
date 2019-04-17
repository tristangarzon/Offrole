using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFuncs 
{
    public static Quaternion RotateTowardsMouse(Transform t)
    {
        Quaternion results = Quaternion.identity;

        Plane indicatorPlane = new Plane(Vector3.up, t.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitDist = 0;

        if (indicatorPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);

            results = Quaternion.LookRotation(targetPoint - t.transform.position);
        }


        return results;
    }

    public static Vector3 MoveTowardsMouse(Transform t)
    {
        Vector3 results = t.position;

        Plane indicatorPlane = new Plane(Vector3.up, t.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitDist = 0;

        if (indicatorPlane.Raycast(ray, out hitDist))
        {
            results = ray.GetPoint(hitDist);

        }


        return results;
    }

    public static void ResetSpellSlot(SpellSlot slot, Indicator IndicatorSystem)
    {
        GameObject.Destroy(IndicatorSystem.CurrentIndicator);
        IndicatorSystem.CurrentIndicator = null;
        IndicatorSystem.OnRangeIndicator = false;
       // IndicatorSystem.CurrentSpell = null;
        slot.OnPressed = false;
    }

    public static void ChangeHealth(Collider other, Component comp, Spell spell)
    {
        (comp as IDamageable).ChangeHealth(spell.BaseDamage, GameConsts.ATTACK_TYPES.PHYICAL);
        //if (spell.StatusEffect.HasStatus)
        //spell.StatusEffect.OnStart(other.gameObject.GetComponent<BaseCharacter>());
    }


    public static float GetAttackSpeed(float attackSpeed)
    {
        float results = 0;

        results = attackSpeed * 60; //2.5 * 60 = 150
        results = 60 / results;     // 60 / 150 = 0.4 

        return results;
    }


}
