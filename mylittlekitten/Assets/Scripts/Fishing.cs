using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

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
    public int fishCount;
    public TextMeshProUGUI progressText;
    public int clickCount;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        
        GameObject fishingProgressObject = GameObject.Find("FishingProgress");

        if (fishingProgressObject != null)
        {
            progressText = fishingProgressObject.GetComponent<TextMeshProUGUI>();
            progressText.gameObject.SetActive(false);

            if (progressText == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on FishingProgress GameObject.");
            }
        }
        else
        {
            Debug.LogError("FishingProgress GameObject not found in the scene.");
        }
        fishCount = 0;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMap")
        {
            Destroy(this);
        }
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
                progressText.gameObject.SetActive(false);
                fishingstart = false;
            }
        }

        if (fishingable && Input.GetKeyDown(KeyCode.Z))
        {
            clickCount = 0;
            if(fishCount>10){
                Debug.LogError("Limit!");
                progressText.gameObject.SetActive(true);
                progressText.text = "No More Fish!";
            }
            else
            {
                fishingstart = true;
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
                print("�����⸦ ȹ���߽��ϴ�");
                fishCount++;
                progressText.text = "0%";
                fishingstart=false;
                progressText.gameObject.SetActive(false);
                

                Vector3 fishingVector = new Vector3(11.1f, 1.3f+0.2f*(fishCount-1), transform.position.z);

                GameObject newObject = Instantiate(fishingMotion, fishingVector, transform.rotation);
            }
        }
        
    }
}
