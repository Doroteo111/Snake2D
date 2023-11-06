using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
    private ScoreUI scoreUIScript;

    public const int POINTS = 100; //constantes en mayus
    private int score; // puntuación jugador
   private LevelGrid levelGrid;
   private Snake snake;
    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance");
        }

        Instance = this;
    }
    private void Start()
    {
        // Configuración de la cabeza de serpiente
        GameObject snakeHeadGameObject = new GameObject("Snake Head");
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.Instance.snakeHeadSprite;
        snake = snakeHeadGameObject.AddComponent<Snake>();

        // Configurar el LevelGrid
        levelGrid = new LevelGrid(20, 20);
       
        //intercambian informacion vvvv
        snake.Setup(levelGrid); 
        levelGrid.Setup(snake);

        scoreUIScript = GetComponentInChildren<ScoreUI>();
        score = 0;
        AddScore(0);
    }
    public int GetScore()    // de lectura
    {
        return score;
    }

    public void AddScore (int pointsToAdd)
    {
        score += pointsToAdd; 
        scoreUIScript.UpdateScoreText(score);
    }

    public void SnakeDied()  // aseguramos que enseña el panel , lo llamamos en el snake cuando muere (en STATE)
    {
        GameOverUI.Instance.Show(); 
    }
}
