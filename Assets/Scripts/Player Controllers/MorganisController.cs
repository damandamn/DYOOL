using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorganisController : PlayerController {

    protected float morganisWalkSpeed = 0.12F;
    protected float morganisTraction = 0.01F;
    protected float morganisWalkAccel = 0.012F;
    protected float morganisDashAccel = 0.012F;

    protected float morganisMaxAirSpeed = 0.065F;
    protected float morganisAirAccel = 0.006F;
    protected float morganisAirDeccel = 0.004F;
    protected float morganisJumpMomentum = 0.33F;
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

}
