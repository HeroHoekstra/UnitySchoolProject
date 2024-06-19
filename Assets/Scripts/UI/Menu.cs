using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject howToMenu;

    public void ShowMainMenu()
    {
        howToMenu.SetActive(false);
        mainMenu.SetActive(true);
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

    public Image fadeImage;
    private Fading fading;

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

    private IEnumerator StartGameCoroutine(float seconds)
    {
        fadeImage.gameObject.SetActive(true);
        fading.fadeDuration = seconds;

        yield return StartCoroutine(fading.FadeToBlack(0));

        SceneManager.LoadScene("Game");
    }
}
