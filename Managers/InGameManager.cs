using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;

    public float   GameTime;

    public int CurrentPlayerTeamId = 0;

    public List <TeamManager> Teams;

  





    private void Awake()
    {
        if (Instance != this)
            Instance = this;


    }


    private void Update()
    {
        GameTime += Time.deltaTime;

       
    }

    //Minion Pathing
    public static List<GameObject> MakePath(int team, int lane)
    {
        List<GameObject> newPath = new List<GameObject>();

        int otherTeam = team == 0 ? 1 : 0;

        foreach(GameObject go in Instance.Teams[team].LaneSpawnPoints[lane].Waypoints)
        {
            newPath.Add(go);
        }

        //Reserve foreach statement 
        for (int i = Instance.Teams[otherTeam].LaneSpawnPoints[lane].Waypoints.Count - 1; i >= 0;i--)
        {
            newPath.Add(Instance.Teams[otherTeam].LaneSpawnPoints[lane].Waypoints[i]);
        }

        return newPath;

    }

    public static int GetTeamKills(int i)
    {

        if (Instance == null)
            return 0;

        int amount = 0;



        foreach(ChampionStats champ in Instance.Teams[i].Champions)
        {
            amount += champ.Kills;
        }


        return amount;

    }


}
