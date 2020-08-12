using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector3 mousePositionRelativeToPlayer = Vector3.zero;
    public PlayerStateNormal normalState = new PlayerStateNormal();
    public PlayerStateChargeThrow chargeThrowState = new PlayerStateChargeThrow();
    private PlayerStateParent currentState = null;
    [Header("General Insertables")]
    public PanicManager panicManager;
    public LightSourceInteractable headLight;
    private bool HasBulb;

    [Header("Light Stats")]
    public float lightFadeSpeed = 1f;
    public float glowDuration = 2f;

    [Header("Panic Stats")]
    private bool recentlyPanicked;
    private float recentlyPanickedCounter = 0;
    [SerializeField] private float stopPanicDelay = 1f;
    [SerializeField] private float panicRegenerationPerSecond = 1f;
    [SerializeField] private float panicDecreaseInDarknessPerSecond = 1f;
    public bool hasBulb
    {
        get { return HasBulb;}
        set
        {
            //Everytime hasBulb is set, it either turns off headLight or activates it.
            HasBulb = value;
            if (!HasBulb) { TurnOffLight(); }
            else { TurnOnLight(); }
        }
    }

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
        //if (Input.GetKeyDown(KeyCode.A)) { panicManager.currentPanic -= 0.5f; }
        currentState.Update();
        PanicHandler();
    }

    public void ApplyPanic()
    {
        recentlyPanicked = true;
        recentlyPanickedCounter = 0;
    }

    private void OnPanicDecrease(float amount)
    {
        ApplyPanic();
    }

    private void PanicHandler()
    {
        RecentlyPanickedHandler();
        DarknessPanicHandler();
    }

    private void DarknessPanicHandler()
    {
        if (!headLight.lightActive)
        {
            panicManager.currentPanic -= panicDecreaseInDarknessPerSecond * Time.deltaTime;
        }
    }


    private void RecentlyPanickedHandler()
    {
        if (recentlyPanicked)
        {
            recentlyPanickedCounter += Time.deltaTime;
        }
        if (recentlyPanickedCounter > stopPanicDelay)
        {
            recentlyPanicked = false;
            recentlyPanickedCounter = 0;
        }
        if (!recentlyPanicked && panicManager.currentPanic < panicManager.basePanic)
        {
            panicManager.currentPanic += panicRegenerationPerSecond * Time.deltaTime;
        }
    }

    public void TurnOffLight()
    {
        headLight.TurnOff(lightFadeSpeed);
    }
    public void TurnOnLight()
    {
        headLight.TurnOn(lightFadeSpeed);
    }

    public void OnEnable()
    {
        panicManager.onPanicDecrease += OnPanicDecrease;
    }
    public void OnDisable()
    {
        panicManager.onPanicDecrease -= OnPanicDecrease;
    }
}
