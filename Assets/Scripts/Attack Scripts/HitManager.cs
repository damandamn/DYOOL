using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour {

	public void CalculateHit(HitBox attack, PlayerController user, PlayerController enemy)
    {
        enemy.currDamage += attack.damage;
        float knockbackValue = 0;
        float growth = (enemy.currDamage / 100);
        float multiplier = 2 - (enemy.weight / 100);

        float angle = CalculateAngle(attack, user, enemy);



        //forces enemy to face attacker
        if (user.transform.position.x < enemy.transform.position.x)
        {
            enemy.isFacingLeft = true;
        } else
        {
            enemy.isFacingLeft = false;
        }

        float modifiedKBG = attack.growthKnockback / 2;
        //Formula (from left to right) = (mKBG * enemydamage/100) * (2 - weight/100) * (1 + attackdamage/100) + BKB
        //30 BKB, 100 KBG, 10 damage, 100 percent and 100 weight on enemy = 140
        //30 kbk, 10 kbg, 3 damage, 10 percent and 100 weight on enemy = 40
        knockbackValue += (modifiedKBG * growth);
        knockbackValue *= (2 - (enemy.weight / 100));
        knockbackValue *= 1 + (attack.damage / 100);
        knockbackValue += attack.baseKnockback;

        Debug.Log(knockbackValue);

        //hitstun duration formula. Improve on this! 
        int hitstunDuration;
        if (knockbackValue >= 50) {
            hitstunDuration = 20 + ((int)knockbackValue / 2);
        } else
        {
            hitstunDuration = 1 + (int)knockbackValue;
        }
        StartCoroutine(enemy.Hitlag(attack.hitlag + (int)attack.damage, knockbackValue, hitstunDuration, angle));
        StartCoroutine(user.Hitlag(attack.hitlag + (int)attack.damage, knockbackValue, hitstunDuration, angle, true));
    }

    //Called at the end of hitlag
    public Vector3 CalculateLaunch(float knockbackValue, float angle, float fallspeed)
    {
        float kbX = 0;
        float kbY = 0;

        float launchSpeed = knockbackValue / 300;

        //Switching sin and cos then multiplying the y speed by -1 made it work
        kbX = launchSpeed * Mathf.Cos((angle / 180) * Mathf.PI);
        kbY = launchSpeed * Mathf.Sin((angle / 180) * Mathf.PI);

        float multiplier = ((fallspeed / 0.007F) - 1) / 1.5F;
        kbY *= 1 + multiplier;

        Vector3 knockback = new Vector3(kbX, kbY);
        return knockback * 1.5F;
    }

    //Alters angle based on directional influence, reverse hit, etc
    float CalculateAngle(HitBox attack, PlayerController user, PlayerController enemy)
    {
        float angle = attack.angle;
        //reverses angle if attacker is reversed
        if (!user.isFacingLeft)
        {
            angle = 180 - angle;
        }
        
        //reverses angle if it is reversable and the enemy is behind the user
        if(attack.reversable)
        {
            if (user.isFacingLeft)
            {
                if (user.transform.position.x > enemy.transform.position.x)
                {
                    angle = 180 - angle;
                }
            } else
            {
                if (user.transform.position.x < enemy.transform.position.x)
                {
                    angle = 180 - angle;
                }
            }
        }

        return angle;
    }

}
