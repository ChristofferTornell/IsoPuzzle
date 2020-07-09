using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public EnemyStateFlee fleeState = new EnemyStateFlee();
    public EnemyStateStand standState = new EnemyStateStand();
    public EnemyStateReturn returnState = new EnemyStateReturn();
    public EnemyStateIdle idleState = new EnemyStateIdle();

    [HideInInspector] public EnemyStateParent currentState = null;
    [HideInInspector] public Vector3 spawnLocation;


    [HideInInspector] public Transform fleeBulb;
    public float rangeToCheckBulbInner;
    public float rangeToCheckBulbOuter;

    public virtual void Start()
    {
        spawnLocation = transform.position;
        currentState = idleState;
    }
    public void Transit(EnemyStateParent targetState)
    {
        currentState.Exit();
        currentState = targetState;
        //Debug.Log("to state: " + targetState);
        currentState.Enter();
    }

    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    public void Update()
    {
        currentState.Update();
    }

    public virtual void OnValidate()
    {

        idleState._OnValidate(this);
        fleeState._OnValidate(this);
        returnState._OnValidate(this);
        standState._OnValidate(this);
    }
    public ItemBulb FindItemBulb()
    {
        return FindObjectOfType<ItemBulb>();
    }
}
