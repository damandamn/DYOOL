using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    public PlayerController user;
    public SphereCollider hitbox;
    public bool isActive = false;
    public Attack attack;

    public int priority;
    public Collider hit;

    public string hitboxType = "Attack";
    public Vector3 offset;
    public float radius;
    float height;

    public float damage;
    public float baseKnockback;
    public float growthKnockback;
    public float angle;
    public int hitlag;
    public float hitStunMultiplier = 1;

    public bool reverseHit = false;

    void Start()
    {
        attack = user.currAttack;
        gameObject.tag = "Hitbox";

        hitbox = gameObject.AddComponent<SphereCollider>();
        hitbox.center = new Vector3(-offset.x, offset.y);
        hitbox.radius = radius;
        if (hitboxType == "Projectile")
        {
            hitbox.enabled = true;
        } else
        {
            hitbox.enabled = false;
        }
    }

    private void Update()
    {
        isActive = hitbox.enabled;
    }


    void OnTriggerStay(Collider other)
    {
        //is this collider another player?
        if (other.gameObject != user.gameObject && other.gameObject.tag == "Player")
        {
            //has this player already been hit by this attack?
            if (other.GetComponent<PlayerController>().nullify.Contains(attack))
            {
                return;
            }
            else
            {
                hit = other;
                user.pam.PrioritizeHit(this);
            }
        }
    }
}
