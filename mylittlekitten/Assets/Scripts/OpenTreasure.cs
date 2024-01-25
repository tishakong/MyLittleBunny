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
    public bool getable;
    public GameObject hitObject;
    public GameObject OpenBoxMotion;
    AudioManager audioManager;
    public float horizontalsave;
    public float verticalsave;



    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            horizontalsave = horizontalInput;
            verticalsave = verticalInput;
            openable = false;
            getable = false;
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
                if(hitObject.name=="carrot"){
                    getable = true;
                }else{
                    openable=true;
                }
            }
            else
            {
                hitObject = null;
                getable = false;
                openable=false;
            }
        }

        if (openable && Input.GetKeyDown(KeyCode.Z))
        {
            audioManager.PlaySound("OpenBox");
            Destroy(hitObject);
            StartCoroutine(OpeningMotion());
        }

        if (getable && Input.GetKeyDown(KeyCode.G))
        {
            DataManager.Instance.myCarrot++;
            audioManager.PlaySound("GetTreasure");
            Destroy(hitObject);
        }
    }

    IEnumerator OpeningMotion()
    {
        Vector3 openingVector = new Vector3(hitObject.transform.position.x, hitObject.transform.position.y, hitObject.transform.position.z);

        GameObject newObject = Instantiate(OpenBoxMotion, openingVector, hitObject.transform.rotation);

        if (newObject.GetComponent<SpriteRenderer>() != null)
        {
            newObject.GetComponent<SpriteRenderer>().sortingOrder = -3;
        }
        openable=false;
        raycasting();
        yield break;
    }
    void raycasting(){
            RaycastHit2D Interactivehit;

            Vector2 inputDirection = new Vector2(horizontalsave, verticalsave).normalized;
            Vector2 start = capsuleCollider.bounds.center;
            Vector2 end = start + inputDirection * raycastLength;

            capsuleCollider.enabled = false;
            Interactivehit = Physics2D.Linecast(start, end, layerMask);
            Debug.DrawRay(start, inputDirection * raycastLength, Color.green);
            capsuleCollider.enabled = true;

            
            if (Interactivehit.transform != null)
            {
                hitObject = Interactivehit.collider.gameObject;
                if(hitObject.name=="carrot"){
                    getable = true;
                }
            }
            else
            {
                hitObject = null;
                getable = false;
            }
    }
}
