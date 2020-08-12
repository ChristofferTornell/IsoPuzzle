using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerStateNormal : PlayerStateParent
{
    private Vector3 inputChange;

    [Header("Stats")]
    public float moveSpeed;
    private float glowCounter = 0f;

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        CheckInputs();
    }
    public override void FixedUpdate()
    {
        AnimateAndMovePlayer();
    }

    private void CheckInputs()
    {
        //Handles player input
        inputChange = Vector3.zero;
        inputChange.x = Input.GetAxis("Horizontal");
        inputChange.y = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            AttemptThrowBulb();
        }
    }
    private void AnimateAndMovePlayer()
    {
        //Animates and moves player depending on input calculations from other method
        if (inputChange != Vector3.zero)
        {
            MovePlayer();
            player.animator.SetFloat("moveX", inputChange.x);
            player.animator.SetFloat("moveY", inputChange.y);
            player.animator.SetBool("isMoving", true);
            player.TurnOnLight();
            glowCounter = 0;
        }
        else
        {
            player.animator.SetBool("isMoving", false); //TODO could be optimized to not call the animator if its already false
            if (glowCounter < player.glowDuration) { glowCounter += Time.deltaTime; }
            else { player.TurnOffLight(); }
        }
    }

    private void MovePlayer()
    {
        player.rigidBody.MovePosition(player.transform.position + inputChange * moveSpeed * Time.deltaTime);
    }

    private void AttemptThrowBulb()
    {
        if (!player.hasBulb) { return; }
        player.Transit(player.chargeThrowState);
    }
}
