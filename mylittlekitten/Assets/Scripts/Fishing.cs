using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fishing : MonoBehaviour
{
    public int pressCount = 0; 

    public TextMeshProUGUI fishingProgress;

    void Start()
    {
        fishingProgress = GameObject.Find("FishingProgress").GetComponent<TextMeshProUGUI>();
        // UpdateUI(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pressCount < 20)
            {
                pressCount ++;
                Debug.Log(pressCount);
                // UpdateUI(); 
            }
            else
            {
                Debug.Log("물고기를 획득했습니다!");
            }
        }
    }

    // void UpdateUI()
    // {
    //     float percentage = (float)pressCount / 20 * 100;
    //     fishingProgress.text = "물고기 획득까지: " + percentage.ToString("F1") + "%";
    // }
}

