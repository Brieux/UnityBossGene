using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPower : MonoBehaviour
{
    private float timeEffet = 10;
    private string nameEffet = "Star";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerHead.launchEffet(gameObject, new Effet(nameEffet, timeEffet,
            delegate ()
            {
                Vector2 localScale = PlayerHead.staticT.localScale;
                localScale.x *= 1.5f;
                localScale.y *= 1.5f;
                PlayerHead.staticT.localScale = localScale;
            }, delegate ()
            {
                Vector2 localScale = PlayerHead.staticT.localScale;
                localScale.x /= 1.5f;
                localScale.y /= 1.5f;
                PlayerHead.staticT.localScale = localScale;
            }));
        }

    }
}
