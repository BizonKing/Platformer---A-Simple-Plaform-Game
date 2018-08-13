using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Takes care of the start menu interactions
 * Mainly starting and closing the game
 */
public class StartCanvas : MonoBehaviour {

    public GameObject mainMenu, loadingScreen;
    
    // Starts game
    public void OnStartClick()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    // Closes to desktop
    public void OnQuitClick()
    {
        Application.Quit();
    }
}
