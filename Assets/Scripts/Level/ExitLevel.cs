using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public TerrainSettings ts;
    public BoatSpawnSettings bSettings;

    private GameObject player;

    private ClearLevel cl;
    private Fading fading;

    private bool firstEnter = true;
    private bool holdInBoat = false;

    public float acceleration = 1.0f;
    private float timeElapsed = 0.0f;

    private void Start()
    {
        cl = GetComponent<ClearLevel>();
        fading = GetComponent<Fading>();

        cl.Init();
    }

    private void Update()
    {
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
       if (other.tag == "Player" && !firstEnter)
        {
            player = other.gameObject;
            StartCoroutine(ExitIsland(other.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            firstEnter = false;
        }
    }

    private IEnumerator ExitIsland(GameObject player)
    {
        holdInBoat = true;
        player.GetComponent<Movement>().enabled = false;

        fading.fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(fading.FadeToBlack(0.5f));

        // Reload scene
        GameObject.Find("MainManager").GetComponent<GameManager>().NextLevelData();
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
