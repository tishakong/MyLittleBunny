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
    public GameObject hitObject;
    public GameObject fishingMotion;
    public int fishCount;
    public TextMeshProUGUI progressText;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        progressText = GetComponent<TextMeshProUGUI>();
        fishCount = 0;
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
            }
        }

        if (fishingable && Input.GetKeyDown(KeyCode.Z))
        {
            FishingMotion(hitObject);

            progressText.gameObject.SetActive(true);
        }
    }

    
    void FishingMotion(GameObject parentObject)
    {
        int clickCount = 0;

        while (clickCount < 20)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                clickCount++;

                float progressPercentage = clickCount / 20.0f * 100.0f;

                progressText.text = progressPercentage.ToString() + "%";
            }
    
        }
        fishCount++;
        progressText.text = "0%";
    }

}

