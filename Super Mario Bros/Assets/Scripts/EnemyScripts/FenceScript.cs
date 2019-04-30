using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 direction = transform.position - collision.gameObject.transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {

            if (direction.x > 0) { print("collision is to the right"); }
            else { print("collision is to the left"); }

        }
        else
        {

            if (direction.y > 0) { print("collision is up"); }
            else
            {
                StartCoroutine(HeadGame.KillPlayer());
            }

        }



    }
}
