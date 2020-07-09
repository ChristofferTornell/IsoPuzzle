using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : EnemyParent
{
    [Header ("Patrol State")]
    public EnemyPatrollingStatePatrolling patrollingState = new EnemyPatrollingStatePatrolling();
    public EnemyPatrollingStateReturn returnStatePatroll = new EnemyPatrollingStateReturn();

    [HideInInspector] public int waypointIndex = 0;

    public Transform[] waypoints;

    public override void Start()
    {
        currentState = patrollingState;
        currentState.Enter();
    }

    public override void OnValidate()
    {
        standState._OnValidate(this);
        patrollingState._OnValidate(this);
        fleeState._OnValidate(this);
        returnStatePatroll._OnValidate(this);
    }

}
