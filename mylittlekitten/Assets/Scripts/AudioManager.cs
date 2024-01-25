using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip ADWatering;
    public AudioClip ADFishingStart;
    public AudioClip ADFishing;
    public AudioClip ADBuySuccess;
    public AudioClip ADHarvest;
    public AudioClip ADGetBread;
    public AudioClip ADUIButtonClick;
    public AudioClip ADOpenBox;
    public AudioClip ADGetTreasure;
    public AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        GameObject go = new GameObject("AD" + action);
        audioSource = go.AddComponent<AudioSource>();
        DontDestroyOnLoad(go);

        switch (action)
        {
            case "Watering":
                audioSource.clip = ADWatering; break;
            case "FishingStart":
                audioSource.clip = ADFishingStart; break;
            case "Fishing":
                audioSource.clip = ADFishing; break;
            case "BuySuccess":
                audioSource.clip = ADBuySuccess; break;
            case "Harvest":
                audioSource.clip = ADHarvest; break;
            case "GetBread":
                audioSource.clip = ADGetBread; break;
            case "UIButtonClick":
                audioSource.clip = ADUIButtonClick; break;
            case "OpenBox":
                audioSource.clip = ADOpenBox; break;
            case "GetTreasure":
                audioSource.clip = ADGetTreasure; break;
        }
        audioSource.Play();
        Destroy(go, audioSource.clip.length);
    }
}
