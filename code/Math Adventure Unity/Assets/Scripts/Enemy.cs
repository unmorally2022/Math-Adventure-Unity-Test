using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [HideInInspector]
    public Hero hero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void DoAttack()
    {
        if (characterState == CharacterState.live) { 
            if (!hero.GetIsDeactive())
            {
                base.DoAttack();
                hero.DoGetHit(AttackDamage);
            }
        }
    }
}
