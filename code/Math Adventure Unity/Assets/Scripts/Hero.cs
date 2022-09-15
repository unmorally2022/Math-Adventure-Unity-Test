using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    [HideInInspector]
    public Enemy[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void DoAttack()
    {
        if (characterState == CharacterState.live)
        {
            List<Enemy> _enemy = new List<Enemy>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (!enemies[i].GetIsDeactive())
                    _enemy.Add(enemies[i]);
            }

            if (_enemy.Count > 0)
            {
                base.DoAttack();

                int TargetIndex = Random.Range(0, _enemy.Count);
                //if the enemy is deactivated then increase the damage attack by 5
                if (_enemy[TargetIndex].DoGetHit(AttackDamage))
                {
                    AttackDamage += 5;
                }
            }
            _enemy.Clear();
        }
    }
    

}
