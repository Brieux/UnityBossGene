using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeadGame : MonoBehaviour
{
    private float timeLeft = 180f;
    private float warningTime = 30;
    private bool switcher = true;
    private Color saveColor;
    private float pas = 1f;

    public bool dead, small;
    private static int playerScore;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;
    public GameObject cornerMin;
    public GameObject cornerMax;
    public static Transform min;
    public static Transform max;
    public GameObject[] hearts;
    public GameObject player;
    private List<GameObject> lifes;
    public static int nbLifes;
    public AudioSource deadAudio;
    public AudioSource powerDownAudio;
    public AudioSource pauseAudio;
    public static bool isPaused;
    public GameObject pausePanel;
    public static AudioSource staticAudio,staticPowerDown;

    public static AudioSource DeadAudio { get => staticAudio; set => staticAudio = value; }
    public static AudioSource PowerDownAudio { get => staticPowerDown; set => staticPowerDown = value; }
    public static int LivesLeft { get => nbLifes; set => nbLifes = value; }
    public static int PlayerScore { get => playerScore; set => playerScore = value; }
    public static bool Pause { get => isPaused; set => isPaused = value; }
    private void Start()
    {
        player.SetActive(true);
        staticAudio = deadAudio;
        staticPowerDown = powerDownAudio;
        dead = false;
        small = false;
        isPaused = false;
        pausePanel.SetActive(false);
        min = cornerMin.transform;
        max = cornerMax.transform;

        InvokeRepeating("HurryUp",timeLeft-warningTime, 1f);
        saveColor = timeLeftUI.gameObject.GetComponent<Text>().color;

        int lifeLeft = PlayerPrefs.GetInt("lifes");

        lifes = new List<GameObject>();

        foreach (GameObject g in hearts)
        {
            g.SetActive(true);
            lifes.Add(g);
        }

        Debug.Log(lifeLeft);

        if (lifeLeft == 0)
        {
            nbLifes = lifes.Count + 1;
        }
        else
        {
            nbLifes = lifeLeft;
            int i = lifes.Count - 1;

            for (; i >= nbLifes - 1; i--)
            {
                lifes[i].SetActive(false);
                lifes.RemoveAt(i);
            }
        }
    }


    void Update()
    {
        if(DeadAudio.isPlaying == true)
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
        if(PowerDownAudio.isPlaying == true)
        {
            gameObject.GetComponent<AudioSource>().Pause();
        } else
        {
            if (gameObject.GetComponent<AudioSource>().isPlaying == false)
                gameObject.GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseAudio.Play();
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            
          // if (pauseAudio.isPlaying == false)
            //{
                if (Input.GetKeyUp(KeyCode.Keypad0))
                {
                    SceneManager.LoadScene("Main_menu");
                }

                pausePanel.SetActive(true);
                Time.timeScale = 0;
            //}
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            timeLeft -= Time.deltaTime;

            timeLeftUI.gameObject.GetComponent<Text>().text = "Time left: " + (int)timeLeft;
            playerScoreUI.gameObject.GetComponent<Text>().text = "Score: " + PlayerScore;

            VerifyLiving();
        }

        float EPSILON = 0.5f;

        if(System.Math.Abs(timeLeft - 10f) < EPSILON || System.Math.Abs(timeLeft - 20f) < EPSILON)
        {
            CancelInvoke("HurryUp");
            if(System.Math.Abs(timeLeft - 20f) < EPSILON)
            {
                InvokeRepeating("HurryUp", 0.5f, 0.5f);
            }
            else
            {
                InvokeRepeating("HurryUp", 0.25f, 0.25f);
            }
        }
    }

    void HurryUp()
    {
        if (switcher) 
        {
            timeLeftUI.gameObject.GetComponent<Text>().color = Color.red;
        }
        else
        {
            timeLeftUI.gameObject.GetComponent<Text>().color = saveColor;
        }

        switcher = !switcher;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EndLevel")
        {
            CountScore();
        }
        if (collision.gameObject.name == "Coin")
        {
            playerScore += 100;
            Destroy(collision.gameObject);
        }

    }

    void VerifyLiving()
    {

        if ((player.transform.position.y < cornerMin.transform.position.y) || (timeLeft < 0f))
        {
            gameObject.GetComponent<AudioSource>().Stop();
            //KillPlayer();
                if (!dead)
                {
                    dead = true;
                    StartCoroutine(Die());
                }
        }

    }




    public IEnumerator Die()
    {
        GameObject player = GameObject.Find("Player");
        player.SetActive(false);
        DeadAudio.PlayOneShot(DeadAudio.clip);
        yield return new WaitForSeconds(DeadAudio.clip.length);
        PlayerScore = 0;
        nbLifes--;

        PlayerPrefs.DeleteKey("lifes");

        if (nbLifes <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            PlayerPrefs.SetInt("lifes", nbLifes);
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static IEnumerator KillPlayer()
    {
        

        if (PlayerHead.EffetsList.Count > 0)
        {
            PowerDownAudio.PlayOneShot(PowerDownAudio.clip);
            //yield return new WaitForSeconds(PowerDownAudio.clip.length);
            Effet e = PlayerHead.EffetsList[PlayerHead.EffetsList.Count - 1];
            e.StopEffet();
            PlayerHead.EffetsList.Remove(e);
        }
        else
        {
            GameObject player = GameObject.Find("Player");
            player.SetActive(false);
            DeadAudio.PlayOneShot(DeadAudio.clip);
            yield return new WaitForSeconds(DeadAudio.clip.length);
            PlayerScore = 0;

            nbLifes--;

            PlayerPrefs.DeleteKey("lifes");

            if (nbLifes <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                PlayerPrefs.SetInt("lifes", nbLifes);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void CountScore()
    {
        HeadGame.PlayerScore += (int)(timeLeft * 10);
        if (DataManagement.dataManagement.highScore <= HeadGame.PlayerScore)
        {
            DataManagement.dataManagement.highScore = HeadGame.PlayerScore;
        }
        Debug.Log("Player score = " + PlayerScore);
    }
}
