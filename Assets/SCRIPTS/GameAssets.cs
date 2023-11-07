using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameAssets : MonoBehaviour
{
    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
    public static GameAssets Instance { get; private set; }

    public Sprite snakeHeadSprite;
    public Sprite foodSprite;
    public Sprite snakeBodySprite;

    public SoundAudioClip[] soundAuioClipsArray;
   
    /*public AudioClip buttonClickClip;
    public AudioClip buttonOverClip;
    public AudioClip snakeDieClip;
    public AudioClip sneekEatClip; 
    public AudioClip snakeMoveClip;   HACEMOS LISTA */

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance");
        }

        Instance = this;
    } 
}
