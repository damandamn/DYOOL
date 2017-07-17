using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {

    public List<GameObject> hitBoxes;
    public List<MoveFrame> frameData;
    public bool aerial = false;
    public int landingLag = 0;
    public bool destroyOnHit = false;
    public bool pierceShield = false;

    //variables for special moves
    public bool special = false;
    public GameObject projectile = null;
    public bool cancelAirMomentum = false;
    public bool reversable = true;
    public int reverseFrames = 0;
    public int aerialUses = 1;
    public bool restoreOnHit = true;
    public bool endlagOnHit = false;
    public bool groundCancel = false;
    public bool specialFall = false;

    public Attack(List<GameObject> hb, List<MoveFrame> fd, bool air = false, int lLag = 0)
    {
        hitBoxes = hb;
        frameData = fd;
        aerial = air;
        landingLag = lLag;

        for (int i = 0; i < hitBoxes.Count; i++)
        {
            hitBoxes[i].GetComponent<HitBox>().priority = i;
        }
    }


    public Attack(bool s, List<GameObject> hb, List<MoveFrame> fd, int airuses = 1, bool spFall = false, bool control = false, bool gCancel = false, int lLag = 0)
    {
        special = s;

        hitBoxes = hb;
        frameData = fd;
        aerialUses = airuses;
        specialFall = spFall;
        groundCancel = gCancel;
        landingLag = lLag;

        for (int i = 0; i < hitBoxes.Count; i++)
        {
            hitBoxes[i].GetComponent<HitBox>().priority = i;
        }
    }


}
