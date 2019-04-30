using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHead : MonoBehaviour
{
    public int EnemySpeed;
    private int XMoveDirection = 0;
    public int myPDV;
    private float timerLeft;
    public float timer = 3;
    public AudioSource powerDownAudio, dieAudio;

    private void Start()
    {
        timerLeft = timer;
        while (XMoveDirection == 0)
            XMoveDirection = Random.Range(-1, 1);
    }

    void Update()
    {
        Move();
        IsLiving();
        IsAttacked();
    }

    void Move()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
    }

    void Attack(Collision2D c)
    {
        if (c.collider.GetComponent<PlayerHead>() && !(c.contacts[0].normal.y < -0.5f))
        {
            StartCoroutine(HeadGame.KillPlayer());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Flip();
        isAttacked(collision);
        Attack(collision);
    }

    void Flip()
    {
        if (XMoveDirection < 0)
        {
            XMoveDirection = 1;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            gameObject.transform.localScale = localScale;
        }
        else
        {
            XMoveDirection = -1;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            gameObject.transform.localScale = localScale;
        }
            
    }

    void isAttacked(Collision2D c)
    {
        if (c.gameObject.GetComponent<PlayerHead>() && c.contacts[0].normal.y < -0.5f)
        {
            gameObject.GetComponent<AudioSource>().Play();
            myPDV -= 5;
            EnemySpeed = 0;
            timerLeft = timer;
        }
    }
    void IsAttacked()
    {
        if (timerLeft > 0)
        {
            timerLeft -= Time.deltaTime;
        }
        else if (EnemySpeed == 0)
        {
            EnemySpeed = 1;
        }

    }

    void IsLiving()
    {
        if (myPDV <= 0)
        {
            HeadGame.PlayerScore += 50;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<EnemyHead>().enabled = false;
        }
    }

}
