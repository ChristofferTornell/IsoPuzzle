using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyStateStand : EnemyStateParent
{
    //public float stateDuration;
    //private float stateCounter;

    public override void Update()
    {
        if (!AnActiveBulbIsBulbTooClose() && !AnActiveBulbIsNearby())
        {
            if (enemy.GetComponent<EnemyPatrolling>() != null) { enemy.Transit(enemy.GetComponent<EnemyPatrolling>().returnStatePatroll); }
            else { enemy.Transit(enemy.returnState); }
        }
    }
}
