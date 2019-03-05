using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour { 

    public GameObject standbyCamera;

    public GameObject[] redSpawn;
    public GameObject[] blueSpawn;

    int state = 0;


    void Start()
    {
        
    }

    void OnGUI()
    {
        switch (state)
        {
            case 0:

                //Spawns Berserker
                if (GUI.Button(new Rect(40, 40, 200, 20), "Berserker"))
                {
                    SpawnPlayer(0, "Berserker");
                    state = 1;
                    PhotonNetwork.ConnectUsingSettings("1.0");
                }


                if (GUI.Button(new Rect(40, 70, 200, 20), "Recon"))
                {
                    SpawnPlayer(0,"Recon");
                    state = 1;
                    PhotonNetwork.ConnectUsingSettings("1.0");
                }


                if (GUI.Button(new Rect(40, 100, 200, 20), "Sorcerer"))
                {
                    SpawnPlayer(0, "Sorcerer");
                    state = 1;
                    PhotonNetwork.ConnectUsingSettings("1.0");
                }

                if (GUI.Button(new Rect(40, 130, 200, 20), "PlayerTest"))
                {
                    SpawnPlayer(0, "PlayerTest");
                    state = 1;
                    PhotonNetwork.ConnectUsingSettings("1.0");
                }

                break;

            case 1:
                //InGame
                break;


        }
    }

    [PunRPC]
        public void SpawnPlayer (int team, string character)
    {
        //Currently Players will only spawn on red side, ill need to change this to make it its own void
        GameObject mySpawn = redSpawn[Random.Range(0, redSpawn.Length)];
        GameObject myPlayer = PhotonNetwork.Instantiate(character, mySpawn.transform.position, mySpawn.transform.rotation, 0);

        //Turns off Champ Selection Camera
        standbyCamera.SetActive(false);

        GameObject mobaCam = PhotonNetwork.Instantiate("MobaCam", mySpawn.transform.position, mySpawn.transform.rotation, 0);
        Moba_Camera mobCamScript = mobaCam.GetComponent<Moba_Camera>();
        mobCamScript.settings.lockTargetTransform = myPlayer.transform;
      
    }





    



}
