using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Instance");
        }

        Instance = this;
        //suscribir metodo al evento OnHighScoreChange
        Score.OnHighScoreChange += Score_OnHighScoreChange;
    }
    private void OnDisable()
    {
        Score.OnHighScoreChange -= Score_OnHighScoreChange;
    }
    private void Score_OnHighScoreChange (object sender, EventArgs e)
    {
        //lo que tien que ocurrir al llamarse el evento (se tiene q repintar)
        UpdateHighScoreText ();
    }

    public void UpdateHighScoreText()
    {
        int highScore = Score.GetHighScore();
        highScoreText.text = highScore.ToString();
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
 
    }

}
