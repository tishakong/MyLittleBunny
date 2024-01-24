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
        // �̹� �ν��Ͻ��� �ִ� ��� �ߺ� ������ �����ϱ� ���� ���� ������ �ı�
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ó�� ������ ��� ���� ������ �ı����� �ʵ��� ����
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
