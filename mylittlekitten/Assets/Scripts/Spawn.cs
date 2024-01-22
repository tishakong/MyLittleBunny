using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Spawn : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject player;

    void Start()
    {
        player = Instantiate(charPrefabs[(int)DataManager.Instance.currentCharacter]);
        player.transform.position = new Vector3 (transform.position.x, transform.position.y,0);

        if (SceneManager.GetActiveScene().name == "MainMap")
        {
            GameObject mainCamera = Camera.main.gameObject;
            mainCamera.transform.parent = player.transform;
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}
