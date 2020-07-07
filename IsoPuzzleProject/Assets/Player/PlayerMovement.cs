using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector3 mousePositionRelativeToPlayer = Vector3.zero;
    public bool hasBulb;
    public PlayerStateNormal normalState = new PlayerStateNormal();
    public PlayerStateChargeThrow chargeThrowState = new PlayerStateChargeThrow();
    private PlayerStateParent currentState = null;

    private void Start()
    {
        hasBulb = true;
        currentState = normalState;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
       
    }

    public void OnValidate()
    {
        normalState.OnValidate(this);
        chargeThrowState.OnValidate(this);
    }

    public void Transit(PlayerStateParent targetState)
    {
        currentState.Exit();
        currentState = targetState;
        currentState.Enter();
    }

    public void ThrowBulb(GameObject _bulbPrefab, float _bulbThrowPower, Vector3 spawnLocation)
    {
        GameObject bulbObj = Instantiate(_bulbPrefab, spawnLocation, Quaternion.identity);
        Vector2 directionToThrow = mousePositionRelativeToPlayer.normalized;
        bulbObj.GetComponent<Rigidbody2D>().AddForce(directionToThrow * _bulbThrowPower);
        hasBulb = false;
    }

    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    public void Update()
    {
        currentState.Update();
    }
}
