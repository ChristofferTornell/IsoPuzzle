using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanicBar : MonoBehaviour
{
    public Slider panicSlider;
    public PanicManager panicManager = null;

    void UpdatePanicUI(float panicPercent)
    {
        panicSlider.value = panicPercent;
    }

    public void OnPanicChange(float panicPercent)
    {
        UpdatePanicUI(panicPercent);
    }

    public void OnEnable()
    {
        panicManager.onPanicChange += OnPanicChange;
    }
    public void OnDisable()
    {
        panicManager.onPanicChange -= OnPanicChange;
    }
}