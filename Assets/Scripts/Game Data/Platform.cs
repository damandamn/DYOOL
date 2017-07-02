using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform {

    public GameObject plat;
    public float height;

    public Platform (float h, GameObject p)
    {
        height = h;
        plat = p;
    }

}
