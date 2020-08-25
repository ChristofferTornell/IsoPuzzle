using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgePan : MonoBehaviour
{
    private CameraController cam = null;
    private BoxCollider2D myCollider = null;
    private float colliderDisableDuration = 0.1f;

    public enum PanDirection
    {
        south,
        west,
        east,
        north
    }

    public PanDirection panDirection = PanDirection.north;
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            if (CameraController.instance.currentlyPanning) { Debug.Log("error: currentlyPanning");  return; }

            //If the player collides with this, then move the player and pan the camera.
            PanCamera(panDirection);
            TransferPlayerToNewRoom(collision.transform, panDirection);

            //Disable collider temporarily to prevent bugs.
            myCollider.enabled = false;
            Invoke("EnableCollider", colliderDisableDuration);
        }
    }

    private void EnableCollider()
    {
        myCollider.enabled = true;
    }
    private void PanCamera(PanDirection _panDirection)
    {
        Vector3 _cameraEdgePanAmount = CameraController.instance.cameraValues.cameraEdgePanAmount;
        Vector2 _cameraEdgePanAmountFilter = new Vector3(0f, 0f);

        switch (_panDirection)
        {
            case PanDirection.south:
                
                _cameraEdgePanAmountFilter.y = -_cameraEdgePanAmount.y;
                break;
            case PanDirection.west:
                _cameraEdgePanAmountFilter.x = -_cameraEdgePanAmount.x;
                break;
            case PanDirection.east:
                _cameraEdgePanAmountFilter.x = _cameraEdgePanAmount.x;
                break;
            case PanDirection.north:
                _cameraEdgePanAmountFilter.y = _cameraEdgePanAmount.y;
                break;
            default:
                break;
        }
        cam.minPosition += _cameraEdgePanAmountFilter;
        cam.maxPosition += _cameraEdgePanAmountFilter;
    }
    private void TransferPlayerToNewRoom(Transform _transform, PanDirection _panDirection)
    {
        Vector3 _playerEdgeMoveAmount = CameraController.instance.cameraValues.playerEdgeMoveAmount;
        Vector3 _playerEdgeMoveAmountFilter = new Vector3(0f, 0f, 0f);

        switch (_panDirection)
        {
            case PanDirection.south:
                _playerEdgeMoveAmountFilter.y = -_playerEdgeMoveAmount.y;
                break;
            case PanDirection.west:
                _playerEdgeMoveAmountFilter.x = -_playerEdgeMoveAmount.x;
                break;
            case PanDirection.east:
                _playerEdgeMoveAmountFilter.x = _playerEdgeMoveAmount.x;
                break;
            case PanDirection.north:
                _playerEdgeMoveAmountFilter.y = _playerEdgeMoveAmount.y;
                break;
            default:
                break;
        }

        _transform.position += _playerEdgeMoveAmountFilter;
    }
}
