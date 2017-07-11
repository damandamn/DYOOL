using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour {

    public Material platMat;

    static float flatStageHeight = 25;
    static float flatStageBottom = -18;
    static float flatStageWidth = 28;

    public void ConstructFlat()
    {
        GameObject plat1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plat1.tag = "Stage";
        plat1.layer = 10;
        plat1.transform.position = new Vector3(0, -5, 0);
        plat1.transform.localScale = new Vector3(30, 2, 1);
        plat1.GetComponent<Renderer>().material = platMat;


        StageData.activePlatforms.Add(new Platform(-2.25F, plat1));

        GameData.stageHeight = flatStageHeight;
        GameData.stageBottom = flatStageBottom;
        GameData.stageWidth = flatStageWidth;
    }

    public void ConstructTower()
    {
        GameObject plat1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject plat2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject plat3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plat1.tag = "Stage";
        plat1.layer = 10;
        plat1.transform.position = new Vector3(0, -5, 0);
        plat1.transform.localScale = new Vector3(30, 2, 1);
        plat1.GetComponent<Renderer>().material = platMat;

        plat2.tag = "Stage";
        plat2.layer = 10;
        plat2.transform.position = new Vector3(-8, 1, 0);
        plat2.transform.localScale = new Vector3(7, 1, 1);
        plat2.GetComponent<Renderer>().material = platMat;

        plat3.tag = "Stage";
        plat3.layer = 10;
        plat3.transform.position = new Vector3(8, 1, 0);
        plat3.transform.localScale = new Vector3(7, 1, 1);
        plat3.GetComponent<Renderer>().material = platMat;


        StageData.activePlatforms.Add(new Platform(-2.25F, plat1));
        StageData.activePlatforms.Add(new Platform(3.25F, plat2));
        StageData.activePlatforms.Add(new Platform(3.25F, plat3));

        GameData.stageHeight = flatStageHeight;
        GameData.stageBottom = flatStageBottom;
        GameData.stageWidth = flatStageWidth;
    }

}
