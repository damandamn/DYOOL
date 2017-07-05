using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public enum MoveStates
    {
        STILL,
        LEFTWALK,
        RIGHTWALK,
        TURN,
        JUMPSQUAT,
        ATTACK,
        AERIAL,
        SPECIALMOVE,
        LANDINGLAG,
        HITLAG,
        HITSTUN,
        AIRBORNE,
        SPECIALFALL
    }

    public PlayerGroundHandler pgh;
    public PlayerAttackManager pam;
    public Renderer render;

    public int playerNum = 1;
    public float currDamage = 0;
    public int weight = 100;

    //input variables
    public float hori;
    public float vert;

    public float horiThreshold = 0.6F;
    public float vertThreshold = 0.8F;

    public float cHori;
    public float cVert;

    public bool aPress;
    public bool bPress;
    public bool xPress;
    public bool yPress;
    public bool zPress;

    //press/hold detection variables
    public bool aHold = false;
    public bool lastA = false;

    public bool bHold = false;
    public bool lastB = false;

    public bool xHold = false;
    public bool lastX = false;

    public bool yHold = false;
    public bool lastY = false;

    public bool zHold = false;
    public bool lastZ = false;

    //used to determine if the stick was just pressed down (for fastfalling)
    public bool onStickDown = false;
    public bool onStick = false;
    public float lastStickY = 0;
    public float lastStickX = 0;

    public bool onCStick = false;
    public float lastCStickY = 0;
    public float lastCStickX = 0;

    //Character state variables
    public MoveStates moveState = MoveStates.STILL;
    protected float groundLevel = -2.25F;

    //Character Attributes! These all change from character to character
    protected float walkSpeed = 0.1F;
    protected float traction = 0.01F;
    protected float walkAccel = 0.01F;
    protected float dashAccel = 0.01F;

    protected float maxAirSpeed = 0.08F;
    protected float airAccel = 0.0065F;
    protected float airDeccel = 0.0042F ;
    protected float jumpMomentum = 0.3F;
    protected float fallSpeed = 0.0095F;

    protected float baseMaxFallSpeed = 0.2F;
    float maxFallSpeed;

    protected int jumpCount = 1;
    int jumpUsed = 0;

    protected int jumpFrames = 4;
    protected int turnFrames = 2;

    int lagCount = 0;

    public Vector3 airMomentum = new Vector3(0, 0);
    public Vector3 groundMomentum = new Vector3(0, 0);

    //hitstun variables
    public Vector3 knockbackMomentum = new Vector3(0, 0);
    public List<Attack> nullify = new List<Attack>();
    public int hitStunDuration = 0;
    protected bool inHitstun = false;


    public bool frameCancel = false;

    public bool isGrounded = true;
    public bool fastFalling = false;
    public bool isFacingLeft = false;

    public Attack currAttack = null;
    public int currAttackFrame = 0;

    //List of all of a characters attacks
    public Attack jabAttack = null;
    public Attack fTiltAttack = null;
    public Attack uTiltAttack = null;
    public Attack dTiltAttack = null;
    public Attack nAirAttack = null;
    public Attack dAirAttack = null;
    public Attack uAirAttack = null;
    public Attack fAirAttack = null;
    public Attack bAirAttack = null;

    public Attack upBAttack = null;
    public int upBUsed = 0;
    public Attack nBAttack = null;

    //Animations
    public Material standing;
    public Material hitstunned;

    void Start()
    {
        pgh = GetComponent<PlayerGroundHandler>();
        pam = GetComponent<PlayerAttackManager>();
        render = GetComponent<Renderer>();
    }

    //Update is called once per frame
    void Update() {

        if (CheckBlastZones())
        {
            Death();
        }

        //checks at the beginning and end of Update()
        pgh.UpdatePlatform();
        groundLevel = pgh.platHeight;

        if (frameCancel == true)
        {
            frameCancel = false;
        }

        GetInputs();
        CheckFastFall();

        //flips character model when facing left
        if (isFacingLeft)
        {
            transform.localScale = new Vector3(-1.5F, 3.5F, 1);
        } else
        {
            transform.localScale = new Vector3(1.5F, 3.5F, 1);
        }

        //Checks Grounded state
        if (transform.position.y > groundLevel)
        {
            groundMomentum.x = 0;
            if (moveState == MoveStates.STILL || moveState == MoveStates.LEFTWALK || moveState == MoveStates.RIGHTWALK)
            {
                moveState = MoveStates.AIRBORNE;
            }
            isGrounded = false;
        }
        else
        {
            airMomentum = Vector3.zero;
            if (isGrounded == false && airMomentum.y <= 0)
            {
                Land();
            }
        }

        //MOVESTATE = HITLAG
        if (moveState == MoveStates.HITLAG)
        {
            if (isGrounded)
            {
                transform.position += new Vector3(Random.Range(-0.02F, 0.02F), transform.position.y);
            }
            else
            {
                transform.position += new Vector3(Random.Range(-0.02F, 0.02F), Random.Range(-0.02F, 0.02F));
            }
            PreventClipping();
        }

        //MOVESTATE = HITSTUN
        else if (moveState == MoveStates.HITSTUN)
        {
            if (!inHitstun)
            {
                try
                {
                    StopCoroutine(HitManager.playersInHitstun[this]);
                    HitManager.playersInHitstun[this] = null;
                } catch { }

                HitManager.playersInHitstun[this] = Hitstun();
                inHitstun = true;
                StartCoroutine(HitManager.playersInHitstun[this]);
            }
            HitStunControl();
        }

        //MOVESTATE = JUMPSQUAT, LANDINGLAG, OR TURN
        else if (moveState == MoveStates.JUMPSQUAT || moveState == MoveStates.TURN || moveState == MoveStates.LANDINGLAG)
        {
            ApplyTraction();
            if (lagCount > 1)
            {
                lagCount--;

            } else
            {
                if (moveState == MoveStates.JUMPSQUAT)
                {
                    Jump();

                } else {
                    moveState = MoveStates.STILL;

                }
            }
        }

        //MOVESTATE = ATTACK
        else if (moveState == MoveStates.ATTACK)
        {
            pam.RunAttackFrame(this, currAttack, currAttackFrame);
            currAttackFrame++;
            if (groundMomentum.x < 0)
            {
                groundMomentum.x += traction;
                if (groundMomentum.x > 0)
                {
                    groundMomentum.x = 0;
                }
            }

            if (groundMomentum.x > 0)
            {
                groundMomentum.x -= traction;
                if (groundMomentum.x < 0)
                {
                    groundMomentum.x = 0;
                }
            }
        }

        //MOVESTATE = AERIAL
        else if (moveState == MoveStates.AERIAL) {
            AerialAttackControl();
        }
        else if (moveState == MoveStates.SPECIALMOVE)
        {
            SpecialMoveControl();
        }
        //MOVESTATE = AIRBORNE, STILL, LEFTWALK, RIGHTWALK
        else
        { 
            //if the player is grounded, utilize grounded controls
            if (isGrounded)
            {
                moveState = MoveStates.STILL;
                GroundControl();

                //Change ground momentum per frame
                if (moveState == MoveStates.RIGHTWALK)
                {
                    if (groundMomentum.x > -walkSpeed)
                    {
                        groundMomentum.x -= walkAccel;
                    }

                    if (groundMomentum.x < - walkSpeed)
                    {
                        groundMomentum.x += walkAccel;
                    }

                }
                else if (moveState == MoveStates.LEFTWALK)
                {
                    if (groundMomentum.x < walkSpeed)
                    {
                        groundMomentum.x += walkAccel;
                    }

                    if (groundMomentum.x > walkSpeed)
                    {
                        groundMomentum.x -= walkAccel;
                    }
                }
                else if (moveState == MoveStates.STILL)
                {
                    ApplyTraction();
                }
            } else
            {
                AirControl();
            }

        }

        if (moveState != MoveStates.HITLAG && moveState != MoveStates.HITSTUN)
        {
            if (isGrounded)
            {
                transform.Translate(groundMomentum);
            }
            else
            {
                if (moveState == MoveStates.SPECIALMOVE)
                {
                    //only translates x or y depending on move properties
                    if (currAttack.frameData[currAttackFrame].canControl)
                    {
                        transform.Translate(new Vector3(airMomentum.x, 0));
                    }
                    if (currAttack.frameData[currAttackFrame].canFall)
                    {
                        transform.Translate(new Vector3(0, airMomentum.y));
                    }
                    if (PreventClipping())
                    {
                        Land();
                    }
                }
                else
                {
                    transform.Translate(airMomentum);
                    if (PreventClipping())
                    {
                        Land();
                    }
                }
            }
        }

        //updates platform again to allow for sliding off edges
        pgh.UpdatePlatform();
        groundLevel = pgh.platHeight;

        //Allows for sliding off edges to keep momentum/cancel lag
        if (transform.position.y > groundLevel)
        {
            //works in MoveStates ATTACK, LANDINGLAG, LEFTWALK, RIGHTWALK, STILL
            if (isGrounded && (moveState == MoveStates.ATTACK || moveState == MoveStates.LANDINGLAG || moveState == MoveStates.LEFTWALK 
                || moveState == MoveStates.RIGHTWALK || moveState == MoveStates.STILL))
            {
              
                airMomentum = groundMomentum;

                moveState = MoveStates.AIRBORNE;
                isGrounded = false;

                if (currAttack != null)
                {
                    InterruptAttack();
                }
            }
        }


        CheckAnimation();
    }

    //Called every frame when the player is grounded and can act
    void GroundControl()
    {
        if (yPress || xPress)
        {
            StartJumpsquat();
        }
        else
        if (bPress)
        {
            pam.UseSpecialAttack(this);
        }
        else
        if (aPress || onCStick)
        {
            pam.UseAAttack(this, onCStick);
        }
        else
        {

            if (hori > horiThreshold)
            {
                moveState = MoveStates.RIGHTWALK;
                isFacingLeft = false;

            }
            else

            if (hori < -horiThreshold)
            {
                moveState = MoveStates.LEFTWALK;
                isFacingLeft = true;
            }
            else
            {
                if (moveState == MoveStates.LEFTWALK || moveState == MoveStates.RIGHTWALK)
                    moveState = MoveStates.STILL;
            }
        }
    }

    //called every frame when the player is airborn and can act. Handles airborne physics
    void AirControl()
    {
        //directional influence for drifting
        if (hori > 0.3F && airMomentum.x > -maxAirSpeed)
        {
            airMomentum.x -= hori * airAccel;
        }
        if (hori < -0.3F && airMomentum.x < maxAirSpeed)
        {
            airMomentum.x += -hori * airAccel;
        }

        //deccel horizontal momentum when not drifting
        if (!(hori > 0.3F) && !(hori < -0.3F))
        {
            if (airMomentum.x > 0)
            {
                airMomentum.x -= airDeccel;
                if (airMomentum.x < 0)
                {
                    airMomentum.x = 0;
                }
            }
            else if (airMomentum.x < 0)
            {
                airMomentum.x += airDeccel;
                if (airMomentum.x > 0)
                {
                    airMomentum.x = 0;
                }
            }
        }

        //fast falling
        if (onStickDown && airMomentum.y < 0)
        {
            fastFalling = true;
            CheckFastFall();
            airMomentum.y = -maxFallSpeed;
        }

        //accel downwards
        if (airMomentum.y > -maxFallSpeed)
        {
            airMomentum.y -= fallSpeed;
        }
        //if falling too fast, slow down
        if (airMomentum.y < -maxFallSpeed)
        {
            airMomentum.y = -maxFallSpeed;
        }

        if (yPress || xPress)
        {
            Jump(true);
        }

        if (bPress)
        {
            pam.UseSpecialAttack(this);
        }
        else
        if (aPress || onCStick)
        {
            pam.UseAerialAttack(this, onCStick);
            fastFalling = false;
        }

        PreventClipping();
    }

    //called every frame when the player is using an aerial
    void AerialAttackControl()
    {
        //directional influence for drifting
        if (hori > 0.3F && airMomentum.x > -maxAirSpeed)
        {
            airMomentum.x -= hori * airAccel;
        }
        if (hori < -0.3F && airMomentum.x < maxAirSpeed)
        {
            airMomentum.x += -hori * airAccel;
        }

        //deccel horizontal momentum when not drifting
        if (!(hori > 0.3F) && !(hori < -0.3F))
        {
            if (airMomentum.x > 0)
            {
                airMomentum.x -= airDeccel;
                if (airMomentum.x < 0)
                {
                    airMomentum.x = 0;
                }
            }
            else if (airMomentum.x < 0)
            {
                airMomentum.x += airDeccel;
                if (airMomentum.x > 0)
                {
                    airMomentum.x = 0;
                }
            }
        }

        //fast falling
        if (onStickDown && airMomentum.y < 0)
        {
            fastFalling = true;
            CheckFastFall();
            airMomentum.y = -maxFallSpeed;
        }

        //accel downwards
        if (airMomentum.y > -maxFallSpeed)
        {
            airMomentum.y -= fallSpeed;
        }
        //if falling too fast, slow down
        if (airMomentum.y < -maxFallSpeed)
        {
            airMomentum.y = -maxFallSpeed;
        }

        if (PreventClipping() || isGrounded)
        {
            EndAttack();
            frameCancel = true;
            return;
        }
        pam.RunAttackFrame(this, currAttack, currAttackFrame);
        currAttackFrame++;
    }

    //called every frame when the player is using a special move
    void SpecialMoveControl()
    {
        if (isGrounded)
        {
            ApplyTraction();
        }
        else
        //Simulates air control if the properties of the move allow
        {
            if (currAttack.frameData[currAttackFrame].canControl)
            {
                //directional influence for drifting
                if (hori > 0.3F && airMomentum.x > -maxAirSpeed)
                {
                    airMomentum.x -= hori * airAccel;
                }
                if (hori < -0.3F && airMomentum.x < maxAirSpeed)
                {
                    airMomentum.x += -hori * airAccel;
                }

                //deccel horizontal momentum when not drifting
                if (!(hori > 0.3F) && !(hori < -0.3F))
                {
                    if (airMomentum.x > 0)
                    {
                        airMomentum.x -= airDeccel;
                        if (airMomentum.x < 0)
                        {
                            airMomentum.x = 0;
                        }
                    }
                    else if (airMomentum.x < 0)
                    {
                        airMomentum.x += airDeccel;
                        if (airMomentum.x > 0)
                        {
                            airMomentum.x = 0;
                        }
                    }
                }
                //transform.Translate(new Vector3(airMomentum.x, 0));
            }

            if (currAttack.frameData[currAttackFrame].canFall)
            {
                //accel downwards
                if (airMomentum.y > -maxFallSpeed)
                {
                    airMomentum.y -= fallSpeed;
                }
                //if falling too fast, slow down
                if (airMomentum.y < -maxFallSpeed)
                {
                    airMomentum.y = -maxFallSpeed;
                }

                //transform.Translate(new Vector3(0, airMomentum.y));
            }
        }
        if ((PreventClipping() || isGrounded) && currAttack.groundCancel && currAttack.frameData[currAttackFrame].cancelable)
        {
            EndAttack();
            frameCancel = true;
            return;
        }

        pam.RunAttackFrame(this, currAttack, currAttackFrame);
        currAttackFrame++;
    }

    //called every frame that the player is in hitstun
    void HitStunControl()
    {
        //accel downwards
        if (knockbackMomentum.y > -maxFallSpeed)
        {
            knockbackMomentum.y -= fallSpeed;
        }
        //if falling too fast, slow down depending on your speed
        if (knockbackMomentum.y < -maxFallSpeed)
        {

            knockbackMomentum.y += (fallSpeed) * 1.05F;
        }

        transform.position += knockbackMomentum;
        //if player has landed on the ground, stop falling (bounce if spiked--techs WIP) and apply traction
        if (PreventClipping())
        {
            Land();
            if (knockbackMomentum.y < -maxFallSpeed * 1.6)
            {
                knockbackMomentum.y = -knockbackMomentum.y * 0.6F;
                hitStunDuration += 10;
            }
            else
            {
                knockbackMomentum.y = 0;
            }
        }

        //Doesnt use ApplyTraction(), because this isn't groundMomentum
        if (isGrounded)
        {
            if (knockbackMomentum.x > 0)
            {
                knockbackMomentum.x -= traction / 2;
                if (knockbackMomentum.x < 0)
                {
                    knockbackMomentum.x = 0;
                }
            }
            else if (knockbackMomentum.x < 0)
            {
                knockbackMomentum.x += traction / 2;
                if (knockbackMomentum.x > 0)
                {
                    knockbackMomentum.x = 0;
                }
            }
        }
 
    }

    //called when hitlag starts, signals end of hitlag
    public virtual IEnumerator Hitlag(int duration, float knockbackValue, int hitstun, float angle, PlayerController sender = null, bool attacker = false)
    {
        if (frameCancel)
        {
            yield break;
        }



        //remembers the attackers state; 0 ATTACK, 1 AERIAL, 2 SPECIALMOVE
        int returnID = 0;
        if (attacker)
        {
            if (moveState == MoveStates.ATTACK)
            {
                returnID = 0;
            }
            if (moveState == MoveStates.AERIAL)
            {
                returnID = 1;
            }
            else if (moveState == MoveStates.SPECIALMOVE)
            {
                returnID = 2;
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
                switch (returnID)
                {
                    case 0:
                        moveState = MoveStates.ATTACK;
                        break;

                    case 1:
                        moveState = MoveStates.AERIAL;
                        break;

                    case 2:
                        moveState = MoveStates.SPECIALMOVE;
                        break;

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

    //called when hitstun starts, signals end of hitstun
    public virtual IEnumerator Hitstun()
    {
        if ((isGrounded || PreventClipping()) && Mathf.Abs(knockbackMomentum.y) < 0.15F)
        {
            knockbackMomentum.y = 0;
        }
        else
        {
            transform.Translate(new Vector3(0, 0.1F));
            isGrounded = false;
        }
        for (int i = 0; i < hitStunDuration; i++)
        {
            yield return null;
        }
        if (isGrounded)
        {
            moveState = MoveStates.STILL;
            groundMomentum.x = knockbackMomentum.x;
            hitStunDuration = 0;
            inHitstun = false;
        }
        else
        {
            moveState = MoveStates.AIRBORNE;
            airMomentum = knockbackMomentum;
            hitStunDuration = 0;
            inHitstun = false;
        }

    }

    //Returns true if clipping was prevented
    protected bool PreventClipping()
    {
        if (transform.position.y <= groundLevel)
        {
            isGrounded = true;
            transform.position = new Vector3(transform.position.x, groundLevel);
            return true;

        }
        else
        {
            return false;
        }
    }

    //initiates jumpsquat when grounded. Automatically jumps when jumpsquat ends
    void StartJumpsquat()
    {
        moveState = MoveStates.JUMPSQUAT;
        lagCount = jumpFrames;
    }

    //initiates the player's jump after jumpsquat ends
    void Jump(bool aerial = false)  
    {
        //sets position to be just above the ground to avoid staying grounded after jumping
        transform.position += new Vector3(0, 0.01F);
        isGrounded = false;
        moveState = MoveStates.AIRBORNE;
        Vector3 momen = new Vector3(0, 0);

        if (aerial)
        {
            if (jumpUsed == jumpCount)
            {
                return;
            } else
            {
                jumpUsed++;
                fastFalling = false;
                momen.y += jumpMomentum;

                //determines if jumping in a directon
                if (hori > horiThreshold)
                {
                    momen.x -= maxAirSpeed;

                }

                if (hori < -horiThreshold)
                {
                    momen.x += maxAirSpeed;

                }
            }
        }
        else
        {

            //determines if this is a shorthop or fullhop
            if (xHold || yHold)
            {
                momen.y += jumpMomentum;
            }
            else
            {
                momen.y += 0.75F * (jumpMomentum);
            }

            //determines if jumping in a directon
            if (hori > horiThreshold)
            {
                momen.x -= maxAirSpeed;

            }

            if (hori < -horiThreshold)
            {
                momen.x += maxAirSpeed;

            }
        }
        airMomentum = momen;
    }

    //called on first frame of landing
    void Land()
    {
        PreventClipping();
        isGrounded = true;
        groundMomentum.x = airMomentum.x;
        airMomentum = new Vector3(0, 0);
        jumpUsed = 0;
        upBUsed = 0;
        fastFalling = false;
    }

    //Slows groundMomentum by traction
    void ApplyTraction()
    {
        if (groundMomentum.x < 0)
        {
            groundMomentum.x += traction;
            if (groundMomentum.x > 0)
            {
                groundMomentum.x = 0;
            }
        }

        if (groundMomentum.x > 0)
        {
            groundMomentum.x -= traction;
            if (groundMomentum.x < 0)
            {
                groundMomentum.x = 0;
            }
        }
    }

    //called on the last frame of an attack. Make sure to use InterruptAttack if the attack is being cancelled by something else
    public void EndAttack()
    {
        pam.DestroyHitboxes();

        if (isGrounded)
        {
            //checks if this move should end with landing lag
            if (currAttack.landingLag != 0 && !currAttack.frameData[currAttackFrame].autoCancel)
            {
                moveState = MoveStates.LANDINGLAG;
                lagCount = currAttack.landingLag;
            }
            else
            {
                moveState = MoveStates.STILL;
            }
        } else
        {
            moveState = MoveStates.AIRBORNE;
        }

        //resets the nullified attacks list
        foreach (List<Attack> nullifier in PlayerAttackManager.nullAttackLists)
        {
            nullifier.Remove(currAttack);
        }

        currAttack = null;
        currAttackFrame = 0;
    }

    //Used when an attack is cut short/canceled by something else.
    public void InterruptAttack()
    {
        pam.DestroyHitboxes();

        //resets the nullified attacks list
        foreach (List<Attack> nullifier in PlayerAttackManager.nullAttackLists)
        {
            nullifier.Remove(currAttack);
        }

        currAttack = null;
        currAttackFrame = 0;
    }

    //returns true if the player is outside the bounds of the stage. Note that if the player is above the top of the stage, they must also be in HITSTUN
    public bool CheckBlastZones()
    {
        if (transform.position.x >= GameData.stageWidth || transform.position.x <= -GameData.stageWidth || 
            (transform.position.y >= GameData.stageHeight && moveState == MoveStates.HITSTUN) || transform.position.y <= GameData.stageBottom)
        {
            return true;
        } else
        {
            return false;
        }
    }

    //called when the player dies
    public void Death()
    {
        InterruptAttack();
        currDamage = 0;
        transform.position = new Vector3(0, 5);
        airMomentum = Vector3.zero;
        groundMomentum = Vector3.zero;
        knockbackMomentum = Vector3.zero;
        moveState = MoveStates.AIRBORNE;

    }

    //gets the values for all of the input variables from the input manager
    void GetInputs()
    {
        switch (playerNum)
        {
            case 1:
                hori = InputManager.hori;
                vert = InputManager.vert;

                onStickDown = InputManager.onStickDown;
                onStick = InputManager.onStick;
                lastStickX = InputManager.lastStickX;
                lastStickY = InputManager.lastStickY;

                cHori = InputManager.cHori;
                cVert = InputManager.cVert;

                onCStick = InputManager.onCStick;
                lastCStickX = InputManager.lastCStickX;
                lastCStickY = InputManager.lastCStickY;

                aPress = InputManager.aPress;
                bPress = InputManager.bPress;
                xPress = InputManager.xPress;
                yPress = InputManager.yPress;
                zPress = InputManager.zPress;

                aHold = InputManager.aHold;
                lastA = InputManager.lastA;

                bHold = InputManager.bHold;
                lastB = InputManager.lastB;

                xHold = InputManager.xHold;
                lastX = InputManager.lastX;

                yHold = InputManager.yHold;
                lastY = InputManager.lastY;

                zHold = InputManager.zHold;
                lastZ = InputManager.lastZ;

                break;
            case 2:
                hori = InputManager.hori2;
                vert = InputManager.vert2;

                onStickDown = InputManager.onStickDown2;
                onStick = InputManager.onStick2;
                lastStickX = InputManager.lastStickX2;
                lastStickY = InputManager.lastStickY2;

                cHori = InputManager.cHori2;
                cVert = InputManager.cVert2;

                onCStick = InputManager.onCStick2;
                lastCStickX = InputManager.lastCStickX2;
                lastCStickY = InputManager.lastCStickY2;

                aPress = InputManager.aPress2;
                bPress = InputManager.bPress2;
                xPress = InputManager.xPress2;
                yPress = InputManager.yPress2;
                zPress = InputManager.zPress2;

                aHold = InputManager.aHold2;
                lastA = InputManager.lastA2;

                bHold = InputManager.bHold2;
                lastB = InputManager.lastB2;

                xHold = InputManager.xHold2;
                lastX = InputManager.lastX2;

                yHold = InputManager.yHold2;
                lastY = InputManager.lastY2;

                zHold = InputManager.zHold2;
                lastZ = InputManager.lastZ2;

                break;
        }
    }

    //sets maximum fall speed depending on fastfalling
    void CheckFastFall()
    {
        if (fastFalling)
        {
            maxFallSpeed = 1.6F * baseMaxFallSpeed;
        } else
        {
            maxFallSpeed = baseMaxFallSpeed;
        }

    }

    void CheckAnimation()
    {

        if ((moveState == MoveStates.HITLAG || moveState == MoveStates.HITSTUN) && currAttack == null)
        {
            render.material = hitstunned;
        } else
        {
            render.material = standing;
        }
    }

    //TODO More movement options (dash, dodge), Wall Collisions, Ledges, Blocking, 
}