using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class PlayerStateChargeThrow : PlayerStateParent
{
    [Header("Stats")]
    public float bulbThrowPower;
    private float stateCounter = 0;
    public float timeBeforePowerIncreaseBegins;
    public float bulbThrowPowerPerSecond;
    private float statePowerCounter = 0;
    public float timeBeforeExitState;

    [Space]
    [Header("Insertables")]
    public GameObject bulbPrefab;
    public GameObject powerBarCanvas;
    public Slider powerBarSlider;

    public override void Enter()
    {
        player.animator.SetBool("isMoving", false);
        player.animator.SetBool("isThrowing", true);
        powerBarSlider.value = 0;
        powerBarCanvas.SetActive(true);
    }

    public override void Exit()
    {
        player.animator.SetBool("isThrowing", false);
        stateCounter = 0;
        statePowerCounter = 0;
        powerBarCanvas.SetActive(false);
    }
    public override void Update()
    {
        UpdateTimer();
        CheckInputs();
        AnimatePlayer();
    }

    void UpdateTimer()
    {
        stateCounter += Time.deltaTime;
        if (stateCounter >= timeBeforePowerIncreaseBegins)
        {
            statePowerCounter += Time.deltaTime;
            powerBarSlider.value = statePowerCounter / (timeBeforeExitState - timeBeforePowerIncreaseBegins);

        }
        if (stateCounter >= timeBeforeExitState)
        {
            player.ThrowBulb(bulbPrefab, bulbThrowPower + bulbThrowPowerPerSecond*statePowerCounter);
            player.Transit(player.normalState);
        }
    }

    void CheckInputs()
    {
        if (Input.GetMouseButtonUp(0))
        {
            player.ThrowBulb(bulbPrefab, bulbThrowPower + bulbThrowPowerPerSecond*statePowerCounter);
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
