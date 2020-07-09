using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EnemyPatrollingStatePatrolling : EnemyPatrollingStateParent
{
    [Header("Stats")]
    public float moveSpeedPatrolling;
    private float waypointCheck = 0.2f;

    private Transform target;
    public override void Enter()
    {
        if (enemyPatrolling.waypointIndex >= enemyPatrolling.waypoints.Length) { enemyPatrolling.waypointIndex = 0; }
        target = enemyPatrolling.waypoints[enemyPatrolling.waypointIndex];
    }

    public override void Update()
    {
        Vector3 moveDirection = target.position - enemy.transform.position;
        enemy.transform.Translate(moveDirection.normalized * moveSpeedPatrolling * Time.deltaTime, Space.World);

        if (Vector3.Distance(enemy.transform.position, target.position) <= waypointCheck)
        {
            NextWaypoint();
        }
        IsBulbTooClose();
    }
    void NextWaypoint()
    {
        enemyPatrolling.waypointIndex++;
        if (enemyPatrolling.waypointIndex >= enemyPatrolling.waypoints.Length) { enemyPatrolling.waypointIndex = 0; }
        target = enemyPatrolling.waypoints[enemyPatrolling.waypointIndex];
    }
    
}
