using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreUI : MonoBehaviour
{
    //reminder limbreria tmpro
    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateScoreText (int score)
    {
        scoreText.text=score.ToString(); //actualizar el contenido y hay que llamarla cada vez q ganamos puntos
    }
    
}
