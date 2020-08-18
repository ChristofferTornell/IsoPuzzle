using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyStateIdle : EnemyStateParent
{
    [Header("Stats")]
    public float moveSpeedPatrolling;
    private float waypointCheck = 0.2f;

    private Transform target;

    public override void Update()
    {
        //Checks for bulb
        AnActiveBulbIsBulbTooClose();

    }
}
