using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateUIManager : MonoBehaviour
{
    public static StateUIManager instance;
    public Image carrotImage;
    public Image fishImage;
    public Image coinImage;
    public TextMeshProUGUI carrotText;
    public TextMeshProUGUI fishText;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        // 이미 인스턴스가 있는 경우 중복 생성을 방지하기 위해 현재 씬에서 파괴
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 처음 생성된 경우 현재 씬에서 파괴되지 않도록 설정
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "SelectCharacter")
        {
            gameObject.SetActive(false);
        }
        carrotText.text = DataManager.Instance.myCarrot.ToString();
        fishText.text = DataManager.Instance.myFish.ToString();
        coinText.text = DataManager.Instance.myCoin.ToString();
    }
}
