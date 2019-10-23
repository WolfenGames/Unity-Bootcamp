using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHit : MonoBehaviour
{
    CharacterCombat combatManager;
    AnimationManager animationManager;
    void Start()
    {
        combatManager = GetComponentInParent<CharacterCombat>();
        animationManager = GetComponentInParent<AnimationManager>();
    }

    public void HitPlayer()
    {
        bool attack = animationManager.animator.GetBool("attack");

        if (attack)
        {
            //Get damage of AI
            //Pass through to the damage to the player
            combatManager.doDamage();
            animationManager.animator.SetBool("attack", false);
        }
    }
}
