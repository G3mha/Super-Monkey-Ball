// Inspired by: https://gamedevacademy.org/unity-start-menu-tutorial/#Unity_Menu_%E2%80%93_Part_1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButtons : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnContinueButton()
    {
        SceneManager.LoadScene("Minigame");
    }
}
