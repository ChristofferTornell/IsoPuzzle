using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EnemyStateFlee : EnemyStateParent
{
    public float moveSpeedFlee;
    private Vector3 target = new Vector3();
    public float waypointCheck = 0.2f;
    public float distanceFromBulb = 0.5f;

    public override void Enter()
    {
        UpdateTargetPosition();
    }
    public override void Update()
    {
        enemy.transform.Translate(target * moveSpeedFlee * Time.deltaTime, Space.World);
        if (enemy.fleeBulb != null && Vector3.Distance(enemy.transform.position, enemy.fleeBulb.position) >= distanceFromBulb)
        {
            enemy.Transit(enemy.standState);
            return;
        }
        if (IsBulbTooClose())
        {
            UpdateTargetPosition();
        }
        if (enemy.fleeBulb == null) 
        {
            if (enemy.GetComponent<EnemyPatrolling>() != null) { enemy.Transit(enemy.GetComponent<EnemyPatrolling>().returnStatePatroll); }
            else { enemy.Transit(enemy.returnState); }
        }
    }

    public void UpdateTargetPosition()
    {
        if (enemy.fleeBulb == null)
        {
            return;
        }
        target = enemy.fleeBulb.position - enemy.transform.position;
        target = target.normalized * -1;
    }
}
