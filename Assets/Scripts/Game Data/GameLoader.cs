using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject HealthUI;

    StageBuilder stagebuilder;

    public static HitManager hitmanager;

    public static int playerCount = 2;

    public PlayerController player1;
    public PlayerController player2;

    public Material marshmallowStand;
    public Material marshmallowHitstun;

    public List<GameObject> jabHitboxes;
    public List<GameObject> fTiltHitboxes;
    public List<GameObject> uTiltHitboxes;
    public List<GameObject> dTiltHitboxes;

    public List<GameObject> nAirHitboxes;
    public List<GameObject> dAirHitboxes;
    public List<GameObject> uAirHitboxes;
    public List<GameObject> fAirHitboxes;
    public List<GameObject> bAirHitboxes;

    //Populates the Game's data using info/functions from GameData;
    void Start () {
        hitmanager = GetComponent<HitManager>();
        stagebuilder = GetComponent<StageBuilder>();

        player1 = Instantiate(playerPrefab).GetComponent<PlayerController>();
        player1.playerNum = 1;
        player2 = Instantiate(playerPrefab, new Vector3(5, -2.25F), Quaternion.identity, null).GetComponent<PlayerController>();
        player2.playerNum = 2;

        PlayerAttackManager.nullAttackLists.Add(player1.nullify);
        PlayerAttackManager.nullAttackLists.Add(player2.nullify);

        player1.jabAttack = GameData.CreateExampleJabAttack(jabHitboxes);
        player1.fTiltAttack = GameData.CreateExampleFTiltAttack(fTiltHitboxes);
        player1.uTiltAttack = GameData.CreateExampleUTiltAttack(uTiltHitboxes);
        player1.dTiltAttack = GameData.CreateExampleDTiltAttack(dTiltHitboxes);

        player1.nAirAttack = GameData.CreateExampleNAirAttack(nAirHitboxes);
        player1.dAirAttack = GameData.CreateExampleDAirAttack(dAirHitboxes);
        player1.uAirAttack = GameData.CreateExampleUAirAttack(uAirHitboxes);
        player1.fAirAttack = GameData.CreateExampleFAirAttack(fAirHitboxes);
        player1.bAirAttack = GameData.CreateExampleBAirAttack(bAirHitboxes);

        player1.standing = marshmallowStand;
        player1.hitstunned = marshmallowHitstun;


        player2.jabAttack = GameData.CreateExampleJabAttack(jabHitboxes);
        player2.fTiltAttack = GameData.CreateExampleFTiltAttack(fTiltHitboxes);
        player2.uTiltAttack = GameData.CreateExampleUTiltAttack(uTiltHitboxes);
        player2.dTiltAttack = GameData.CreateExampleDTiltAttack(dTiltHitboxes);

        player2.nAirAttack = GameData.CreateExampleNAirAttack(nAirHitboxes);
        player2.dAirAttack = GameData.CreateExampleDAirAttack(dAirHitboxes);
        player2.uAirAttack = GameData.CreateExampleUAirAttack(uAirHitboxes);    
        player2.fAirAttack = GameData.CreateExampleFAirAttack(fAirHitboxes);
        player2.bAirAttack = GameData.CreateExampleBAirAttack(bAirHitboxes);

        player2.standing = marshmallowStand;
        player2.hitstunned = marshmallowHitstun;


        GameObject HUI = Instantiate(HealthUI);

        GameObject damageCounter1 = GameObject.Find("Player1Health");
        GameObject damageCounter2 = GameObject.Find("Player2Health");

        damageCounter1.GetComponent<DamageCounter>().damageSource = player1;
        damageCounter2.GetComponent<DamageCounter>().damageSource = player2;

        stagebuilder.ConstructFlat();
    }
}
