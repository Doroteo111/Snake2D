using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using static SoundManager;

public static class SoundManager  // clase statica
{
    public enum Sound
    {
        ButtonClick,
        ButtonOver,
        SnakeDie,
        SmakeEat,
        SnakeMove
    }
    private static GameObject soundManagerGameObject; //hacerla global
    private static AudioSource audioSource;

    private static void CreateSoundManagerGameObject()
    {
        {
            if (soundManagerGameObject == null)
            {
                soundManagerGameObject = new GameObject("Sound Manager");
                audioSource = soundManagerGameObject.AddComponent<AudioSource>();
            }
            else
            {
                Debug.LogError("Sound Manager already exists");
            }
        }
    }

    public static void PlaySound( Sound sound) //por cada vez que creamos un sonido
    {
        //GameObject soundGameObject = new GameObject("Sound" + sound);
       // AudioSource audioSource= soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClipFromSound(sound));
    }
    private static AudioClip GetAudioClipFromSound(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAuioClipsArray) //que coincida e identifcar desde el array
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("sound" + sound + "not found");
        return null;
    }
}
