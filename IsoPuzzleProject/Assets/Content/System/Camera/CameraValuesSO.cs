using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

public class CameraValuesSO : ScriptableObject
{
    public float smoothingValue = 0f;
    public Vector2 cameraEdgePanAmount;
    public Vector3 playerEdgeMoveAmount;
}
