using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AutoAttack : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Spell Ability;
    public Vector3 StartPos;
    public Vector3 CurrPos;
    public Vector3 TargetPos;
    public string OwnerTag;

    void Start()
    {
        Agent = GetComponentInChildren<NavMeshAgent>();

        Agent.updateRotation = false;
        Agent.updatePosition = true;
        Agent.SetDestination(TargetPos);

        StartPos = transform.position;
    }

    private void Update()
    {
        CurrPos = transform.position;
        float dist = Vector3.Distance(StartPos, CurrPos);

        if(dist >= Ability.Range.x)
        {
            Destroy(gameObject);
        }
    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(OwnerTag) && Ability.BaseDamage > 0)
            return;

        if (!other.gameObject.CompareTag(OwnerTag) && Ability.BaseDamage > 0)
            return;

        Component comp = other.gameObject.GetComponent(typeof(IDamageable));
        if (comp == null)
            return;

        GameFuncs.ChangeHealth(other, comp, Ability);
        Destroy(gameObject);





    }
}
