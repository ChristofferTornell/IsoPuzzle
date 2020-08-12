using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPanicMaker : MonoBehaviour
{
    public PanicManager panicManager;
    [SerializeField] private float panicDecreasePerSecond;
    [SerializeField] private float panicPerDistanceMultiplier;
    [SerializeField] private float panicPerDistanceExponential;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            float distanceFromPlayer = Vector2.Distance(transform.position, collision.transform.position);
            collision.GetComponent<PlayerMovement>().ApplyPanic();
            panicManager.currentPanic -= ((panicDecreasePerSecond + Mathf.Pow(panicPerDistanceMultiplier, (Mathf.Pow(distanceFromPlayer, panicPerDistanceExponential)))) * Time.deltaTime);
        }
    }
}
