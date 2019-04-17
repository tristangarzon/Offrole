using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionStats : BaseCharacter
{
    public int Kills;
    public int Deaths;
    public int Assist;
    public int MinionScore;
    public int Gold;
    public List<SpellSlot> SpellSlots;




   private void Update ()
    {
        if (Health.Curr > 0)
        {
            UpdateStats();
            UpdateSpells();
        }
        else
        {
            Destroy(gameObject);
            Debug.Log(CharacterName + " Has Died");
        }
    }

    private void UpdateSpells()
    {
      foreach(SpellSlot slot in SpellSlots)
        {
            if (slot.Ability.Icon == null)
            {
                slot.Icon.enabled = false;
                slot.CoolDown.enabled = false;
                slot.KeyName.enabled = false;
                return;
            }
            else
            {
               slot.Icon.enabled = true;
               slot.UpdateIcon();
               slot.UpdateTimer();
               slot.UpdateKeyName();

            }
        }
    }






}
