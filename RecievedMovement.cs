using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RecievedMovement : MonoBehaviour
{
    private NavMeshAgent nav;


    Vector3 newposition;
    public float speed;
    public float walkRange;

    public GameObject graphics;

    private Animator anim;

    private bool Walk = false;





    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        newposition = this.transform.position;
        anim = GetComponent<Animator>();

    }


    void Update()
    {


        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            Walk = false;
        }
        else
        {
            Walk = true;
        }

        /*
        if (Vector3.Distance(newposition, this.transform.position) > walkRange)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newposition, speed * Time.deltaTime);
            Quaternion transRot = Quaternion.LookRotation(newposition - this.transform.position, Vector3.up);
            graphics.transform.rotation = Quaternion.Slerp(transRot, graphics.transform.rotation, 0.7f);
        }
        */

        //PathFinding
        if (nav.destination != newposition && newposition != null)
        {
            nav.destination = newposition;
        }

        anim.SetBool("Walk", Walk);



    }

    //Movement
    [PunRPC]
    public void RecievedMove(Vector3 movePos)
    {
     
        newposition = movePos;
    }



}