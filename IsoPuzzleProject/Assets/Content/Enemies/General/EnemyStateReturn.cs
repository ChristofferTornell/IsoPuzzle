using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyStateReturn : EnemyStateParent
{
    [SerializeField] private float moveSpeedReturn_ = 0.5f;
    [HideInInspector] public Vector3 targetDirection;
    public float waypointCheck = 0.2f;
    [HideInInspector] public Vector3 targetLocation;

    public override void Enter()
    {
        //Target is direction
        targetDirection = enemy.spawnLocation - enemy.transform.position;

        //Targetlocation is the location to check if reached
        targetLocation = enemy.spawnLocation;
    }
    public override void Update()
    {
        GoInDirection(targetDirection);

        //If found target location
        if (Vector3.Distance(enemy.transform.position, targetLocation) <= waypointCheck)
        {
            OnFoundLocation();
        }

        //If bulb is too close
        if (AnActiveBulbIsBulbTooClose())
        {
            UpdateTargetPosition();
        }
    }

    public void UpdateTargetPosition()
    {

    }
    public void GoInDirection(Vector3 _targetDirection)
    {
        enemy.transform.Translate(_targetDirection.normalized * moveSpeedReturn_ * Time.deltaTime, Space.World);
    }
    public virtual void OnFoundLocation()
    {
        enemy.Transit(enemy.idleState);
    }
}
