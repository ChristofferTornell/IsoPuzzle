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
        //Initiates by trying to find the furthest distance from the bulb
        UpdateTargetPosition();
    }
    public override void Update()
    {
        //Tries to find the furthest distance from the bulb every frame, unless the bulb is far away, thus going to stand state. Distance from bulb is how far it will run. 
        enemy.transform.Translate(target * moveSpeedFlee * Time.deltaTime, Space.World);
        if (AnActiveBulbIsNearby())
        {
            enemy.Transit(enemy.standState);
        }
        else if (AnActiveBulbIsBulbTooClose())
        {
            UpdateTargetPosition();
        }
        else 
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
