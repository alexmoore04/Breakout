using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private GameObject GameOverUI;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameOverUI.activeSelf == false)
        {
            rb.velocity = new Vector2(0, 0);
            horizontalInput = Input.GetAxisRaw("Horizontal");

            Vector3 direction = new Vector3(horizontalInput, 0, 0);
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
