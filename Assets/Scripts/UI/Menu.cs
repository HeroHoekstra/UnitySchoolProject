using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject howToMenu;

    public Image fadeImage;
    private Fading fading;

    // Button functions
    public void ShowMainMenu()
    {
        howToMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowHowTo()
    {
        howToMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine(0.5f));
    }

    private void Start()
    {
        // Set background to transparent
        fading = GetComponent<Fading>();
        fading.fadeImage = fadeImage;
        fading.fadeDuration = 0;

        StartCoroutine(fading.FadeToTransparent(0));
        fadeImage.gameObject.SetActive(false);

        // Sometimes the GameManeger is multiplied.
        // This removes all extra GameManagers
        bool found = false;
        foreach (GameObject gM in GameObject.FindGameObjectsWithTag("EditorOnly"))
        {
            if (gM.name == "MainManager")
            {
                if (!found)
                {   
                    found = true;
                    continue;
                }

                Destroy(gM);
            }
        }
    }

    // This handles starting the game and fading the black image
    private IEnumerator StartGameCoroutine(float seconds)
    {
        fadeImage.gameObject.SetActive(true);
        fading.fadeDuration = seconds;

        yield return StartCoroutine(fading.FadeToBlack(0));

        SceneManager.LoadScene("Game");
    }
}
