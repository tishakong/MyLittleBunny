using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class  Fishing : MonoBehaviour
{
    private Vector3 vector;
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;
    public bool fishingable;
    public bool fishingstart;
    public GameObject hitObject;
    public GameObject fishingMotion;
    public Text progressText;
    public int clickCount;
    AudioManager audioManager;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMap")
        {
            Destroy(this);
        }

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();

        GameObject fishingProgressObject = GameObject.Find("FishingProgress");

        if (fishingProgressObject != null)
        {
            progressText = fishingProgressObject.GetComponent<Text>();
            progressText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            fishingable = false;
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            RaycastHit2D Interactivehit;

            Vector2 inputDirection = new Vector2(horizontalInput, verticalInput).normalized;
            Vector2 start = capsuleCollider.bounds.center;
            Vector2 end = start + inputDirection * raycastLength;

            capsuleCollider.enabled = false;
            Interactivehit = Physics2D.Linecast(start, end, layerMask);
            Debug.DrawRay(start, inputDirection * raycastLength, Color.green);
            capsuleCollider.enabled = true;

            if (Interactivehit.transform != null)
            {
                fishingable = true;
            }
            else
            {
                progressText.text = "Start Fishing!";
                progressText.gameObject.SetActive(false);
                fishingstart = false;
            }
        }

        if (fishingable && Input.GetKeyDown(KeyCode.Z))
        {
            clickCount = 0;
            if(DataManager.Instance.myFish > 10){
                Debug.LogError("Limit!");
                progressText.gameObject.SetActive(true);
                progressText.text = "No More Fish!";
            }
            else
            {
                fishingstart = true;
                audioManager.PlaySound("FishingStart");
                audioManager.PlaySound("Fishing");
            }
            
        }

        if (fishingstart)
        {
            progressText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                clickCount++;

                float progressPercentage = clickCount / 20.0f * 100.0f;

                progressText.text = progressPercentage.ToString() + "%";
            }

            if (clickCount==20)
            {
                audioManager.PlaySound("FishingStart");
                DataManager.Instance.myFish++;
                progressText.text = "0%";
                fishingstart=false;
                progressText.gameObject.SetActive(false);
                
                Vector3 fishingVector = new Vector3(11.1f, 1.3f+0.2f*(DataManager.Instance.myFish - 1), transform.position.z);

                GameObject newObject = Instantiate(fishingMotion, fishingVector, transform.rotation);

                print(DataManager.Instance.myFish);
            }
        }
        else
        {
            if (GameObject.Find("ADFishing"))
            {
                Destroy(GameObject.Find("ADFishing"));
            }
        }
        
    }
}

