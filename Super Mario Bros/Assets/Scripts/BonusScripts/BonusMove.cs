using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMove : MonoBehaviour
{

    public int BonusSpeed;
    private int XMoveDirection = 0;

    private void Start()
    {

        while (XMoveDirection == 0)
            XMoveDirection = Random.Range(-1, 1);
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * BonusSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Flip();
    }

    void Flip()
    {
        if (XMoveDirection < 0)
            XMoveDirection = 1;
        else
            XMoveDirection = -1;
    }
}
