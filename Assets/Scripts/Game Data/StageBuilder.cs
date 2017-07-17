using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour {

    public Material platMat;

    static float flatStageHeight = 26;
    static float flatStageBottom = -19;
    static float flatStageWidth = 31;

    public void ConstructFlat()
    {
        GameObject plat1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plat1.tag = "Stage";
        plat1.layer = 10;
        plat1.transform.position = new Vector3(0, -5, 0);
        plat1.transform.localScale = new Vector3(30, 2, 1);
        plat1.GetComponent<Renderer>().material = platMat;

        //TODO: add formula based on player size for ledgegrabposition

        LedgeNode ledge1 = Instantiate(GameData.ledgePrefab).GetComponent<LedgeNode>();
        ledge1.ledgePosition = new Vector3(15.25F, -5F);
        ledge1.ledgeGrabPosition = ledge1.ledgePosition + (Vector3.right * 0.5F);
        ledge1.ledgeGrabLeft = false;

        LedgeNode ledge2 = Instantiate(GameData.ledgePrefab).GetComponent<LedgeNode>();
        ledge2.ledgePosition = new Vector3(-15.25F, -5F);
        ledge2.ledgeGrabPosition = ledge2.ledgePosition + (Vector3.left * 0.5F);
        ledge2.ledgeGrabLeft = true;



        StageData.activePlatforms.Add(new Platform(0, plat1));

        GameData.stageHeight = flatStageHeight;
        GameData.stageBottom = flatStageBottom;
        GameData.stageWidth = flatStageWidth;
    }

}
