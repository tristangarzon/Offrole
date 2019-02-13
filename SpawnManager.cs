using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Photon.MonoBehaviour
{


    public GameObject[] redSpawn;
    public GameObject[] blueSpawn;

    public GameObject lobbyCamera;


    public int state = 0;

    void Connect ()
    {
        PhotonNetwork.ConnectUsingSettings("V1.0");
    }

    void OnJoinedLobby()
    {
        state = 1;
    }
 
    //  If players are unable to join a room it will create a new room
    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }



    void OnJoinedRoom()
    {
        state = 2;
    }


    void Start()
    {
        
    }

  
    void Update()
    {

 


    }

    void OnGUI()
    {
        switch (state)
        {
            //Preconnect Screen
            case 0:
                if (GUI.Button(new Rect(10, 10, 100, 30), "Connect"))
                {
                    Connect();
                }
                break;


                //Connected to the server
            case 1:
                GUI.Label(new Rect(10, 10, 100, 30), "Connected");

                if (GUI.Button(new Rect(10, 10, 100, 30), "Search 1v1"))
                {
                    PhotonNetwork.JoinRandomRoom();
                }

                break;

            case 2:
                //Champ Select
                GUI.Label(new Rect(10, 10, 200, 30), "Select Your Champion");
                if (GUI.Button(new Rect(70, 10, 100, 30), "Warrior"))
                {
                    Spawn(0, "Warrior");
                }

               if (GUI.Button(new Rect(70,10,200,50), "Ranger"))
                {
                    Spawn(0, "Ranger");
                }

                break;

            case 3:
                // In Game
                break;
        }
    }


    void Spawn (int team, string character)

    {
        state = 3;
        //Debug.Log("You are on team ..." + team + ", And are playing as " + character);
        lobbyCamera.SetActive(false);

        GameObject mySpawn = redSpawn[Random.Range(0, redSpawn.Length)];


       GameObject myPlayer = PhotonNetwork.Instantiate(character, mySpawn.transform.position, mySpawn.transform.rotation, 0);

        //All Scripts related to movement & abilites will need to be turned on here
        //Ill need to do this in the future once I get movement working
        //I will also need to make mulitple of the code I have below

       // myPlayer.GetComponent<Scripts that I want to be active go here> ().enabled = true;

    }
}
