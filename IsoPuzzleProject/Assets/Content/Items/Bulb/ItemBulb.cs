using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ItemBulb : MonoBehaviour
{
    public float timeBeforeAbleToPickup;
    private bool ableToPickup;
    public CircleCollider2D colliderTrigger;

    public float glowDuration = 2f;
    public float fadeSpeed = 0.1f;
    public LightSourceInteractable myLight;

    private void Start()
    {
        Invoke("SetAbleToPickup", timeBeforeAbleToPickup); //FIX STRING
        Invoke("TurnOffLight", glowDuration); //FIX STRING

        //Makes the light radius equal to the radius that makes enemies scared of the light, giving the illusion that it's the light that is scaring the enemies.
        myLight.light2D.pointLightOuterRadius = colliderTrigger.radius;
    }

    private void TurnOffLight()
    {
        myLight.TurnOff(fadeSpeed);
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

    private void Update()
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
