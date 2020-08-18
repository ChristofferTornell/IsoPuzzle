using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Door
{
    public GameObject gateDoor = null;
    public override void Open()
    {
        if (isOpen) { return; }
        isOpen = true;
        gateDoor.SetActive(false);
    }

    public override void Close()
    {
        if (!isOpen) { return; }
        isOpen = false;
        gateDoor.SetActive(true);
    }
}
