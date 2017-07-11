using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    //input variables for joystick 1
    public static float hori;
    public static float vert;

    public static float horiThreshold = 0.8F;
    public static float vertThreshold = 0.8F;

    public static float cHori;
    public static float cVert;

    public static bool onStickDown;
    public static bool onStick;
    public static bool onStickHold;
    public static float lastStickY;
    public static float lastStickX;

    public static bool onCStick;
    public static bool onCStickHold;
    public static float lastCStickY;
    public static float lastCStickX;

    public static bool aPress;
    public static bool bPress;
    public static bool xPress;
    public static bool yPress;
    public static bool zPress;

    public static bool rPress;
    public static bool lPress;

    //press/hold detection
    public static bool aHold = false;
    public static bool lastA = false;

    public static bool bHold = false;
    public static bool lastB = false;

    public static bool xHold = false;
    public static bool lastX = false;

    public static bool yHold = false;
    public static bool lastY = false;

    public static bool zHold = false;
    public static bool lastZ = false;

    public static bool rHold = false;
    public static bool lastR = false;

    public static bool lHold = false;
    public static bool lastL = false;

    /* ------------------------------------------------------------------------------------------------------------------------*/

    //input variables for joystick 1
    public static float hori2;
    public static float vert2;

    public static float cHori2;
    public static float cVert2;

    public static bool onStickDown2;
    public static bool onStick2;
    public static bool onStickHold2;
    public static float lastStickY2;
    public static float lastStickX2;

    public static bool onCStick2;
    public static bool onCStickHold2;
    public static float lastCStickY2;
    public static float lastCStickX2;

    public static bool aPress2;
    public static bool bPress2;
    public static bool xPress2;
    public static bool yPress2;
    public static bool zPress2;

    public static bool rPress2;
    public static bool lPress2;


    //press/hold detection
    public static bool aHold2 = false;
    public static bool lastA2 = false;

    public static bool bHold2 = false;
    public static bool lastB2 = false;

    public static bool xHold2 = false;
    public static bool lastX2 = false;

    public static bool yHold2 = false;
    public static bool lastY2 = false;

    public static bool zHold2 = false;
    public static bool lastZ2 = false;

    public static bool rHold2 = false;
    public static bool lastR2 = false;

    public static bool lHold2 = false;
    public static bool lastL2 = false;


    void Update()
    {
        switch (GameLoader.playerCount)
        {
            case 1:
                CheckInputs1();
                CheckSticksDown1();

                break;
            case 2:
                CheckInputs1();
                CheckSticksDown1();

                CheckInputs2();
                CheckSticksDown2();

                break;
        }
    }

    void CheckInputs1() {

        //axis values
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        cHori = Input.GetAxis("CHorizontal");
        cVert = Input.GetAxis("CVertical");

        //button values
        if (Input.GetAxis("AButton") != 0)
        {
            aHold = true;
            aPress = !lastA;
            lastA = aHold;

        }
        else
        {
            aHold = false;
            aPress = false;
            lastA = false;
        }

        if (Input.GetAxis("BButton") != 0)
        {
            bHold = true;
            bPress = !lastB;
            lastB = bHold;
        }
        else
        {
            bHold = false;
            bPress = false;
            lastB = false;
        }

        if (Input.GetAxis("XButton") != 0)
        {
            xHold = true;
            xPress = !lastX;
            lastX = xHold;
        }
        else
        {
            xHold = false;
            xPress = false;
            lastX = false;
        }

        if (Input.GetAxis("YButton") != 0)
        {
            yHold = true;
            yPress = !lastY;
            lastY = yHold;
        }
        else
        {
            yHold = false;
            yPress = false;
            lastY = false;
        }

        if (Input.GetAxis("ZButton") != 0)
        {
            zHold = true;
            zPress = !lastZ;
            lastZ = zHold;
        }
        else
        {
            zHold = false;
            zPress = false;
            lastZ = false;
        }

        if (Input.GetAxis("RButton") != 0)
        {
            rHold = true;
            rPress = !lastR;
            lastR = rHold;
        }
        else
        {
            rHold = false;
            rPress = false;
            lastR = false;
        }

        if (Input.GetAxis("LButton") != 0)
        {
            lHold = true;
            lPress = !lastL;
            lastL = lHold;
        }
        else
        {
            lHold = false;
            lPress = false;
            lastL = false;
        }

    }

    void CheckInputs2()
    {
        //axis values
        hori2 = Input.GetAxis("Horizontal2");
        vert2 = Input.GetAxis("Vertical2");
        cHori2 = Input.GetAxis("CHorizontal2");
        cVert2 = Input.GetAxis("CVertical2");

        //button values
        if (Input.GetAxis("AButton2") != 0)
        {
            aHold2 = true;
            aPress2 = !lastA2;
            lastA2 = aHold2;

        }
        else
        {
            aHold2 = false;
            aPress2 = false;
            lastA2 = false;
        }

        if (Input.GetAxis("BButton2") != 0)
        {
            bHold2 = true;
            bPress2 = !lastB2;
            lastB2 = bHold2;
        }
        else
        {
            bHold2 = false;
            bPress2 = false;
            lastB2 = false;
        }

        if (Input.GetAxis("XButton2") != 0)
        {
            xHold2 = true;
            xPress2 = !lastX2;
            lastX2 = xHold2;
        }
        else
        {
            xHold2 = false;
            xPress2 = false;
            lastX2 = false;
        }

        if (Input.GetAxis("YButton2") != 0)
        {
            yHold2 = true;
            yPress2 = !lastY2;
            lastY2 = yHold2;
        }
        else
        {
            yHold2 = false;
            yPress2 = false;
            lastY2 = false;
        }

        if (Input.GetAxis("ZButton2") != 0)
        {
            zHold2 = true;
            zPress2 = !lastZ2;
            lastZ2 = zHold2;
        }
        else
        {
            zHold2 = false;
            zPress2 = false;
            lastZ2 = false;
        }
        if (Input.GetAxis("RButton2") != 0)
        {
            rHold2 = true;
            rPress2 = !lastR2;
            lastR2 = rHold2;
        }
        else
        {
            rHold2 = false;
            rPress2 = false;
            lastR2 = false;
        }

        if (Input.GetAxis("LButton2") != 0)
        {
            lHold2 = true;
            lPress2 = !lastL2;
            lastL2 = lHold2;
        }
        else
        {
            lHold2 = false;
            lPress2 = false;
            lastL2 = false;
        }

    }

    //TODO: Check if this works, and then copy it to CheckSticksDown2();
    void CheckSticksDown1()
    {
        //determines if the stick was just pressed down (for fastfalling/CStick inputs)
        if (vert <= -vertThreshold && lastStickY > -0.7F && onStickDown == false)
        {
            onStickDown = true;
        }
        else
        {
            onStickDown = false;
        }

        //determines if this is the first frame on which the stick is not neutral
        if (Mathf.Abs(vert) >= vertThreshold || (Mathf.Abs(hori) >= horiThreshold))
        {
            onStickHold = true;
            if (Mathf.Abs(lastStickX) <= 0.7F && Mathf.Abs(lastStickY) <= 0.7F)
            {
                onStick = true;
            } else
            {
                onStick = false;
            }
        } else
        {
            onStickHold = false;
            onStick = false;
        }

        lastStickY = vert;
        lastStickX = hori;

        //same for CStick
       if (Mathf.Abs(cVert) >= vertThreshold || (Mathf.Abs(cHori) >= horiThreshold))
        {
            onCStickHold = true;
            if (Mathf.Abs(lastCStickY) <= vertThreshold && Mathf.Abs(lastCStickX) <= horiThreshold)
            {
                onCStick = true;
            }
            else
            {
                onCStick = false;
            }
        }
        else
        {
            onCStickHold = false;
            onCStick = false;
        }

        lastCStickY = cVert;
        lastCStickX = cHori;
    }

    void CheckSticksDown2()
    {
        //determines if the stick was just pressed down (for fastfalling/CStick inputs)
        if (vert2 <= -vertThreshold && lastStickY2 > -0.7F && onStickDown2 == false)
        {
            onStickDown2 = true;
        }
        else
        {
            onStickDown2 = false;
        }

        //determines if this is the first frame on which the stick is not neutral
        if (Mathf.Abs(vert2) >= vertThreshold || (Mathf.Abs(hori2) >= horiThreshold))
        {
            onStickHold2 = true;
            if (Mathf.Abs(lastStickX2) <= 0.7F && Mathf.Abs(lastStickY2) <= 0.7F)
            {
                onStick2 = true;
            }
            else
            {
                onStick2 = false;
            }
        }
        else
        {
            onStickHold2 = false;
            onStick2 = false;
        }

        lastStickY2 = vert2;
        lastStickX2 = hori2;


        //same for CStick
        if (Mathf.Abs(cVert2) >= vertThreshold || (Mathf.Abs(cHori2) >= horiThreshold))
        {
            onCStickHold2 = true;
            if (Mathf.Abs(lastCStickX2) <= 0.7F && Mathf.Abs(lastCStickY2) <= 0.7F)
            {
                onCStick2 = true;
            }
            else
            {
                onCStick2 = false;
            }
        }
        else
        {
            onCStickHold2 = false;
            onCStick2 = false;
        }

        lastCStickY2 = cVert2;
        lastCStickX2 = cHori2;
    }
}
