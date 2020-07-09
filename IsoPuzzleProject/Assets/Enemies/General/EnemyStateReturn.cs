using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyStateReturn : EnemyStateParent
{
    [SerializeField] private float moveSpeedReturn_ = 0.5f;
    [HideInInspector] public Vector3 target;
    public float waypointCheck = 0.2f;
    [HideInInspector] public Vector3 targetLocation;

    public override void Enter()
    {
        target = enemy.transform.position - enemy.spawnLocation;
        targetLocation = enemy.spawnLocation;
    }
    public override void Update()
    {
        GoToLocation(target);
        if (Vector3.Distance(enemy.transform.position, targetLocation) <= waypointCheck)
        {
            OnFoundLocation();
        }
        if (IsBulbTooClose())
        {
            UpdateTargetPosition();
        }
    }

    public void UpdateTargetPosition()
    {

    }
    public void GoToLocation(Vector3 _targetLocation)
    {
        enemy.transform.Translate(_targetLocation.normalized * moveSpeedReturn_ * Time.deltaTime, Space.World);
    }
    public virtual void OnFoundLocation()
    {
        enemy.Transit(enemy.idleState);
    }
}
