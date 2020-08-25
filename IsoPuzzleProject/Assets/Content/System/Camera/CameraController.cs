using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform followTarget = null;
    public Vector2 minPosition, maxPosition;
    public CameraValuesSO cameraValues = null;
    public static CameraController instance = null;
    [HideInInspector] public bool currentlyPanning = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        followTarget = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //Follow the player as long as its not clamped between min/max values.
        if (transform.position != followTarget.position)
        {
            Vector3 followTargetPosition = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);

            followTargetPosition.x = Mathf.Clamp(followTargetPosition.x, minPosition.x, maxPosition.x);
            followTargetPosition.y = Mathf.Clamp(followTargetPosition.y, minPosition.y, maxPosition.y);


            transform.position = Vector3.Lerp(transform.position, followTargetPosition, cameraValues.smoothingValue);
        }
    }
}
