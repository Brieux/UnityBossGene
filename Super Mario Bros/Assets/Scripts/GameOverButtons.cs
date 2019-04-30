using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button8) || Input.GetKeyUp(KeyCode.Q))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            NewGame();
        }
    }

    public void NewGame()
    {   
        HeadGame.LivesLeft = 4;
        HeadGame.PlayerScore = 0;
        SceneManager.LoadScene("Niveau_1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
