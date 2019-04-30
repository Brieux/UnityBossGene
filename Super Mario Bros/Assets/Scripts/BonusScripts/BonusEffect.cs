using UnityEngine;

public class BonusEffect : MonoBehaviour
{
    public float timeEffet;
    public string nameEffet;

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