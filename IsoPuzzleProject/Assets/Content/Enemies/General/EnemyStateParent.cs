using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateParent
{
    protected EnemyParent enemy;

    public virtual void _OnValidate(EnemyParent _enemy)
    {
        this.enemy = _enemy;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
    public virtual void FixedUpdate()
    {

    }
    public virtual void Update()
    {

    }

    public virtual bool AnActiveBulbIsBulbTooClose()
    {
        ItemBulb bulb = enemy.FindItemBulb();
        if (bulb != null && bulb.myLight.lightActive && Vector3.Distance(bulb.transform.position, enemy.transform.position) <= enemy.rangeToCheckBulbInner)
        {
            enemy.fleeBulb = bulb.transform;
            enemy.Transit(enemy.fleeState);
            return true;
        }
        return false;
    }
    public virtual bool AnActiveBulbIsNearby()
    {
        ItemBulb bulb = enemy.FindItemBulb();
        if (bulb != null && bulb.myLight.lightActive && Vector3.Distance(bulb.transform.position, enemy.transform.position) <= enemy.rangeToCheckBulbOuter)
        {
            return true;
        }
        return false;
    }

}
