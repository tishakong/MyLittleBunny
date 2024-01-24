using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void ChangeScenefunc(){
        switch (this.gameObject.name)
        {
            case "Start_button":
                SceneManager.LoadScene("SelectCharacter");
                break;

            case "Select_button":
                SceneManager.LoadScene("MainMap");
                break;
        }

        audioManager.PlaySound("UIButtonClick");

    }
    
}
