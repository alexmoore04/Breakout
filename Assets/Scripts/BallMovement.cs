using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BallMovement : MonoBehaviour
{
    public int lives = 3;
    [SerializeField] private float ballVelocity;
    private float currentVelocity;

    private float timer;
    private bool respawned = true;

    private int collisionCounter;
    private Rigidbody2D rb;

    [SerializeField] private UnityEvent _onDeath;

    public AudioSource hit;
    public AudioSource Dead;

    [SerializeField] private TMP_Text livesText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(0, -ballVelocity);
        currentVelocity = ballVelocity;
    }
    void Update()
    {
        if (respawned == true)
        {
            if (rb.velocity.magnitude > currentVelocity)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, currentVelocity);
            }

            if (rb.velocity.magnitude < currentVelocity)
            {
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -currentVelocity);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, currentVelocity);
                }
            }
        }
        else
        {
            if (timer <= Time.time)
            {
                respawned = true;
                rb.velocity = new Vector2(0, -ballVelocity);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Brick")
        {
            hit.Play();
            collisionCounter++;
            if (collisionCounter == 6)
            {
                collisionCounter = 0;
                currentVelocity++;
            }
        }
        if (collision.gameObject.tag == "Death")
        {
            Dead.Play();
            lives--;
            if (lives == 0)
            {
                _onDeath?.Invoke();
            }
            else
            {
                livesText.text = "Lives: " + lives;
                RestartBall();
            }
        }
    }

    public void RestartBall()
    {
        collisionCounter = 0;
        respawned = false;
        timer = Time.time + 3;
        currentVelocity = ballVelocity;
        transform.position = new Vector2(0, 0);
        rb.velocity = new Vector2(0, 0);
    }
}
