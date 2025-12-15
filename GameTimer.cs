using System.Collections;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
public class GameTimer : MonoBehaviour
{
    [Header("Timer UI")]
    public TMP_Text timerText;

    [Header("Game Over UI")]
    public TMP_Text gameOverText; // Zaman bitince game over+ skor ve kazanan� g�sterir.
     
    [Header("Timer Settings")]
    public float matchDuration = 10f;

    private float remainingTime;
    private GoalManager goalManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainingTime = matchDuration;
        UpdateTimerUI();

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false); // Ba�ta gizli olsun
        }
        StartCoroutine(TimerCountdown());
        //GoalManager referans�n� al
        goalManager = FindAnyObjectByType<GoalManager>();
    }

    private IEnumerator TimerCountdown()
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
            UpdateTimerUI();
        }
        remainingTime = 0f;
        UpdateTimerUI();
        EndMatch();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void EndMatch()
    {
        // Skorlar� GoalManagerden al
        int leftScore = goalManager.GetLeftScore();
        int rightScore = goalManager.GetRightScore();

        // Kazanan� belirle
        string result;
        if (leftScore > rightScore)
        {
            result = "PLAYER LEFT Kazand�";
        }
        else if (rightScore > leftScore)
        {
            result = " PLAYER RIGHT Kazand�";
        }
        else
        {
            result = "BERABERE";
        }

        //UI da g�ster
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "OYUN B�TT�!\n" + leftScore + "-" + rightScore + "\n" + result;
        }
    }

}


