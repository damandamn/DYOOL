using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferManager {

    public static Attack BufferAOption(PlayerController user, bool isCStick = false)
    {
        if (user.moveState == PlayerController.MoveStates.JUMPSQUAT)
        {
            return BufferAerialOption(user, isCStick);
        } else
        {

        }


        return null;
    }

    public static Attack BufferAerialOption(PlayerController user, bool isCStick)
    {
        if (isCStick)
        {
            //CStick is diagonal
            if (Mathf.Abs(user.cHori) >= user.horiThreshold && Mathf.Abs(user.cVert) >= user.vertThreshold)
            {
                return user.nAirAttack;
            }
            else
            //CStick is down
            if (user.cVert <= -user.vertThreshold)
            {
                return user.dAirAttack;
            }
            else
            //CStick is up
            if (user.cVert >= user.vertThreshold)
            {
                return user.uAirAttack;
            }
            else
            //CStick is to the right
            if (user.cHori >= user.horiThreshold)
            {
                if (!user.isFacingLeft)
                {
                    return user.fAirAttack;
                }
                else
                {
                    return user.bAirAttack;
                }
            }
            else
            //CStick is to the left
            if (user.cHori <= -user.horiThreshold)
            {
                if (user.isFacingLeft)
                {
                    return user.fAirAttack;
                }
                else
                {
                    return user.bAirAttack;
                }
            }
        }
        else
        {
            //Stick is diagonally up
            if (Mathf.Abs(user.hori) > Mathf.Abs(user.horiThreshold) && user.vert > user.vertThreshold)
            {
                if (Mathf.Abs(user.vert) >= Mathf.Abs(user.hori))
                {
                    return user.uAirAttack;
                }
                else
                {
                    if (user.hori > user.horiThreshold)
                    {
                        if (!user.isFacingLeft)
                        {
                            return user.fAirAttack;
                        }
                        else
                        {
                            return user.bAirAttack;
                        }
                    }
                    else
                    if (user.hori < -user.horiThreshold)
                    {
                        if (user.isFacingLeft)
                        {
                            return user.fAirAttack;
                        }
                        else
                        {
                            return user.bAirAttack;
                        }
                    }
                }
            }
            else
            //Stick is diagonally down
            if (Mathf.Abs(user.hori) > Mathf.Abs(user.horiThreshold) && user.vert < -user.vertThreshold)
            {
                if (Mathf.Abs(user.vert) >= Mathf.Abs(user.hori))
                {
                    return user.dAirAttack;
                }
                else
                {
                    if (user.hori > user.horiThreshold)
                    {
                        if (!user.isFacingLeft)
                        {
                            return user.fAirAttack;
                        }
                        else
                        {
                            return user.bAirAttack;
                        }
                    }
                    else
                    if (user.hori < -user.horiThreshold)
                    {
                        if (user.isFacingLeft)
                        {
                            return user.fAirAttack;
                        }
                        else
                        {
                            return user.bAirAttack;
                        }
                    }
                }
            }
            else
            if (Mathf.Abs(user.hori) < user.horiThreshold && Mathf.Abs(user.vert) < user.vertThreshold)
            {
                return user.nAirAttack;
            }
            else
            if (Mathf.Abs(user.hori) < user.horiThreshold && user.vert < -user.vertThreshold)
            {
                return user.dAirAttack;
            }
            else
            if (Mathf.Abs(user.hori) < user.horiThreshold && user.vert > user.vertThreshold)
            {
                return user.uAirAttack;
            }
            else
            if (user.hori > user.horiThreshold && Mathf.Abs(user.vert) < user.vertThreshold)
            {
                if (!user.isFacingLeft)
                {
                    return user.fAirAttack;
                }
                else
                {
                    return user.bAirAttack;
                }
            }
            else
            if (user.hori < -user.horiThreshold && Mathf.Abs(user.vert) < user.vertThreshold)
            {
                if (user.isFacingLeft)
                {
                    return user.fAirAttack;
                }
                else
                {
                    return user.bAirAttack;
                }
            }
        }
        return null;
    }

    public static Attack BufferBOption(PlayerController user)
    {


        return null;
    }
}
