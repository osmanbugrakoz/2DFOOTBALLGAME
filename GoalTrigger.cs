using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class GoalTrigger : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip goalCLip;
    public AudioSource AudioSource;

    [Header("UI")]
    public Image goalImage;

    [Header("Goal Settings")]
    public string ballTag = "Ball";
    public bool isLeftGoal; // Sol kale mi? inspectordan ayarla
    public GoalManager goalManager; // Inspectordan ayarla

    public float disableForSeconds = 1f; // art arda tetiklemesini engellemek i�in (opsiyonel)
    public float imageShowTime = 2f; // Gol resmi ne kadar s�reyle g�r�ns�n
    public float fadeDuration = 0.5f; // Ne kadar s�rede yava��a g�r�ns�n/kaybolsun

    bool recentlyTriggered = false;

    void Awake()
    {
        if (AudioSource == null)
        {
            AudioSource = GetComponent<AudioSource>();
        }
        if (goalImage != null)
        {
            // ba�ta g�r�nmez
            Color c = goalImage.color;
            c.a = 0f;
            goalImage.color = c;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (recentlyTriggered) return;

        if (other.CompareTag(ballTag))
        {
            if (AudioSource != null && goalCLip != null) // ses �al
            {
                AudioSource.PlayOneShot(goalCLip);
            }

            if (goalImage != null) // gol resmini g�ster
            {
                StartCoroutine(ShowGoalImage());
            }
            // GoalManager�a bildir
            if (goalManager != null)
            {
                goalManager.OnGoalScored(isLeftGoal);
            }
        
            if (disableForSeconds > 0f)
                StartCoroutine(ResetTriggerAfterSeconds(disableForSeconds));
        }
    }

    IEnumerator ShowGoalImage()
    {
        // Fade in
        yield return StartCoroutine(FadeImage(0f, 1f, fadeDuration));

        // Bekle
        yield return new WaitForSeconds(imageShowTime);

        // Fade out
        yield return StartCoroutine(FadeImage(1f, 0f, fadeDuration));
    }
    IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color c = goalImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            goalImage.color = c;
            yield return null;
        }

        // kesin biti� de�eri ayarla
        c.a = endAlpha;
        goalImage.color = c;
    }
    IEnumerator ResetTriggerAfterSeconds(float secs)
    {
        recentlyTriggered = true;
        yield return new WaitForSeconds(secs);
        recentlyTriggered = false;
    }
    
}
