using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextMeshProFader : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshProArray;  // Array of TextMeshPro objects
    public float fadeDuration = 1.0f;           // Time it takes to fade in and out
    public float waitTime = 0.5f;               // Time to wait between fade in and fade out
    public AudioSource audioSource;

    private void Start()
    {
            StartCoroutine(FadeTextAlpha());
    }

    private IEnumerator FadeTextAlpha()
    {
        foreach (var text in textMeshProArray)
        {   
            yield return StartCoroutine(FadeAlpha(text, 0, 1, fadeDuration));

            // Wait
            yield return new WaitForSeconds(waitTime);
            
            // Fade Out
            yield return StartCoroutine(FadeAlpha(text, 1, 0, fadeDuration));

            yield return new WaitForSeconds(waitTime);

        }
        yield return StartCoroutine(FadeOutAudio(audioSource,fadeDuration));
        SceneManager.LoadScene(2);
    }

    private IEnumerator FadeAlpha(TextMeshProUGUI text, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = text.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            text.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure final alpha value is set correctly at the end
        text.color = new Color(color.r, color.g, color.b, endAlpha);
    }

    private IEnumerator FadeOutAudio(AudioSource source, float duration)
    {
        float startVolume = source.volume;  // Get the starting volume

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Lerp the volume from startVolume to 0 over the fadeDuration
            source.volume = Mathf.Lerp(startVolume, 0, elapsed / duration);

            // Wait until the next frame
            yield return null;
        }

        // Ensure the volume is set to 0 at the end
        source.volume = 0;
        source.Stop();  // Optional: Stop the audio when the volume reaches 0
    }

}
