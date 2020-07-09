using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingStateParent : EnemyStateParent
{
    [HideInInspector] public EnemyPatrolling enemyPatrolling;
    public override void _OnValidate(EnemyParent _enemy)
    {
        this.enemy = _enemy;
        this.enemyPatrolling = enemy.GetComponent<EnemyPatrolling>();
    }

}
