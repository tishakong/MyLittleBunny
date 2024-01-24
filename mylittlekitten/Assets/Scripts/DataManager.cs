using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    WhiteRabbit, BlackRabbit, BrownRabbit
}

public class DataManager : MonoBehaviour
{
    public int myCarrot; //당근1개에 코인 5개
    public int myFish; //물고기1마리에 코인 2개
    public int myCoin;
    public static DataManager Instance;
    public Character currentCharacter;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        myCarrot = 0;
        myFish = 0;
        myCoin = 0;
    }


}
