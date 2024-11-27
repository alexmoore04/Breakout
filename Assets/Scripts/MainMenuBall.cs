using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBall : MonoBehaviour
{
    private Rigidbody2D rb;

    public AudioSource hit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(10, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit.Play(); 
    }
}
