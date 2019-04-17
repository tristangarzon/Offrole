using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Spell
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public KeyCode Key;

    public int Cost;
    public float CoolDown;
    public float CastTime;
    public float BaseDamage;
    public float SpellTime;

    public Vector2 Range; // x = width y = length
    public GameConsts.EFFECT Effect;

    //temp
    public Status StatusEffect;






    //Indicator if I have enough time
    public GameObject Indicator;
    public GameObject Prefab;


    public GameObject SpawnIndicator(Transform t)
    {
         GameObject go = GameObject.Instantiate(Indicator);

        if (Effect == GameConsts.EFFECT.AOE)
            return go;

    //Projector projector =  go.transform.GetChild(0).GetComponent<Projector>();
    //projector.orthographicSize = Range.x;
    //projector.aspectRatio = Range.y;

    //projector.transform.position = new Vector3(0, 0.5, Range.y);

    //    go.transform.SetParent(t);
    //    go.transform.position = t.position;

    //    go.transform.rotation = GameFuncs.RotateTowardsMouse(go.transform);
        return go;
    }





}
