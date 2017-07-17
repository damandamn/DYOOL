using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCollision : MonoBehaviour {

    public PlayerController user;

    //0 top, 1 left, 2 right
    public int directionID = 0;
    int modifiedID = 0;

    private void Update()
    {
        StartCoroutine(CancelBools());
        if (directionID == 1)
        {
            if (user.isFacingLeft) {
                modifiedID = 2;
            } else
            {
                modifiedID = 1;
            }
        }
        else
        if (directionID == 2)
        {
            if (user.isFacingLeft)
            {
                modifiedID = 1;
            }
            else
            {
                modifiedID = 2;
            }
        }
        else
        {
            modifiedID = directionID;
        }
    }

    private void OnTriggerStay(Collider other)
    { 
        if (other.gameObject.tag == "Stage")
        {
            switch (modifiedID)
            {
                case 0:
                    user.topColl = true;

                    if (user.airMomentum.y > 0)
                    {
                        //user.airMomentum.y = 0;
                    }
                    if (user.knockbackMomentum.y > 0)
                    {
                        user.knockbackMomentum.y = 0;
                    }

                    user.gameObject.transform.position -= new Vector3(0, GameData.ClampFloat(user.airMomentum.y + user.knockbackMomentum.y));

                    break;

                case 1:
                    user.leftColl = true;

                    if (user.airMomentum.x < 0)
                    {
                        user.airMomentum.x = 0;
                    }
                    if (user.groundMomentum.x < 0)
                    {
                        user.groundMomentum.x = 0;
                    }
                    if (user.knockbackMomentum.x < 0)
                    {
                        user.knockbackMomentum.x *= -1;
                    }

                    user.gameObject.transform.position += new Vector3(GameData.ClampFloat(user.airMomentum.x + user.knockbackMomentum.x + user.groundMomentum.x, true),0);

                    break;

                case 2:
                    user.rightColl = true;

                    if (user.airMomentum.x > 0)
                    {
                        user.airMomentum.x = 0;
                    }
                    if (user.groundMomentum.x > 0)
                    {
                        user.groundMomentum.x = 0;
                    }
                    if (user.knockbackMomentum.x > 0)
                    {
                        user.knockbackMomentum.x *= -1;
                    }

                    user.gameObject.transform.position -= new Vector3(GameData.ClampFloat(user.airMomentum.x + user.knockbackMomentum.x + user.groundMomentum.x), 0);

                    break;


            }
        }
    }

    IEnumerator CancelBools()
    {
        yield return new WaitForEndOfFrame();

        user.topColl = false;
        user.leftColl = false;
        user.rightColl = false;
    }
}
