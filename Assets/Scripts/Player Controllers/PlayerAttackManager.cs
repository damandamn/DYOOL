using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour {

    //used to prevent disabling already disabled hitboxes;
    bool allHitboxesDisabled = false;
    List<GameObject> currHitboxes;

    //used for priority
    bool hitThisFrame = false;
    int hitCount = 0;
    List<HitBox> hits;

    public static List<List<Attack>> nullAttackLists = new List<List<Attack>>();

    //checks which A attack use
    public void UseAAttack(PlayerController user, bool isCStick)
    {
        if (isCStick)
        {
            //CStick is diagonal
            if (Mathf.Abs(user.cHori) >= user.horiThreshold && Mathf.Abs(user.cVert) >= user.vertThreshold)
            {
                StartAttack(user, user.jabAttack);
            }
            else
            //CStick is to the right
            if (user.cHori >= user.horiThreshold)
            {
                user.isFacingLeft = false;
                StartAttack(user, user.fTiltAttack);
            }
            else
            //CStick is to the left
            if (user.cHori <= -user.horiThreshold)
            {
                user.isFacingLeft = true;
                StartAttack(user, user.fTiltAttack);
            }
            else
            //CStick is up
            if (user.cVert >= user.vertThreshold)
            {
                StartAttack(user, user.uTiltAttack);
            }
            else
            //CStick is down
            if (user.cVert <= -user.vertThreshold)
            {
                StartAttack(user, user.dTiltAttack);
            }
            else
            {
                user.InterruptAttack();
            }
        }
        else
        {
            //Stick is diagonally up
            if (Mathf.Abs(user.hori) > Mathf.Abs(user.horiThreshold) && user.vert > user.vertThreshold)
            {
                if (Mathf.Abs(user.hori) >= Mathf.Abs(user.vert))
                {
                    StartAttack(user, user.fTiltAttack);
                }
                else
                {
                    StartAttack(user, user.uTiltAttack);
                }
            }
            else
            //Stick is diagonally down
            if (Mathf.Abs(user.hori) > Mathf.Abs(user.horiThreshold) && user.vert < -user.vertThreshold)
            {
                if (Mathf.Abs(user.hori) > Mathf.Abs(user.vert))
                {
                    StartAttack(user, user.fTiltAttack);
                }
                else
                {
                    StartAttack(user, user.dTiltAttack);
                }
            }
            else
            //Stick is neutral
            if (Mathf.Abs(user.hori) < Mathf.Abs(user.horiThreshold) && Mathf.Abs(user.vert) < Mathf.Abs(user.vertThreshold))
            {
                StartAttack(user, user.jabAttack);
            }
            else
            //stick is forward/backward
            if (Mathf.Abs(user.hori) > Mathf.Abs(user.horiThreshold) && Mathf.Abs(user.vert) < Mathf.Abs(user.vertThreshold))
            {
                StartAttack(user, user.fTiltAttack);
            }
            else
            //stick is up
            if (Mathf.Abs(user.hori) < Mathf.Abs(user.horiThreshold) && user.vert > user.vertThreshold)
            {
                StartAttack(user, user.uTiltAttack);
            }
            else
            //stick is down
            if (Mathf.Abs(user.hori) < Mathf.Abs(user.horiThreshold) && user.vert < -user.vertThreshold)
            {
                StartAttack(user, user.dTiltAttack);
            }
            else
            {
                user.InterruptAttack();
            }
        }
    }

    public void UseAerialAttack(PlayerController user, bool isCStick)
    {
        if (isCStick)
        {
            //CStick is diagonal
            if (Mathf.Abs(user.cHori) >= user.horiThreshold && Mathf.Abs(user.cVert) >= user.vertThreshold)
            {
                StartAttack(user, user.nAirAttack);
            }
            else
            //CStick is down
            if (user.cVert <= -user.vertThreshold)
            {
                StartAttack(user, user.dAirAttack);
            }
            else
            //CStick is up
            if (user.cVert >= user.vertThreshold)
            {
                StartAttack(user, user.uAirAttack);
            }
            else
            //CStick is to the right
            if (user.cHori >= user.horiThreshold)
            {
                if (!user.isFacingLeft)
                {
                    StartAttack(user, user.fAirAttack);
                }
                else
                {
                    StartAttack(user, user.bAirAttack);
                }
            }
            else
            //CStick is to the left
            if (user.cHori <= -user.horiThreshold)
            {
                if (user.isFacingLeft)
                {
                    StartAttack(user, user.fAirAttack);
                }
                else
                {
                    StartAttack(user, user.bAirAttack);
                }
            }
            else
            {
                user.InterruptAttack();
            }

        }
        else
        {
            //Stick is diagonally up
            if (Mathf.Abs(user.hori) > Mathf.Abs(user.horiThreshold) && user.vert > user.vertThreshold)
            {
                if (Mathf.Abs(user.vert) >= Mathf.Abs(user.hori))
                {
                    StartAttack(user, user.uAirAttack);
                }
                else
                {
                    if (user.hori > user.horiThreshold)
                    {
                        if (!user.isFacingLeft)
                        {
                            StartAttack(user, user.fAirAttack);
                        }
                        else
                        {
                            StartAttack(user, user.bAirAttack);
                        }
                    }
                    else
                    if (user.hori < -user.horiThreshold)
                    {
                        if (user.isFacingLeft)
                        {
                            StartAttack(user, user.fAirAttack);
                        }
                        else
                        {
                            StartAttack(user, user.bAirAttack);
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
                    StartAttack(user, user.dAirAttack);
                }
                else
                {
                    if (user.hori > user.horiThreshold)
                    {
                        if (!user.isFacingLeft)
                        {
                            StartAttack(user, user.fAirAttack);
                        }
                        else
                        {
                            StartAttack(user, user.bAirAttack);
                        }
                    }
                    else
                    if (user.hori < -user.horiThreshold)
                    {
                        if (user.isFacingLeft)
                        {
                            StartAttack(user, user.fAirAttack);
                        }
                        else
                        {
                            StartAttack(user, user.bAirAttack);
                        }
                    }
                }
            }
            else
            if (Mathf.Abs(user.hori) < user.horiThreshold && Mathf.Abs(user.vert) < user.vertThreshold)
            {
                StartAttack(user, user.nAirAttack);
            }
            else
            if (Mathf.Abs(user.hori) < user.horiThreshold && user.vert < -user.vertThreshold)
            {
                StartAttack(user, user.dAirAttack);
            }
            else
            if (Mathf.Abs(user.hori) < user.horiThreshold && user.vert > user.vertThreshold)
            {
                StartAttack(user, user.uAirAttack);
            }
            else
            if (user.hori > user.horiThreshold && Mathf.Abs(user.vert) < user.vertThreshold)
            {
                if (!user.isFacingLeft)
                {
                    StartAttack(user, user.fAirAttack);
                }
                else
                {
                    StartAttack(user, user.bAirAttack);
                }
            }
            else
            if (user.hori < -user.horiThreshold && Mathf.Abs(user.vert) < user.vertThreshold)
            {
                if (user.isFacingLeft)
                {
                    StartAttack(user, user.fAirAttack);
                }
                else
                {
                    StartAttack(user, user.bAirAttack);
                }
            }
            else
            {
                user.InterruptAttack();
            }
        }
    }

    public virtual void UseSpecialAttack(PlayerController user)
    {
        //stick is neutral
        if (Mathf.Abs(user.vert) < user.vertThreshold && Mathf.Abs(user.hori) < user.horiThreshold)
        {
            StartAttack(user, user.neutralBAttack);
        }
        else
        //stick is up
        if (user.vert > user.vertThreshold)
        {
            if (user.vert >= Mathf.Abs(user.hori))
            {
                if (user.upBUsed < user.upBAttack.aerialUses)
                {
                    user.upBUsed++;
                    StartAttack(user, user.upBAttack);
                }
            }
            else
            //Stick is diagonally up
            {

            }
        }
        else
        //stick is left or right
        if (user.hori >= user.horiThreshold)
        {
            if (user.sideBUsed < user.sideBAttack.aerialUses)
            {
                user.isFacingLeft = false;
                if (user.isGrounded)
                {
                    StartAttack(user, user.sideBAttack);
                } else
                {
                    StartAttack(user, user.sideBAttackAerial);
                }
            }

        }
        else if (user.hori <= user.horiThreshold)
        {
            if (user.sideBUsed < user.sideBAttack.aerialUses)
            {
                user.isFacingLeft = true;
                if (user.isGrounded)
                {
                    StartAttack(user, user.sideBAttack);
                }
                else
                {
                    StartAttack(user, user.sideBAttackAerial);
                }
            }
        }
    }

    public void UseDefensiveOption(PlayerController user)
    {
        StartAttack(user, user.airdodge);
    }

    //initiates a specified attack
    protected void StartAttack(PlayerController user, Attack attack)
    {
        currHitboxes = new List<GameObject>();
        if (attack.aerial)
        {
            user.moveState = PlayerController.MoveStates.AERIAL;
        }
        else if (attack.special)
        {
            user.moveState = PlayerController.MoveStates.SPECIALMOVE;
            if (attack.cancelAirMomentum)
            {
                user.airMomentum = Vector3.zero;
            }
        }
        else
        {
            user.moveState = PlayerController.MoveStates.ATTACK;
        }
        user.currAttackFrame = 0;
        user.currAttack = attack;
        foreach (GameObject hitbox in user.currAttack.hitBoxes)
        {
            if (hitbox.GetComponent<HitBox>().hitboxType == "Attack")
            {
                CreateHitbox(hitbox, user);
            }


        }
        allHitboxesDisabled = false;
    }

    public void CreateHitbox(GameObject hitbox, PlayerController user)
    {
        GameObject go = Instantiate(hitbox);

        go.transform.parent = user.transform;
        go.transform.position = user.transform.position;
        go.transform.localScale = new Vector3(0.3F, 0.3F, 0.3F);
        HitBox goHitbox = go.GetComponent<HitBox>();
        goHitbox.user = user;

        //Visualizes hitbox if neccesary
        if (GameData.visualizeHitboxes)
        {
            GameObject hitboxSphere = Instantiate(GameData.hitboxRenderPrefab);
            hitboxSphere.transform.parent = go.transform;

            hitboxSphere.GetComponent<HitboxRender>().source = go;
            hitboxSphere.GetComponent<HitboxRender>().material = hitboxSphere.GetComponent<Renderer>().material;

            hitboxSphere.GetComponent<HitboxRender>().InitiateVisualization();
        }

        currHitboxes.Add(go);
    }

    //checks what to do at the start of each frame in an attack animation
    public void RunAttackFrame(PlayerController user, Attack attack, int frame)
    {
        MoveFrame attackFrame = attack.frameData[frame];

        //runs on frames that play audio
        if (attackFrame.playSound && user.moveState != PlayerController.MoveStates.HITLAG)
        {
            user.PlaySound(attackFrame.frameSound);
        }

        //B-reversing in the first few frames of a move
        if (attack.special && attack.reversable && frame < attack.reverseFrames)
        {
            if (user.isFacingLeft)
            {
                if (user.hori >= user.horiThreshold)
                {
                    user.isFacingLeft = false;
                    user.airMomentum.x *= -1;
                }
            }
            else
            {
                if (user.hori <= -user.horiThreshold)
                {
                    user.isFacingLeft = true;
                    user.airMomentum.x *= -1;
                }
            }
        }

        //moves the user by the frames specified vector, if any
        if ((attackFrame.userGroundMovement != Vector3.zero || attackFrame.userMovement != Vector3.zero) && user.moveState != PlayerController.MoveStates.HITLAG)
        { 
            Vector3 tempMovement;

            if (user.isGrounded && attackFrame.userMovement.y <= 0)
            {
                if (user.isFacingLeft)
                {
                    tempMovement = new Vector3(attackFrame.userGroundMovement.x, attackFrame.userGroundMovement.y);
                }
                else
                {
                    tempMovement = new Vector3(-attackFrame.userGroundMovement.x, attackFrame.userGroundMovement.y);
                }
            } else
            {
                if (user.isFacingLeft)
                {
                    tempMovement = new Vector3(attackFrame.userMovement.x, attackFrame.userMovement.y);
                }
                else
                {
                    tempMovement = new Vector3(-attackFrame.userMovement.x, attackFrame.userMovement.y);
                }
            }

            if (user.isGrounded)
            {
                user.transform.Translate(tempMovement);
            }

            if (attack.aerial) {
                user.airMomentum = tempMovement;
            }
            else
            if (attack.special)
            {
                if (attackFrame.setAirMomentum)
                {
                    user.transform.Translate(tempMovement);
                } else
                {
                    user.airMomentum += tempMovement;
                }
            }
            
        }
        
        //allows for multihit moves. Always put rehit on an inactive frame
        if (attackFrame.reHit && user.moveState != PlayerController.MoveStates.HITLAG)
        {
            foreach (List<Attack> nullify in nullAttackLists)
            {
                nullify.Remove(attack);
            }
        }

        //runs on i-frames
        if (attackFrame.iFrame)
        {
            user.MakeInvincible(false);
        } else
        {
            user.MakeInvincible(true);
        }

        //runs on frames with no active hitboxes
        if (attackFrame.startupFrame == true || attackFrame.endlagFrame == true)
        {
            if (!allHitboxesDisabled)
            {
                foreach (GameObject hitbox in currHitboxes)
                {
                    hitbox.GetComponent<SphereCollider>().enabled = false;
                }
                allHitboxesDisabled = true;
            }
        }

        //runs on frames that spawn projectiles
        if (attackFrame.spawnProjectile && user.moveState != PlayerController.MoveStates.HITLAG)
        {
            attack.projectile.GetComponent<HitBox>().user = user;
            Projectile proj = attack.projectile.GetComponent<Projectile>();
            proj.user = user;

            foreach (GameObject go in attack.hitBoxes)
            {
                go.GetComponent<HitBox>().user = user;
            }

            if (!user.isFacingLeft)
            {
                Instantiate(attack.projectile, user.transform.position + proj.offset, proj.rotation);
            }
            else
            {
                Instantiate(attack.projectile, user.transform.position + new Vector3(-proj.offset.x, proj.offset.y), new Quaternion(proj.rotation.x, proj.rotation.y + 180, proj.rotation.z, 0));
            }
        }

        //runs on frames that contain active hitboxes
        if (attackFrame.hitboxActive == true)
        {
            allHitboxesDisabled = false;

            //Animates animated hitboxes
            if (attackFrame.hitboxAnimated)
            {
                int i = 0;
                foreach (Vector3 offset in attackFrame.allHitboxesAnimated)
                {
                    if (offset.x != 0 || offset.y != 0)
                    {
                        currHitboxes[i].transform.Translate(new Vector3(offset.x, offset.y));
                    }

                    i++;
                }
            }

            //Disables/enables hitboxes as described in allHitboxesActive
            for (int i2 = 0; i2 < attackFrame.allHitboxesActive.Count; i2++)
            {
                if (attackFrame.allHitboxesActive[i2])
                {
                    currHitboxes[i2].GetComponent<SphereCollider>().enabled = true;
                }
                else
                {
                    currHitboxes[i2].GetComponent<SphereCollider>().enabled = false;
                }
            }
        } else

        //runs on the last frame of an attack
        if (attackFrame.lastFrame == true)
        {
            user.EndAttack();
        }
    }

    public void PrioritizeHit(HitBox hit)
    {
        hitCount++;
        if (!hitThisFrame)
        {
            hitThisFrame = true;
            hits = new List<HitBox>();
            StartCoroutine(SendHit());
        }
        hits.Add(hit);
    }

    public void DestroyHitboxes()
    {
        try
        {
            foreach (GameObject hitbox in currHitboxes)
            {
                Destroy(hitbox);

            }
        } catch
        {
        }
    }

    void DestroyHitbox(HitBox hitbox)
    {
        Destroy(hitbox.gameObject);
    }

    IEnumerator SendHit()
    {
        yield return new WaitForEndOfFrame();
        HitBox sender = hits[0];
        if (hitCount > 1)
        { 
            foreach (HitBox h in hits)
            {
                if (h.priority < sender.priority)
                {
                    sender = h;
                }
            }
        }

        //Destroys hitbox if applicable
        if (sender.attack.destroyOnHit)
        {
            if (sender.hitboxType == "Collateral")
            {
                foreach (Transform child in sender.transform.parent)
                {
                    Destroy(child.gameObject);
                }
                Destroy(sender.transform.parent.gameObject);
            }
            else
            {
                try
                {
                    Destroy(sender.gameObject);
                } catch { }
            }
        }
        if (sender.hitSound != null)
        {
            sender.PlaySound();
        }
        sender.hit.GetComponent<PlayerController>().nullify.Add(sender.attack);
        GameLoader.hitmanager.CalculateHit(sender, sender.user, sender.hit.GetComponent<PlayerController>());
        if (sender.attack.endlagOnHit)
        {
            int i = 0;
            foreach (MoveFrame frame in sender.attack.frameData)
            {
                if (frame.endlagFrame)
                {
                    break;
                }
                i++;
            }
            sender.user.currAttackFrame = i;
        }

        hitCount = 0;
        hitThisFrame = false;
    }

}
