using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{

    public static ExitManager Instance;
    AudioManager audioManager;
    public Button ExitButton;
    public Button ReStartButton;
    bool isOn;
    public bool isRestart;

    private void Awake()
    {
        isRestart = false;
        isOn = false;
        if (Instance == null) Instance = this;
        else if (Instance != null) return;
        DontDestroyOnLoad(gameObject);
        ExitButton.gameObject.SetActive(false);
        ReStartButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            audioManager.PlaySound("UIButtonClick");
            isOn = !isOn;
        }

        if (!isOn)
        {
            ReStartButton.gameObject.SetActive(false);
            ExitButton.gameObject.SetActive(false);
        }
        else
        {
            ReStartButton.gameObject.SetActive(true);
            ExitButton.gameObject.SetActive(true);
        }
    }


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        ExitButton.onClick.AddListener(() => Exitfunc(ExitButton.name));
        ReStartButton.onClick.AddListener(() => Exitfunc(ReStartButton.name));
    }

    public void Exitfunc(string buttonName)
    {
        audioManager.PlaySound("UIButtonClick");
        switch (buttonName)
        {
            case "ExitButton":
                Application.Quit();
                break;

            case "RestartButton":
                isRestart = true; isOn = !isOn;
                SceneManager.LoadScene("Start");
                break;
        }
    }
}
