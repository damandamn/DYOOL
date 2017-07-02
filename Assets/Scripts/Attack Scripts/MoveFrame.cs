using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFrame {

    public bool startupFrame = false;
    public bool endlagFrame = false;

    public bool hitboxActive = false;
    public List<bool> allHitboxesActive;

    public bool hitboxAnimated = false;
    public List<Vector3> allHitboxesAnimated;

    public bool autoCancel = false;
    public bool cancelable = false;
    public bool lastFrame = false;

    public MoveFrame(bool startframe, bool endframe, bool hboxactive, List<bool> activehboxlist= null, bool lastframe = false, bool hboxanimated = false, List<Vector3> animateList = null, bool aCan = false, bool can = false)
    {
        startupFrame = startframe;
        endlagFrame = endframe;
        hitboxActive = hboxactive;
        allHitboxesActive = activehboxlist;
        lastFrame = lastframe;

        hitboxAnimated = hboxanimated;
        allHitboxesAnimated = animateList;

        autoCancel = aCan;
        cancelable = can;
}
}
