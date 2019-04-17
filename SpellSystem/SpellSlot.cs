using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



[System.Serializable]
public class SpellSlot 
{
    public Spell Ability;
    public Image Icon;
    public Image CoolDown;
    public Text KeyName;

    public float CoolDownTimer;
    public bool OnCoolDown = false;
    public bool OnPressed = false;

    public float Percent()
    {
       
        return CoolDownTimer / Ability.CoolDown;
    }


    public void CastSpell(string ownerTag, Transform loc, Transform playerLoc)
    {
        OnCoolDown = true;
        Debug.Log("CastSpell ::" + Ability.Name);

        playerLoc.rotation = GameFuncs.RotateTowardsMouse(playerLoc);

        Vector3 pos = Ability.Effect == GameConsts.EFFECT.PROJECTILE ? loc.position : loc.position;

        GameObject go = GameObject.Instantiate(Ability.Prefab, pos, loc.rotation);

        Projectile projectile = (Projectile)go.GetComponent(typeof(Projectile));

        switch(Ability.Effect)
        {
            case GameConsts.EFFECT.PROJECTILE:
                {
                    Projectile proj = go.GetComponent<Projectile>();
                    proj.Ability = Ability;
                    proj.OwnerTag = ownerTag;
                    break;
                }
            case GameConsts.EFFECT.AOE:
                {

                    AoE aoe = go.GetComponent<AoE>();
                    aoe.Ability = Ability;
                    aoe.OwnerTag = ownerTag;

                    //Size of AOE
                    float scale = Ability.Range.x * 0.2f;
                    go.transform.GetChild(0).localScale = new Vector3(scale, 0.1f, scale);
                    break;
                }

            case GameConsts.EFFECT.CLICK:
                {
                    break;
                }
        }
    }

    public void UpdateTimer()
    {
        if(CoolDownTimer < Ability.CoolDown && OnCoolDown)
        {
            CoolDown.enabled = true;
            CoolDownTimer += Time.deltaTime;
            CoolDown.fillAmount = Percent();
        }
        else
        {
            CoolDown.enabled = false;
            CoolDownTimer = 0;
            OnCoolDown = false;
        }
    }


    public void UpdateIcon()
    {
        Icon.sprite = Ability.Icon;
    }

    public void UpdateKeyName()
    {
        if (Ability.Key == KeyCode.None)
        {
            KeyName.enabled = false;

        }
        else
        {
            KeyName.enabled = true;
            KeyName.text = Ability.Key.ToString();
        }
    }

}
