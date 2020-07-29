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
        //Initiates state by validating that the next target waypoint is in bounds of the waypoint array
        if (enemyPatrolling.waypointIndex >= enemyPatrolling.waypoints.Length) { enemyPatrolling.waypointIndex = 0; }
        target = enemyPatrolling.waypoints[enemyPatrolling.waypointIndex];
    }

    public override void Update()
    {
        //Moves towards the target waypoint
        Vector3 moveDirection = target.position - enemy.transform.position;
        enemy.transform.Translate(moveDirection.normalized * moveSpeedPatrolling * Time.deltaTime, Space.World);

        // Gets a new target waypoint every time it reaches the target waypoint
        if (Vector3.Distance(enemy.transform.position, target.position) <= waypointCheck)
        {
            NextWaypoint();
        }

        //Checks for bulb
        IsBulbTooClose();
    }
    void NextWaypoint()
    {
        //Validates that the next waypoint is in bounds of the waypoint array. Goes back to the 0th waypoint if the check reaches past the last element of array.
        enemyPatrolling.waypointIndex++;
        if (enemyPatrolling.waypointIndex >= enemyPatrolling.waypoints.Length) { enemyPatrolling.waypointIndex = 0; }
        target = enemyPatrolling.waypoints[enemyPatrolling.waypointIndex];
    }
    
}
