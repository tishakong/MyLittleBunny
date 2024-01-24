using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class OpenTreasure : MonoBehaviour
{
    private Vector3 vector;
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;
    public bool openable;
    public GameObject hitObject;
    public GameObject openingMotion;
    // AudioManager audioManager;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        // audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            openable = false;
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
                openable = true;
            }
            else
            {
                hitObject = null;
            }
        }

        if (openable && Input.GetKeyDown(KeyCode.Z))
        {
            // audioManager.PlaySound("Opening");
            Destroy(hitObject);
            StartCoroutine(OpeningMotion());
            openable = false;
        }
    }

    IEnumerator OpeningMotion()
    {
        Vector3 openingVector = new Vector3(hitObject.transform.position.x, hitObject.transform.position.y, hitObject.transform.position.z);

        GameObject newObject = Instantiate(openingMotion, openingVector, hitObject.transform.rotation);

        if (newObject.GetComponent<SpriteRenderer>() != null)
        {
            newObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        yield return new WaitForSeconds(2.0f); 
    }
}
