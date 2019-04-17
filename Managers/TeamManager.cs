using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{

    public List<ChampionStats> Champions;

    public int TeamID = 0;
    public List<GameObject> SpawnPoints;
    public List<Lane> LaneSpawnPoints;
    public Transform Minions;





    public GameObject MeleePrefab;
    public GameObject RangedPrefab;
    public GameObject CannonPrefab;
    public GameObject SuperPrefab;


    //Wave Number Deals with which wave has spawned, as the game progress stronger waves will spawn
    public int waveNumber = 0;

    public float WaveTimer = 0;

    //If I want to add Inhibs and super minions in the game I can use the following code below
    //Its here just in-case I change my mind
    //What I would plan to do with this is:
    //First: I would need to add inhibs to the map
    //Second: When they are destoryed I would make super minions spawn within the game

    //Inhibs
    public bool MidInhibitor = false;
    public bool TopInhibitor = false;
    public bool BotInhibitor = false;




    private void Start()
    {
        WaveTimer = GameConsts.MINION_WAVE_TIME;
    }


    private void Update ()
    {
        
        SpawnWave();
      
        
    }


    //Spawns Wave
    void  SpawnWave()
    {
        if (InGameManager.Instance.GameTime < GameConsts.MINION_SPAWN_TIME)
            return;

        if (WaveTimer >= GameConsts.MINION_WAVE_TIME)
        {
            // Spawn Wave

            //Debug
            System.TimeSpan t = System.TimeSpan.FromSeconds(InGameManager.Instance.GameTime);
            Debug.Log(string.Format("WaveNumber: {0} has spawned at {1}!", waveNumber, string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds)));

            //TEMP
            // SpawnUnit(MeleePrefab, GameConsts.SPAWN_MID);


            #region Wave Spawner
          //  Super Minions, Read comments above
          //  This will be commented out unless I decide to add a inhib system/ Super minion system

            if (BotInhibitor || MidInhibitor || TopInhibitor)
            {
                if (BotInhibitor && MidInhibitor && TopInhibitor)
                {
                    for (int m = 0; m < GameConsts.SUPER_ALL_COUNT; m++)
                    {
                        //Spawn Super Minions
                      //  SpawnUnit(SuperPrefab, GameConsts.SPAWN_TOP);
                        SpawnUnit(SuperPrefab, GameConsts.SPAWN_MID);
                        SpawnUnit(SuperPrefab, GameConsts.SPAWN_BOT);

                    }
                }
                else
                {
                    //Spawn Super Minions
                   // SpawnUnit(SuperPrefab, GameConsts.SPAWN_TOP);
                    SpawnUnit(SuperPrefab, GameConsts.SPAWN_MID);
                    SpawnUnit(SuperPrefab, GameConsts.SPAWN_BOT);
                }
            }


            //Melee 
            for (int m = 0; m < GameConsts.MELEE_COUNT; m++)
            {
                //Spawn Melee Minions
               // SpawnUnit(MeleePrefab, GameConsts.SPAWN_TOP);
                SpawnUnit(MeleePrefab, GameConsts.SPAWN_MID);
                SpawnUnit(MeleePrefab, GameConsts.SPAWN_BOT);

            }



            //Cannon Spawn if Cannon Wave
            ///Cannon Minions will spawn more frequent as time goes on
            if (InGameManager.Instance.GameTime <= GameConsts.CANNON_FIRST_WAVE)
            {
                //Every 3 Waves
                if (waveNumber % 3 == 0)
                {
                    // SpawnUnit(CannonPrefab, GameConsts.SPAWN_TOP);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_MID);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_BOT);
                }
            }
            else if (InGameManager.Instance.GameTime > GameConsts.CANNON_FIRST_WAVE && InGameManager.Instance.GameTime <= GameConsts.CANNON_SECOND_WAVE)
            {
                //Every 2 Waves
                if (waveNumber % 2 == 0)
                {
                    //  SpawnUnit(CannonPrefab, GameConsts.SPAWN_TOP);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_MID);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_BOT);
                }
            }
            else
            {
                //Every Wave
                for (int m = 0; m < GameConsts.CANNON_COUNT; m++)
                {
                    //Spawn Ranged Minions
                    //  SpawnUnit(CannonPrefab, GameConsts.SPAWN_TOP);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_MID);
                    SpawnUnit(CannonPrefab, GameConsts.SPAWN_BOT);

                }
            }


            //Ranged
            for (int m = 0; m < GameConsts.RANGED_COUNT; m++)
            {
                //Spawn Ranged Minions
              //  SpawnUnit(RangedPrefab, GameConsts.SPAWN_TOP);
                SpawnUnit(RangedPrefab, GameConsts.SPAWN_MID);
                SpawnUnit(RangedPrefab, GameConsts.SPAWN_BOT);

            }

            #endregion


            WaveTimer = 0;
            waveNumber++;
        }
        else
        {
            WaveTimer += Time.deltaTime;
        }

    }


    private void SpawnUnit(GameObject prefab, int spawnLoc)
    {


        GameObject go = Instantiate(prefab, SpawnPoints[spawnLoc].transform.position, Quaternion.identity);

        go.transform.SetParent(Minions);

        Minion minion = go.GetComponent<Minion>();

        //Minion Pathing
        minion.Path = InGameManager.MakePath(TeamID, spawnLoc);
        minion.Target = minion.Path[0].transform;


    }


}

[System.Serializable]
public class Lane
{
    public List<GameObject> Waypoints;
}
