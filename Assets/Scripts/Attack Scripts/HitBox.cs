using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    public PlayerController user;
    public SphereCollider hitbox;
    public bool isActive = false;
    public Attack attack;

    public AudioSource AS;
    public AudioClip hitSound;
    public float hitSoundVolume = 1F;

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
        hitbox.isTrigger = true;
        hitbox.center = new Vector3(-offset.x, offset.y);
        hitbox.radius = radius;
        if (hitboxType == "Projectile" || hitboxType == "Collateral")
        {
            hitbox.enabled = true;
        } else
        {
            hitbox.enabled = false;
        }
        
        //Weird solution but I cant find a better one: creates a separate object to play sound, which is deleted after 5 seconds
        //Or at the end of playing its sound clip
        if (hitSound != null)
        {
            AS = new GameObject().AddComponent<AudioSource>();
            AS.clip = hitSound;
            AS.volume = hitSoundVolume;
            Destroy(AS.gameObject, 5);
        }
    }

    private void Update()
    {
        isActive = hitbox.enabled;
    }

    public void PlaySound()
    {
        AS.pitch = Random.Range(0.9F, 1.1F);
        AS.Play();
        Destroy(AS.gameObject, hitSound.length);
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
        else
        if (other.gameObject.tag == "Hitbox")
        {
            if (attack.destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
    }
}
