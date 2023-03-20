using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("GameMenu");
    }

    public void QuitButton() {
        Application.Quit();
        Debug.Log("Game closed");
    }
}
