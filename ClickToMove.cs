using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{

    private Animator anim;
    private NavMeshAgent navMesh;


    Vector3 newposition;

    private bool walk = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        newposition = this.transform.position;
    }



    void Update()
    {

        bool RMB = Input.GetMouseButtonDown(1);

        if (RMB)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            //Ground
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Ground")
            {
                navMesh.destination = hit.point;
            }


        }



        //if (navMesh.remainingDistance <= navMesh.stoppingDistance)
        //{
        //    walk = false;
        //}
        //else
        //{
        //    walk = true;
        //}




        //pathfinding
        if (navMesh.destination != newposition && newposition != null)
        {
            navMesh.destination = newposition;
        }


    }
}


////{        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//RaycastHit hit;

//        if (Input.GetMouseButtonDown(1))
//        {
//            if(Physics.Raycast(ray, out hit, 100))
//            {
//                navMesh.destination = hit.point;
//            }
//        }

//        if (navMesh.remainingDistance <= navMesh.stoppingDistance)
//        {
//            walk = false;
//        }
//        else
//        {
//            walk = true;
//        }
//        anim.SetBool("Walk", walk);}