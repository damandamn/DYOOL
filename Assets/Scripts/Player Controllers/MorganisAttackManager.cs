using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorganisAttackManager : PlayerAttackManager {

    public override void UseSpecialAttack(PlayerController user)
    {
        //stick is neutral
        if (Mathf.Abs(user.vert) < user.vertThreshold && Mathf.Abs(user.hori) < user.horiThreshold)
        {
            StartAttack(user, user.neutralBAttack);
        }
        else
        //stick is up
        if (user.vert > user.vertThreshold)
        {
            if (user.vert >= Mathf.Abs(user.hori))
            {
                if (user.upBUsed < user.upBAttack.aerialUses)
                {
                    user.upBUsed++;
                    if (user.isGrounded)
                    {
                        StartAttack(user, user.upBAttack);
                    }
                    else
                    {
                        StartAttack(user, user.upBAttackAerial);
                    }
                }
            }
            else
            //Stick is diagonally up
            {

            }
        }
        else
        //stick is left or right
        if (user.hori >= user.horiThreshold )
        {
            if (user.sideBUsed < user.sideBAttack.aerialUses)
            {
                user.sideBUsed++;
                user.isFacingLeft = false;
                StartAttack(user, user.sideBAttack);
            }

        } else if (user.hori <= user.horiThreshold)
        {
            if (user.sideBUsed < user.sideBAttack.aerialUses)
            {
                user.sideBUsed++;
                user.isFacingLeft = true;
                StartAttack(user, user.sideBAttack);
            }
        }
    }

}
