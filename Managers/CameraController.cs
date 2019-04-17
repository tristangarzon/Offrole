using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameConsts.CAMERA_STATE state;
    public Transform Player;
    public Vector3 Offset = new Vector3(0f, 7.5f, 0f);
    public float PanThickness = 5;
    public float PanSpeed = 5;

    

    private void LateUpdate()
    {
     
        

        if(Input.GetKeyDown(KeyCode.Y))
        {
            state = state == GameConsts.CAMERA_STATE.LOCKED ? GameConsts.CAMERA_STATE.UNLOCKED : GameConsts.CAMERA_STATE.LOCKED;
        }


        switch(state)
        {
            case GameConsts.CAMERA_STATE.LOCKED:
                {
                    Locked();
                    break;
                }
            case GameConsts.CAMERA_STATE.UNLOCKED:
                {
                    Unlocked();
                    break;
                }
              
        }
    }

    void Locked()
    {
        transform.position = Player.position + Offset;
        if(Input.GetKeyUp(KeyCode.Space))
        {
            state = GameConsts.CAMERA_STATE.UNLOCKED;
        }
    }

    void Unlocked()
    {
        state = Input.GetKey(KeyCode.Space) ? GameConsts.CAMERA_STATE.LOCKED : GameConsts.CAMERA_STATE.UNLOCKED;



        CameraMove();
    }
    void CameraMove()
    {
        Vector3 pos = transform.position;

        if(Input.mousePosition.y >= Screen.height - PanThickness)
        {
            pos.z += PanSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y < PanThickness)
        {
            pos.z -= PanSpeed * Time.deltaTime;
        }
        
       
        if (Input.mousePosition.x >= Screen.width - PanThickness)
        {
            pos.x += PanSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x < PanThickness)
        {
            pos.x -= PanSpeed * Time.deltaTime;
        }

        transform.position = pos;

    }
}
