using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour {

    public Material platMat;

    static float flatStageHeight = 25;
    static float flatStageBottom = -18;
    static float flatStageWidth = 27;

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

}
