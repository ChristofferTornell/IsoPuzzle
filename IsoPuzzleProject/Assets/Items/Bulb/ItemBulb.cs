using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBulb : MonoBehaviour
{
    public float timeBeforeAbleToPickup;
    private bool ableToPickup;
    public CircleCollider2D colliderTrigger;


    private void Start()
    {
        Invoke("SetAbleToPickup", timeBeforeAbleToPickup);
        InvokeRepeating("CheckPickup", 0, 0.1f);
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

    private void CheckPickup()
    {
        if (!ableToPickup)
        {
            return;
        }
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (Vector3.Distance(player.transform.position, this.transform.position) <= colliderTrigger.radius * transform.localScale.x)
        {
            player.hasBulb = true;
            Destroy(gameObject);
        }
    }
}
