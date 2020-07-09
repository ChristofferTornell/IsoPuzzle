using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class EnemyPatrollingStateReturn : EnemyStateReturn
{
    public override void Enter()
    {
        enemy.GetComponent<EnemyPatrolling>().waypointIndex = 0;
        float closestDistance = Mathf.Infinity;
        foreach(Transform waypoint in enemy.GetComponent<EnemyPatrolling>().waypoints)
        {
            if (Vector3.Distance(waypoint.position, enemy.transform.position) < closestDistance)
            {
                enemy.GetComponent<EnemyPatrolling>().waypointIndex++;

                closestDistance = Vector3.Distance(waypoint.position, enemy.transform.position);
                target = enemy.transform.position - waypoint.position;
                target = target.normalized * -1;
                targetLocation = waypoint.position;
            }
        }
    }
    public override void OnFoundLocation()
    {
        enemy.Transit(enemy.GetComponent<EnemyPatrolling>().patrollingState);
    }
}
