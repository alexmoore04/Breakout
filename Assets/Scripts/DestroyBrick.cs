using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBrick : MonoBehaviour
{
    private GameManager gameManager;

    private AudioSource brickBreak;

    private void Start()
    {
        gameManager = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
        brickBreak = (AudioSource)GameObject.Find("BrickHit").GetComponent("AudioSource");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            gameObject.SetActive(false);
            gameManager.ScoreUpdate();
            brickBreak.Play();
        }
    }
}
