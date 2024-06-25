using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class ExitLevel : MonoBehaviour
{
    public TerrainSettings ts;
    public BoatSpawnSettings bSettings;

    private GameObject player;

    private Fading fading;

    private bool firstEnter = true;
    private bool holdInBoat = false;

    public float acceleration = 1.0f;
    private float timeElapsed = 0.0f;

    private void Start()
    {
        fading = GetComponent<Fading>();
    }

    private void Update()
    {
        // Hold the player in the boat by sticking it to the boat and disabling movement
        if (holdInBoat)
        {
            player.transform.position = gameObject.transform.position;

            timeElapsed += Time.deltaTime;
            float speedFactor = acceleration * timeElapsed;
            transform.position += new Vector3(speedFactor * transform.position.x, 0, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Leave when the player enters the trigger again
       if (other.tag == "Player" && !firstEnter)
        {
            player = other.gameObject;
            StartCoroutine(ExitIsland(other.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Make sure the player doesn't inmediatly leave
        if (other.tag == "Player")
        {
            firstEnter = false;
        }
    }

    private IEnumerator ExitIsland(GameObject player)
    {
        holdInBoat = true;
        player.GetComponent<Movement>().enabled = false;

        // Fade
        fading.fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(fading.FadeToBlack(0.5f));

        // Reload scene
        GameObject.Find("MainManager").GetComponent<GameManager>().NextLevelData();
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
