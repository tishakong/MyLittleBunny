using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using TMPro;

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
        // GameObject.Find로 GameObject 찾기
        GameObject fishingProgressObject = GameObject.Find("FishingProgress");

        // GameObject가 null이 아니고 TextMeshProUGUI 컴포넌트를 가지고 있다면 할당
        if (fishingProgressObject != null)
        {
            progressText = fishingProgressObject.GetComponent<TextMeshProUGUI>();

            // TextMeshProUGUI 컴포넌트가 null이면 로그 출력
            if (progressText == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on FishingProgress GameObject.");
            }
        }
        else
        {
            // GameObject가 null이면 로그 출력
            Debug.LogError("FishingProgress GameObject not found in the scene.");
        }
        fishCount = 0;

        progressText.gameObject.SetActive(false);
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
                hitObject = Interactivehit.collider.gameObject;
                print(hitObject.name);
                fishingable = true;
            }
            else
            {
                hitObject = null;
                progressText.gameObject.SetActive(false);
                fishingstart = false;
            }
        }

        if (fishingable && Input.GetKeyDown(KeyCode.Z))
        {
            clickCount = 0;
            fishingstart = true;
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
                print("물고기를 획득했습니다");
                fishCount++;
                progressText.text = "0%";
                fishingstart=false;
                progressText.gameObject.SetActive(false);
            }
        }
    }
}

