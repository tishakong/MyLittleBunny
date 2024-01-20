using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Spawn : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject player;

    void Start()
    {
        player = Instantiate(charPrefabs[(int)DataManager.Instance.currentCharacter]);
        player.transform.position = transform.position;

        GameObject mainCamera = Camera.main.gameObject;
        mainCamera.transform.parent = player.transform;
    }
}
