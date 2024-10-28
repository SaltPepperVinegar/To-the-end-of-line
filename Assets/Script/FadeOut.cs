using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{

    public GameObject GameObject1;
    private Image image;
    public float fadeDuration = 1.0f;           
    public float waitTime = 0.5f;               

    private void Start()
    {
        image = GameObject1.GetComponent<Image>();
        StartCoroutine(FadeAlpha(image, 1, 0, fadeDuration));
    }



    private IEnumerator FadeAlpha(Image text, float startAlpha, float endAlpha, float duration)
    {
        yield return new WaitForSeconds(0.5f);

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
        gameObject.SetActive(false);
    }
}
