using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConsts
{

    public static int MELEE_COUNT = 3;
    public static int RANGED_COUNT = 3;
    public static int CANNON_COUNT = 1;
    public static int SUPER_COUNT = 1;
    public static int SUPER_ALL_COUNT = 2;


    public static int RED_TEAM = 0;
    public static int BLUE_TEAM = 1;


    public static int SPAWN_MID = 0;
    public static int SPAWN_BOT = 1;
   // public static int SPAWN_TOP = 2;

    //How Long it takes to Spawn Minions
    public static float MINION_SPAWN_TIME = 5; //75
    public static float MINION_WAVE_TIME = 60; //30


    //How Long it takes to Spawn Cannon Minions
    public static float CANNON_FIRST_WAVE = 120; //20 mins
    public static float CANNON_SECOND_WAVE = 210; //35 mins

    public static float ROTATE_SPEED = 20;

    public static float SPEED_MODIFIER = 10;


    public static int STAT_ATTACK_SPEED = 3;


    //Regen
    public static int STAT_HEALTH_REGEN = 4;
    public static int STAT_RESOURCE_REGEN = 5;


    public enum CAMERA_STATE
    {
        LOCKED,
        UNLOCKED

    }

    public enum TARGETING_STATE
    {
        NORMAL,
        QUICK, 
        SMART
    }

    public enum EFFECT
    {
        PROJECTILE,
        AOE,
        CLICK
    }

    public enum STATUS_EFFECT
    {
        NONE,
        ROOT,
        SPEED,
        STUN
    }

    public enum DURATION
    {
        //BUFF/DEBUFF
        STATIC,
        //OVER TIME EFFECT
        DYNMAIC,
        //ON USE (ONE TIME ONLY, EG. POTS)
        ONUSE
    }

    public enum STAT
    {
        HEALTH,
        RESOURCE,
        ATTACK_SPEED,
        ATTACK_DAMAGE,
        ABILITY_POWER,
        ARMOR,
        MAGIC_RESIST,
        SPEED
    }


    public enum ATTACK_TYPES
    {
        TRUE,
        PHYICAL,
        MAGICAL
    }




}

///NOTES///
///1. The game needs to be a CTF gamemode, meaning unlike how a MOBAS win condition is to destory the nexus
///I must make the win condition whoever captures the most flags
///2. I need to find a way to have the timer count down so that when it hits zero the sides are swapped and the game resets 
///but at the same time I need to make sure that the game can record the previous score of the other team
///3. For now im going to make the win condition like any MOBA but will try and put in a CTF gamemode
///I will likely use the normal win condition for the 1v1 map but include the 3 following win conditions
///A) Destory Enemy Tower
///B) Kill the Enemy
///C) Kill 100 Minions
///Therefore meaning that I will need to have a value keep track of both players minion counts, in the normal CTF gamemode I will just have it keep track without the result being a win
///4. I will need to change minion timers to address the fact that the game timer will be counting down rather than up
///5. I will need to add a feature that when a flag is captured gives the attacking team a set amount of time
///*SIDENOTES*
///I feel its simpler to make the game first like any normal MOBA and have the win condition be like that since I already have the knowledge on how a normal MOBA works
///Later I will implement the system I have stated above