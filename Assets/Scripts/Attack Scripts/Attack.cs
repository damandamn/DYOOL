using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {

    public List<GameObject> hitBoxes;
    public List<MoveFrame> frameData;
    public bool aerial = false;
    public int landingLag = 0;

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

    
}
