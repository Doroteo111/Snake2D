using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance { get; private set; }
    public Sprite snakeHeadSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.LogError(message: "There is more thsn one Instane");
        }
        Instance = this;
    }
}
