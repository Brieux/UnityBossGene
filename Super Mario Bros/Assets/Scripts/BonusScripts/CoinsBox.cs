using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=wi6sbOpYXIg&frags=pl%2Cwn

public class CoinsBox : MonoBehaviour
{
    public Material disabledMaterial;
    private Animator animator;
    public Material myMaterial;
    public int coinTotal;
    private int coinLeft;

    private void Start()
    {
        if (coinTotal <= 0)
        {
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = disabledMaterial;
        } else
        {
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = myMaterial;
        }
        
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        coinLeft = coinTotal;
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (coinLeft > 0)
        {
            if (c.collider.GetComponent<Player_Move_Prototype>() && c.contacts[0].normal.y > 0.5f)
            {
                gameObject.GetComponent<AudioSource>().Play();
                animator.SetTrigger("getCoin");
                HeadGame.PlayerScore += 100;
                coinLeft--;
                if(coinLeft <= 0)
                {
                    Desactive();
                }
            }
        }
    }

    private void Desactive()
    {
        gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = disabledMaterial;
        Debug.Log("Disabled");
    }
}