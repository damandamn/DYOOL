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
            if (Mathf.Abs(user.cHori) >= user.horiThreshold && Mathf.Abs(user.cVert) >= user.vertThreshold)
            {
                StartAttack(user, user.jabAttack);
            }
            else
            if (user.cHori >= user.horiThreshold)
            {
                user.isFacingLeft = false;
                StartAttack(user, user.fTiltAttack);
            }
            else
            if (user.cHori <= -user.horiThreshold)
            {
                user.isFacingLeft = true;
                StartAttack(user, user.fTiltAttack);
            }
            else
            if (user.cVert >= user.vertThreshold)
            {
                StartAttack(user, user.uTiltAttack);
            }
            else
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
            if (Mathf.Abs(user.hori) < Mathf.Abs(user.horiThreshold) && Mathf.Abs(user.vert) < Mathf.Abs(user.vertThreshold))
            {
                StartAttack(user, user.jabAttack);
            }
            else
            if (Mathf.Abs(user.hori) > Mathf.Abs(user.horiThreshold) && Mathf.Abs(user.vert) < Mathf.Abs(user.vertThreshold))
            {
                StartAttack(user, user.fTiltAttack);
            }
            else
            if (Mathf.Abs(user.hori) < Mathf.Abs(user.horiThreshold) && user.vert > user.vertThreshold)
            {
                StartAttack(user, user.uTiltAttack);
            }
            else
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
            if (Mathf.Abs(user.cHori) >= user.horiThreshold && Mathf.Abs(user.cVert) >= user.vertThreshold)
            {
                StartAttack(user, user.nAirAttack);
            }
            else
            if (user.cVert <= -user.vertThreshold)
            {
                StartAttack(user, user.dAirAttack);
            }
            else
            if (user.cVert >= user.vertThreshold)
            {
                StartAttack(user, user.uAirAttack);
            }
            else
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
                } else
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
                } else
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

    //initiates a specified attack
    void StartAttack(PlayerController user, Attack attack)
    {
        currHitboxes = new List<GameObject>();
        if (attack.aerial)
        {
            user.moveState = PlayerController.MoveStates.AERIAL;
        }
        else
        {
            user.moveState = PlayerController.MoveStates.ATTACK;
        }
        user.currAttackFrame = 0;
        user.currAttack = attack;
        foreach (GameObject hitbox in user.currAttack.hitBoxes)
        {
            GameObject go = Instantiate(hitbox);

            go.transform.parent = user.transform;
            go.transform.position = user.transform.position;
            go.transform.localScale = new Vector3(0.3F, 0.3F, 0.3F);

            go.GetComponent<HitBox>().user = user;
            currHitboxes.Add(go);
        }
        allHitboxesDisabled = false;
    }

    //checks what to do at the start of each frame in an attack animation
    public void RunAttackFrame(PlayerController user, Attack attack, int frame)
    {


        MoveFrame attackFrame = attack.frameData[frame];

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

        //runs on frames that contain active hitboxes
        if (attackFrame.hitboxActive == true)
        {
            allHitboxesDisabled = false;

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
            int i2 = 0;
            foreach (bool active in attackFrame.allHitboxesActive)
            {
                if (active)
                {
                    currHitboxes[i2].GetComponent<SphereCollider>().enabled = true;
                } else
                {
                    currHitboxes[i2].GetComponent<SphereCollider>().enabled = false;
                }

                i2++;
            }
        } else

        //runs on the last frame of an animation
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

    IEnumerator SendHit()
    {
        yield return new WaitForEndOfFrame();
        HitBox sender = hits[0];
        if (hitCount == 1)
        {
            sender.hit.GetComponent<PlayerController>().nullify.Add(sender.attack);
            GameLoader.hitmanager.CalculateHit(sender, sender.user, sender.hit.GetComponent<PlayerController>());
        } else
        {
            foreach (HitBox h in hits)
            {
                if (h.priority < sender.priority)
                {
                    sender = h;
                }
            }
            sender.hit.GetComponent<PlayerController>().nullify.Add(sender.attack);
            GameLoader.hitmanager.CalculateHit(sender, sender.user, sender.hit.GetComponent<PlayerController>());
        }
        hitCount = 0;
        hitThisFrame = false;
    }

}
