using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("firstLaunch", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Niveau_1");
        HeadGame.PlayerScore = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {

        Vector3 pos = new Vector3(0f, 0f, 0f);
        GameObject.Find("Player").transform.position = pos;
        GameObject.Find("Player").GetComponent<Player_Move_Prototype>().enabled = true;
        SceneManager.LoadScene("Niveau_2");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            NextLevel();
            //PlayGame();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            QuitGame();
        }

    }
}
