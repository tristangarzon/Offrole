using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(ChampionStats))]


public class ChampionController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent Agent;
    public ThirdPersonCharacter Character;
    public Vector3 Target;
    public ChampionStats Stats;
    public GameConsts.TARGETING_STATE TargetingState;
    public Indicator IndicatorSystem;


    //Temp
    public float autoAttackCoolDown;
    public float autoAttackCurTime;

    //temp
    public float CurrentAttack = 0;
    //public float AutoRange;
    public Spell AutoAttack;
    //public GameObject AutoAttackPrefab;




    private Animator anim;
    private bool Walk = false;
    private bool Auto = false;


    private void Start()
    {
        Agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        Character = GetComponent<ThirdPersonCharacter>();
        anim = GetComponent<Animator>();
        Stats = GetComponent<ChampionStats>();

        Agent.SetDestination(transform.position);
        Agent.updateRotation = false;
        Agent.updatePosition = true;
    }

    private void Update()
    {
        Anims();
        

        //temp
        if (Stats.IsStunned)
            return;

        UpdateAutoAttack();
        KeyInputs();
        MouseInputs();
        TargetingFSM();

        //temp
        if (Stats.IsRooted)
            return;


        AgentMove();
    }

    private void FixedUpdate()
    {
       IndicatorSystem.UpdateIndicator();
        IndicatorSystem.UpdateRangedIndicator();
    }

    private void AgentMove()
    {
        
            Agent.SetDestination(Target);

        if (Agent.remainingDistance > Agent.stoppingDistance)
            Character.Move(Agent.desiredVelocity, false, false);
        else
            Character.Move(Vector3.zero, false, false);
    }

    private void KeyInputs()
    {
        foreach (SpellSlot slot in Stats.SpellSlots)
        {
            if (slot.Ability.Key == KeyCode.None)
                continue;
            if (slot.OnPressed)
                return;
        }
       
            foreach (SpellSlot slot in Stats.SpellSlots)
        {
            if (slot.Ability.Key == KeyCode.None)
                continue;
            if (slot.OnCoolDown)
                return;

            if (Input.GetKeyDown(slot.Ability.Key))
            {
                slot.OnPressed = true;
            }
            else if (Input.GetKeyUp(slot.Ability.Key))
            {
                slot.OnPressed = false;

            }
        }

    }


    private void MouseInputs()
    {

        if (Input.GetMouseButtonDown(0))
        {
            foreach(SpellSlot slot in Stats.SpellSlots)
            {
                if (slot.OnPressed)
                {
                    if (Stats.Resource.Curr < slot.Ability.Cost)
                    {
                        GameFuncs.ResetSpellSlot(slot, IndicatorSystem);
                        return;
                    }

                    //if (IndicatorSystem.CheckDistance() == false)
                    //{
                    //    GameFuncs.ResetSpellSlot(slot, IndicatorSystem);
                    //    return;
                    //}

                    Stats.Resource.Curr -= slot.Ability.Cost;

                    slot.CastSpell(tag, IndicatorSystem.SpawnLocation.transform, transform);
                   GameFuncs.ResetSpellSlot(slot, IndicatorSystem);
                    break;
                }
            }
        }

        else if (Input.GetMouseButtonDown(1))
        {


            foreach (SpellSlot slot in Stats.SpellSlots)
            {
                if(slot.OnPressed)
                {
                    GameFuncs.ResetSpellSlot(slot, IndicatorSystem);
                    break;
                }
            }

           if(IndicatorSystem.UpdateAutoTarget( )&& UpdateAutoAttack())
            {
                //ATTACK THE TARGET
                CastAutoAttack(tag, IndicatorSystem.AutoTarget.transform, transform, IndicatorSystem);
                CurrentAttack = 0;

            }
           else
            {
                Movement();
            }
        }
    }

    private void ResetSpellSlot(SpellSlot slot)
    {
        //Destroy(IndicatorSystem.CurrentIndicator);
        //IndicatorSystem.CurrentIndicator = null;
        IndicatorSystem.OnRangeIndicator = false;
        slot.OnPressed = false;
    }

    private void Movement()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            switch (hitObject.tag)
            {
                case "Ground":
                    {
                        Target = hitInfo.point;
                        break;
                    }
                case "RedTeam":
                    {
                        Target = hitInfo.point;
                        
                        break;
                    }
                case "BlueTeam":
                    {
                        //temp
                        Target = hitInfo.point;

                        break;
                    }
                default:
                    {
                        Debug.Log("Tag: " + hitObject.tag);
                        break;
                    }
            }

        }
    }

    private void TargetingFSM()
    {
        switch(TargetingState)
        {
            case GameConsts.TARGETING_STATE.NORMAL:
                {
                    TargetingNormal();
                    break;
                }
        }
    }

    private void TargetingNormal()
    {
        foreach(SpellSlot slot in Stats.SpellSlots)
        {
            if (!slot.OnPressed)
                continue;


            //For Indicator if I have enough time
            //if (indicatorsystem.currentindicator == null)
            //{
            //    if (slot.ability.effect == gameconsts.effect.aoe)
            //    {
            //        indicatorsystem.onrangeindicator = true;
            //        indicatorsystem.currentspell = slot;
            //    }
            //    indicatorsystem.currentindicator = slot.ability.spawnindicator(transform);
            //}
        }
    }

    public bool UpdateAutoAttack()
    {
        anim.SetBool("Auto", Auto);


        if (CurrentAttack >= GameConsts.STAT_ATTACK_SPEED)
        {
            Auto = false;
            return true;
        }
        else
        {
            Auto = true;
            CurrentAttack += Time.deltaTime;
            return false;

        }

    }





    public void CastAutoAttack(string ownerTag, Transform enemy, Transform player,  Indicator indicator)
    {

        if (!indicator.CheckDistance(AutoAttack.Range.x, player.position, enemy.position))
        {
            // move 2 a loc to attack
            return;
        }

        player.rotation = GameFuncs.RotateTowardsMouse(player);

        Vector3 pos = new Vector3
            (indicator.SpawnLocation.transform.position.x, 0, indicator.SpawnLocation.transform.position.z);
           


        GameObject go = GameObject.Instantiate(AutoAttack.Prefab, pos,  indicator.SpawnLocation.transform.rotation);

        AutoAttack aa = go.GetComponent<AutoAttack>();
        aa.Ability = AutoAttack;
        aa.TargetPos = enemy.position;
        aa.OwnerTag = ownerTag;

    }



    private void Anims()
    {
        if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            Walk = false;
        }
        else
        {
            Walk = true;
        }
        anim.SetBool("Walk", Walk);
    }


    //Auto Attack






}
