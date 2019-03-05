using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RecievedMovement : MonoBehaviour
{
    public NavMeshAgent nav;


    Vector3 newposition;
    public float speed;
    public float walkRange;

    public GameObject graphics;

    //Animations
    private bool mRunning = false;


    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        newposition = this.transform.position;

    }


    void Update()
    {
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

    }

    //Movement
    [PunRPC]
    public void RecievedMove(Vector3 movePos)
    {
        newposition = movePos;
    }



}
