using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBulb : MonoBehaviour
{
    public float timeBeforeAbleToPickup;
    private bool ableToPickup;


    private void Start()
    {
        Invoke("SetAbleToPickup", timeBeforeAbleToPickup);
    }

    private void SetAbleToPickup()
    {
        ableToPickup = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ableToPickup && collision.GetComponent<PlayerMovement>() != null)
        {
            collision.GetComponent<PlayerMovement>().hasBulb = true;
            Destroy(gameObject);
        }
    }
}
