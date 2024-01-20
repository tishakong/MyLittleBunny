using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeScenefunc(){
        switch (this.gameObject.name)
        {
            case "Start_button":
                SceneManager.LoadScene("SelectCharacter");
                break;
        }

    }
    
}
