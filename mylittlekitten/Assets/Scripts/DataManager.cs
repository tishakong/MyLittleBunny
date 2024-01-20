using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Character
{
    WhiteRabbit, BlackRabbit
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    public Character currentCharacter;

}
