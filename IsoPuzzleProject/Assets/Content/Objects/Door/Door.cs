﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector] public bool isOpen = false;
    public virtual void Open()
    {
        if (isOpen) { return; }
        isOpen = true;
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public virtual void Close()
    {
        if (!isOpen) { return; }
        isOpen = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
