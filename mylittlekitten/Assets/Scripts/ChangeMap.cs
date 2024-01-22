using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ToInside"))
        {
            if (other.name == "BakeryPoint")
            {
                SceneManager.LoadScene("BakeryShop");
            }
            else if (other.name == "WhitePoint")
            {
                SceneManager.LoadScene("cafe_white_ver");
            }
            else if (other.name == "BrownPoint")
            {
                SceneManager.LoadScene("Garden");
            }
            else if (other.name == "ChristmasPoint")
            {
                SceneManager.LoadScene("Christmas");
            }
            else if (other.name == "BlackPoint")
            {
                SceneManager.LoadScene("cafe_black_ver");
            }
        }

        if (other.gameObject.CompareTag("ToOutside"))
        {
            print("ToOutside ÅÂ±×µÊ");
            SceneManager.LoadScene("MainMap");
        }

    }


}
