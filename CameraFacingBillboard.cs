using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    public Camera M_Camera;

    void Update()
    {
      if (M_Camera != null)
        {
            transform.LookAt(transform.position + M_Camera.transform.rotation * Vector3.forward, M_Camera.transform.rotation * Vector3.up);
        }

      else
        {
            M_Camera = Camera.main;
        }



    }
}
