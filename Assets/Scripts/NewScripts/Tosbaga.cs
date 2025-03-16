using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tosbaga : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Mevcut düþüþ hýzýný sýfýrla
                rb.AddForce(Vector2.up * bounceForce + Vector2.left * bounceForce * 1.5f, ForceMode2D.Impulse);
            }
        }
    }
}