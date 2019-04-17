using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Indicator 
{
    //For the Indicator if I have enough time to go back & fix it
    public GameObject CurrentIndicator;
    public GameObject SpawnLocation;
    public GameObject RangeIndicator;
    public SpellSlot CurrentSpell;
    public GameObject AutoTarget;
    public GameObject Target;

    public bool OnRangeIndicator;





    public void UpdateIndicator()
    {
        if (CurrentIndicator == null)
        return;

    if(CurrentSpell.Ability.Effect == GameConsts.EFFECT.PROJECTILE)
        {
            CurrentIndicator.transform.rotation = Quaternion.Slerp(CurrentIndicator.transform.rotation, GameFuncs.RotateTowardsMouse(CurrentIndicator.transform), GameConsts.ROTATE_SPEED * Time.deltaTime);
        }
    else
        {
            CurrentIndicator.transform.position = GameFuncs.MoveTowardsMouse(CurrentIndicator.transform);
        }



    }

    public void UpdateRangedIndicator()
    {
        RangeIndicator.SetActive(OnRangeIndicator);

        if (!OnRangeIndicator == false && CurrentSpell == null)
            return;



        Projector projector = RangeIndicator.transform.GetChild(0).GetComponent<Projector>();
        projector.orthographicSize = CurrentSpell.Ability.Range.x;
        projector.aspectRatio = CurrentSpell.Ability.Range.y;


        //RangeIndicator.SetActive(true);
    }

    //temp
    public bool UpdateAutoTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            GameObject go = hit.collider.gameObject;

            switch (go.tag)
            {
                case "Ground":
                    {
                        AutoTarget = null;
                        return false;
                    }
                case "Enemy":
                    {
                        AutoTarget = go;
                        return true;
                    }
                case "Ally":
                    {
                        AutoTarget = null;
                        return false;
                    }
                case "RedTeam":
                    {
                        AutoTarget = go;
                        return true;
                    }
                case "BlueTeam":
                    {
                        AutoTarget = go;
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }
        return false;
    }


    public bool CheckDistance()
    {


        
        float dist = Vector3.Distance(CurrentIndicator.transform.position, RangeIndicator.transform.position);
        return dist <= CurrentSpell.Ability.Range.x;
    }


    public bool CheckDistance(float range, Vector3 player, Vector3 enemy)
    {



        float dist = Vector3.Distance(player, enemy);
        return dist <= range;
    }
}
