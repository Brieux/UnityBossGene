using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHead : MonoBehaviour
{

    private static List<Effet> effetsList;
    public CharacterController2D controller2D;
    public AudioSource coinAudio, jumpAudio, powerUpAudio, powerDownAudio, dieAudio;
    private bool facingRight = true;
    private bool vMove = false;
    private float hMove = 0f;
    public float speed = 40f;
    public static Transform staticT;
    public Transform t;
    private Vector3 savePosition;
    public static AudioSource staticDieAudio;

    public static AudioSource DieAudio { get => staticDieAudio; set => staticDieAudio = value; }
    public static List<Effet> EffetsList { get => effetsList; set => effetsList = value; }
    
    private void Start()
    {
        staticDieAudio = dieAudio;
        EffetsList = new List<Effet>();
        staticT = t;
    }

    void Update()
    {
        Move();
        ExecuteEffets();
    }

    private void ExecuteEffets()
    {
        if (EffetsList.Count > 0)
        {
            foreach (Effet e in new List<Effet>(EffetsList))
            {
                if (e.enable)
                {
                    e.UpdateEffet();
                }

                if (!e.enable || (e.timerLeft <= 0f && e.timer > 0))
                {
                    e.StopEffet();
                    EffetsList.Remove(e);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if ((hMove <= 0f && t.position.x - 1 < HeadGame.min.position.x) || (hMove >= 0f && t.position.x + 1 > HeadGame.max.position.x))
        {
            hMove = 0;
        }
        controller2D.Move(hMove * Time.fixedDeltaTime * speed, false, vMove);
        vMove = false;
    }

    void Move()
    {
        hMove = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jumpAudio.Play();
            //gameObject.GetComponent<AudioSource>().Play();
            vMove = true;
        }

        if (hMove < 0.0f && !facingRight)
        {
            flipPlayer();
        }
        else if (hMove > 0.0f && !facingRight)
        {
            flipPlayer();
        }
    }

    void flipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public static void launchEffet(GameObject g, Effet effet)
    {

        Destroy(g);

        foreach (Effet e in new List<Effet>(EffetsList))
        {
            if (e.nameEffet == effet.nameEffet)
            {

                if (e.nameEffet.Equals("Star"))
                {
                    e.enable = false;
                    effet.timer += e.timerLeft;
                    effet.timerLeft += e.timerLeft;
                }
                else if (e.nameEffet.Equals("GrowUp"))
                {
                    
                    return;
                }
                e.StopEffet();
                effetsList.Remove(e);
            }
        }
        EffetsList.Add(effet);
        effet.StartEffet();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin" || collision.gameObject.tag == "paper")
        {
            coinAudio.Play();
            HeadGame.PlayerScore += 100;
            Destroy(collision.gameObject);
        }
    }

}
