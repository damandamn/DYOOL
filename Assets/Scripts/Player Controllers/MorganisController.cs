using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorganisController : PlayerController {

    protected float morganisWalkSpeed = 0.12F;
    protected float morganisTraction = 0.01F;
    protected float morganisWalkAccel = 0.012F;
    protected float morganisDashAccel = 0.012F;

    protected float morganisMaxAirSpeed = 0.065F;
    protected float morganisAirAccel = 0.005F;
    protected float morganisAirDeccel = 0.0035F;
    protected float morganisJumpMomentum = 0.35F;
    protected float morganisFallSpeed = 0.012F;

    protected float morganisBaseMaxFallSpeed = 0.25F;

    protected int morganisJumpCount = 1;

    protected int morganisJumpFrames = 4;
    protected int morganisTurnFrames = 2;

    private void Start()
    {
        pgh = GetComponent<PlayerGroundHandler>();
        pam = GetComponent<PlayerAttackManager>();
        render = GetComponent<Renderer>();

        //Sets PlayerController variables to character specifics
        walkSpeed = morganisWalkSpeed;
        traction = morganisTraction;
        walkAccel = morganisWalkAccel;
        dashAccel = morganisDashAccel;

        maxAirSpeed = morganisMaxAirSpeed;
        airAccel = morganisAirAccel;
        airDeccel = morganisAirDeccel;
        jumpMomentum = morganisJumpMomentum;
        fallSpeed = morganisFallSpeed;

        baseMaxFallSpeed = morganisBaseMaxFallSpeed;

        jumpCount = morganisJumpCount;

        jumpFrames = morganisJumpFrames;
        turnFrames = morganisTurnFrames;
}

    public override IEnumerator Hitlag(int duration, float knockbackValue, int hitstun, float angle, PlayerController sender = null, bool attacker = false)
    {
        if (frameCancel)
        {
            yield break;
        }

        if (!attacker)
        {
            upBUsed = 0;
        }

        //remembers whether the attacker was using an aerial or not
        bool aerialReturn = false;
        if (attacker)
        {
            if (moveState == MoveStates.AERIAL)
            {
                aerialReturn = true;
            }
        }
        else
        {
            InterruptAttack();
        }
        moveState = MoveStates.HITLAG;

        for (int i = 0; i < duration; i++)
        {
            yield return null;
        }

        if (attacker)
        {
            if (currAttack != null)
            {
                if (aerialReturn)
                {
                    moveState = MoveStates.AERIAL;
                }
                else
                {
                    moveState = MoveStates.ATTACK;
                }
            }
        }
        else
        {
            //begin launch
            knockbackMomentum = GameLoader.hitmanager.CalculateLaunch(knockbackValue, angle, fallSpeed, sender);
            hitStunDuration = hitstun;

            inHitstun = false;
            moveState = MoveStates.HITSTUN;
        }
    }
}
