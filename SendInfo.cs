using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendInfo : MonoBehaviour
{

   

    void Start()
    {
      
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
                Send(hit.point);
            }

        }

    }

    //Movement
    void Send(Vector3 hitPoint)
    {
        this.GetComponent<PhotonView>().RPC("RecievedMove", PhotonTargets.AllBuffered, hitPoint);
    }


}
