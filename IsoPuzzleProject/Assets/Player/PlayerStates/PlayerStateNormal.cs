using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerStateNormal : PlayerStateParent
{
    private Vector3 inputChange;

    [Header("Stats")]
    public float moveSpeed;

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        CheckInputs();
        AnimateAndMovePlayer();
    }

    private void CheckInputs()
    {
        inputChange = Vector3.zero;
        inputChange.x = Input.GetAxis("Horizontal");
        inputChange.y = Input.GetAxis("Vertical");
        player.animator.SetFloat("moveX", inputChange.x);
        player.animator.SetFloat("moveY", inputChange.y);

        if (Input.GetMouseButtonDown(0))
        {
            AttemptThrowBulb();
        }
    }
    private void AnimateAndMovePlayer()
    {
        if (inputChange != Vector3.zero)
        {
            MovePlayer();
            player.animator.SetBool("isMoving", true);
        }
        else
        {
            player.animator.SetBool("isMoving", false);
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
