using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.SceneView;

public class Fading : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration;

    private void Start()
    {
        if (fadeImage == null)
        {
            Debug.LogWarning("Could not assign fade image. Trying default...");
            fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
            if (fadeImage == null ) 
            {
                Debug.LogError("Could not assign fade image");
            }
        }
    }

    public IEnumerator FadeToBlack(float fadeOffset)
    {
        yield return StartCoroutine(FadeImage(fadeImage, fadeImage.color.a, 1f, fadeOffset));
    }

    public IEnumerator FadeToTransparent(float fadeOffset)
    {
        yield return StartCoroutine(FadeImage(fadeImage, fadeImage.color.a, 0f, fadeOffset));
    }

    private IEnumerator FadeImage(Image img, float startAlpha, float endAlpha, float fadeOffset)
    {
        yield return new WaitForSeconds(fadeOffset);

        float elapsedTime = 0f;
        Color color = img.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            img.color = color;

            yield return null;
        }

        color.a = endAlpha;
        img.color = color;
    }
}
