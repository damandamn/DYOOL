using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundHandler : MonoBehaviour {

    public Platform nextPlat;
    public float platHeight;
    public Collider col;

    public LayerMask platforms;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    public void UpdatePlatform()
    {
        nextPlat = GetCurrentPlat();
        if (nextPlat != null)
        {
            platHeight = nextPlat.height;
        } else
        {
            platHeight = -30;
        }
        
    }

    public Platform GetCurrentPlat()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position - new Vector3(0, 1.4F), Vector3.down);

        if (Physics.Raycast(ray, out hit, 40, platforms))
        {
            GameObject go = hit.collider.gameObject;
            if (go.tag == "Stage" && go.transform.position.y <= transform.position.y)
            {
                foreach (Platform p in StageData.activePlatforms)
                {
                    if (p.plat == go)
                    {
                        return p;
                    }
                }
            }
        }

        return null;
    }


}
