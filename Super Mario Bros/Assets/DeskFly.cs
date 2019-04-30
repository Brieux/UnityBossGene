using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskFly : MonoBehaviour
{
    public int Speed;
    public float distance;
    public bool vertically = true;
    public int Direction;
    private float distanceLeft;
    public bool yo_yo;

    private void Start()
    {

        distanceLeft = distance;
    }

    void Update()
    {
        Move();

        if (yo_yo)
        {
            distanceLeft -= Time.fixedDeltaTime * Speed;//TODO

            if (distanceLeft <= 0)//TODO
            {
                Direction *= -1;
                distanceLeft = distance;
            }
        }
    }

    void Move()
    {
        RaycastHit2D hit;

        if (vertically) { 
            hit = Physics2D.Raycast(transform.position, new Vector2(0, Direction));
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Direction) * Speed;
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, new Vector2( Direction,0));
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Direction,0) * Speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.GetComponent<PlayerHead>())
        {
            Direction *= -1;

            if (yo_yo)
                yo_yo = !yo_yo;
        }
    }

}
