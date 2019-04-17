using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    private GameObject spawnerLocations;
    public GameObject[] prefabsToSpawn;
    private GameObject[] prefabsToClone;

    

    void Start()
    {
        //Initalize
        prefabsToClone = new GameObject[prefabsToSpawn.Length];

        //Spawn Objects
        Spawn();
    }


    public void Spawn()
    {
        spawnerLocations = GameObject.FindGameObjectWithTag("BlueSpawns");

        for (int i = 0; i < prefabsToSpawn.Length; i++)
        {
            prefabsToClone[i] = Instantiate(prefabsToSpawn[i], spawnerLocations.transform.position, Quaternion.Euler(0, 90, 0));
        }
    }

    void DestoryClonedGameObjects ()
    {
        for (int i = 0; i < prefabsToClone.Length; i++)
        {
            Destroy(prefabsToClone[i]);
        }
    }

    public void Respawn()
    {
        DestoryClonedGameObjects();


        Spawn();
    }
}
