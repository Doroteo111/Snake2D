using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button backMainButton;
    public static PauseUI Instance { get; private set; }
    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance");
        }

        Instance = this;
        Hide();

        resumeButton.onClick.AddListener(() => { GameManager.Instance.ResumeGame(); });
        backMainButton.onClick.AddListener(() => { Time.timeScale = 1f; // devuelve el tiempo a como estaba antes
                                                  Loader.Load(Loader.Scene.MainMenu); });
        
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
