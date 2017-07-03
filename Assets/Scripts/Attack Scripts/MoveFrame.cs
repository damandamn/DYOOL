using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFrame {

    public bool startupFrame = false;
    public bool endlagFrame = false;

    public bool hitboxActive = false;
    public List<bool> allHitboxesActive = null;

    public bool hitboxAnimated = false;
    public List<Vector3> allHitboxesAnimated = null;

    public bool autoCancel = false;
    public bool cancelable = false;
    public bool lastFrame = false;

    public bool canControl = false;
    public bool canFall = false;
    public Vector3 userMovement = Vector3.zero;
}
