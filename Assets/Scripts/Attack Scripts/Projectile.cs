using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public PlayerController user;
    Vector3 useVelocity;
    public Vector3 velocity;
    public Vector3 offset;
    public Quaternion rotation;
    public int duration;
    public bool reflectable = true;

    int framesElapsed = 0;

    private void Start()
    {
        if (user.isFacingLeft)
        {
            useVelocity = new Vector3(-velocity.x, velocity.y);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            useVelocity = new Vector3(velocity.x, velocity.y);
        }
    }

    // Update is called once per frame
    void Update () {
        if (framesElapsed >= duration)
        {
            Destroy(gameObject);
        }
        transform.Translate(useVelocity);
        framesElapsed++;
	}
}
