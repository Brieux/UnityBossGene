using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowUpPower : MonoBehaviour
{
    private string nameEffet = "GrowUp";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            PlayerHead.launchEffet(gameObject, new Effet(nameEffet, 0,
            delegate ()
            {
                Vector2 localScale = PlayerHead.staticT.localScale;
                localScale.x *= 1.2f;
                localScale.y *= 1.2f;
                PlayerHead.staticT.localScale = localScale;
            }, delegate ()
            {
                Vector2 localScale = PlayerHead.staticT.localScale;
                localScale.x /= 1.2f;
                localScale.y /= 1.2f;
                PlayerHead.staticT.localScale = localScale;
            }));
        }

    }

    private void Update()
    {
        if (HeadGame.min.position.y > gameObject.transform.position.y)
            Destroy(gameObject);
    }
}
