using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]

public class Minion : BaseCharacter
{

    public UnityEngine.AI.NavMeshAgent Agent;
    public ThirdPersonCharacter Character;
    public Transform Target;
    public List<GameObject> Path;
    public int PathCount = 0;
    //public float distant; 

  




    private void Start()
    {
        Agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        Character = GetComponent<ThirdPersonCharacter>();


        Agent.updateRotation = false;
        Agent.updatePosition = true;

    }


   

    private void Update()
    {

       

        if (Health.Curr > 0)
        {
            AgentMove();
            UpdateStats();
        }
        else
        {
            Destroy(gameObject);
        }


        //distant = Agent.remainingDistance;


    }

    private void AgentMove()
    {

        if (Target != null)
            Agent.SetDestination(Target.position);

        if (Agent.remainingDistance >= Agent.stoppingDistance)
            Character.Move(Agent.desiredVelocity, false, false);
        else
        {
            if (PathCount + 1 < Path.Count)
            {
                PathCount++;
                Target = Path[PathCount].transform;
                Agent.SetDestination(Target.position);
            }

        }


    }
}
