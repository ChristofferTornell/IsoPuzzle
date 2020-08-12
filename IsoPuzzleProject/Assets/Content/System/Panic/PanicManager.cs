using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu()]

public class PanicManager : ScriptableObject
{
    [HideInInspector] public float basePanic = 1;
    private float CurrentPanic = 1;
    public UnityAction<float> onPanicChange = delegate { };
    public UnityAction<float> onPanicDecrease = delegate { };
    private void OnEnable()
    {
        currentPanic = basePanic;
    }
    public float currentPanic
    {
        get
        {
            return CurrentPanic;
        }
        set
        {
            if (CurrentPanic != value)
            {
                if (CurrentPanic > value)
                {
                    onPanicDecrease(CurrentPanic - value);
                }
                CurrentPanic = value;
                
                onPanicChange(CurrentPanic/basePanic);
                if (CurrentPanic > basePanic)
                {
                    CurrentPanic = basePanic;
                }
                if (CurrentPanic < 0)
                {
                    CurrentPanic = 0;
                }
            }
        }
    }
}
