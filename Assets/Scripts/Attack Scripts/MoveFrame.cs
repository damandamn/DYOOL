using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFrame {

    public bool playSound = false;
    public AudioClip frameSound = null;

    public bool startupFrame = false;
    public bool endlagFrame = false;

    public bool hitboxActive = false;
    public List<bool> allHitboxesActive = null;

    public bool hitboxAnimated = false;
    public List<Vector3> allHitboxesAnimated = null;

    public bool reHit = false;
    public bool autoCancel = false;
    public bool cancelable = true;
    public bool lastFrame = false;
    public Vector3 userMovement = Vector3.zero;
    public Vector3 userGroundMovement = Vector3.zero;
    public bool iFrame = false;

    //for special moves
    public bool spawnProjectile = false;
    public bool canControl = false;
    public bool canFall = false;
    public GameObject projectile;
    public bool setAirMomentum = false;
}
