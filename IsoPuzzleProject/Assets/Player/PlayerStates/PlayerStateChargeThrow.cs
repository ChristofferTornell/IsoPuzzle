using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class PlayerStateChargeThrow : PlayerStateParent
{
    [Header("Stats")]
    public float bulbThrowPower;

    [Space]
    [Header("Insertables")]
    public GameObject bulbPrefab;

    public override void Enter()
    {
        player.animator.SetBool("isMoving", false);
        player.animator.SetBool("isThrowing", true);
    }

    public override void Exit()
    {
        player.animator.SetBool("isThrowing", false);
    }
    public override void Update()
    {
        CheckInputs();
        AnimatePlayer();
    }

    void CheckInputs()
    {
        if (Input.GetMouseButtonUp(0))
        {
            player.ThrowBulb(bulbPrefab, bulbThrowPower);
            player.Transit(player.normalState);
        }
        if (Input.GetMouseButtonDown(1))
        {
            player.Transit(player.normalState);
        }
    }

    void AnimatePlayer()
    {
        //Checking mouse position relative to player
        Vector2 mouseScreenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        player.mousePositionRelativeToPlayer = mouseWorldPosition - (Vector2)player.transform.position;

        //Changing face direction of player character
        player.animator.SetFloat("mouseX", player.mousePositionRelativeToPlayer.x);
        player.animator.SetFloat("mouseY", player.mousePositionRelativeToPlayer.y);
    }
}
