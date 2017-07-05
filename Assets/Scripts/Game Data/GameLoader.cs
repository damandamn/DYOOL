﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject morganisPrefab;
    public GameObject healthUI;

    StageBuilder stagebuilder;

    public static HitManager hitmanager;

    public static int playerCount = 2;

    public PlayerController player1;
    public PlayerController player2;

    public Material marshmallowStand;
    public Material marshmallowHitstun;

    //Example Hitboxes
    public List<GameObject> jabHitboxes;
    public List<GameObject> fTiltHitboxes;
    public List<GameObject> uTiltHitboxes;
    public List<GameObject> dTiltHitboxes;

    public List<GameObject> nAirHitboxes;
    public List<GameObject> dAirHitboxes;
    public List<GameObject> uAirHitboxes;
    public List<GameObject> fAirHitboxes;
    public List<GameObject> bAirHitboxes;

    //Morganis Hitboxes
    public List<GameObject> morganisFTiltHitboxes;
    public List<GameObject> morganisFAirHitboxes;

    public List<GameObject> morganisUpBHitboxes;
    public List<GameObject> exampleProjectile;

    //Populates the Game's data using info/functions from GameData;
    void Start () {
        LoadResources();

        hitmanager = GetComponent<HitManager>();
        stagebuilder = GetComponent<StageBuilder>();

        player1 = SpawnExamplePlayer();
        player1.playerNum = 1;
        player2 = SpawnMorganis(new Vector3(5, -2.25F));
        player2.playerNum = 2;

        PlayerAttackManager.nullAttackLists.Add(player1.nullify);
        PlayerAttackManager.nullAttackLists.Add(player2.nullify);

        GameObject HUI = Instantiate(healthUI);

        GameObject damageCounter1 = GameObject.Find("Player1Health");
        GameObject damageCounter2 = GameObject.Find("Player2Health");

        damageCounter1.GetComponent<DamageCounter>().damageSource = player1;
        damageCounter2.GetComponent<DamageCounter>().damageSource = player2;

        stagebuilder.ConstructFlat();
    }

    void LoadResources()
    {
        GameData.hitboxRenderPrefab = (GameObject)Resources.Load("Prefabs/HitboxRenderPrefab");

    }

    public static Material SetAlpha(Material material, float value)
    {
        Color color = material.color;
        color.a = value;
        material.color = color;

        return material;
    }

    //Instantiates an example player and populates their attacks/animations
    PlayerController SpawnExamplePlayer(Vector3 spawnPosition = default(Vector3))
    {
        PlayerController player;
        if (spawnPosition == default(Vector3))
        {
            player = Instantiate(playerPrefab).GetComponent<PlayerController>();
        } else
        {
            player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity, null).GetComponent<PlayerController>();
        }
        

        player.jabAttack = GameData.CreateExampleJabAttack(jabHitboxes);
        player.fTiltAttack = GameData.CreateExampleFTiltAttack(fTiltHitboxes);
        player.uTiltAttack = GameData.CreateExampleUTiltAttack(uTiltHitboxes);
        player.dTiltAttack = GameData.CreateExampleDTiltAttack(dTiltHitboxes);

        player.nAirAttack = GameData.CreateExampleNAirAttack(nAirHitboxes);
        player.dAirAttack = GameData.CreateExampleDAirAttack(dAirHitboxes);
        player.uAirAttack = GameData.CreateExampleUAirAttack(uAirHitboxes);
        player.fAirAttack = GameData.CreateExampleFAirAttack(fAirHitboxes);
        player.bAirAttack = GameData.CreateExampleBAirAttack(bAirHitboxes);

        player.upBAttack = GameData.CreateMorganisUpBAttack(morganisUpBHitboxes);
        player.nBAttack = GameData.CreateExampleProjectileAttack(exampleProjectile);

        player.standing = marshmallowStand;
        player.hitstunned = marshmallowHitstun;

        return player;
    }

    //Instantiates a Morganis player
    PlayerController SpawnMorganis(Vector3 spawnPosition = default(Vector3))
    {
        MorganisController player;
        if (spawnPosition == default(Vector3))
        {
            player = Instantiate(morganisPrefab).GetComponent<MorganisController>();
        }
        else
        {
            player = Instantiate(morganisPrefab, spawnPosition, Quaternion.identity, null).GetComponent<MorganisController>();
        }


        player.jabAttack = GameData.CreateExampleJabAttack(jabHitboxes);
        player.fTiltAttack = GameData.CreateMorganisFTiltAttack(morganisFTiltHitboxes);
        player.uTiltAttack = GameData.CreateExampleUTiltAttack(uTiltHitboxes);
        player.dTiltAttack = GameData.CreateExampleDTiltAttack(dTiltHitboxes);

        player.nAirAttack = GameData.CreateExampleNAirAttack(nAirHitboxes);
        player.dAirAttack = GameData.CreateExampleDAirAttack(dAirHitboxes);
        player.uAirAttack = GameData.CreateExampleUAirAttack(uAirHitboxes);
        player.fAirAttack = GameData.CreateMorganisFAirAttack(morganisFAirHitboxes);
        player.bAirAttack = GameData.CreateExampleBAirAttack(bAirHitboxes);

        player.upBAttack = GameData.CreateMorganisUpBAttack(morganisUpBHitboxes);
        player.nBAttack = GameData.CreateExampleProjectileAttack(exampleProjectile);

        player.standing = marshmallowStand;
        player.hitstunned = marshmallowHitstun;

        return player;
    }
}
