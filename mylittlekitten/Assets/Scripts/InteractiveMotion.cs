using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class InteractiveMotion : MonoBehaviour
{
    //걷기 위한 변수
    private Vector3 vector;

    //오브젝트 감지 변수
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;
    public bool actionable;
    public GameObject hitObject;
    public GameObject wateringMotion;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            actionable = false;
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
                actionable = true;
            }
            else
            {
                hitObject = null;
            }
        }

        if (actionable && Input.GetKeyDown(KeyCode.Z))
        {
            Watering(hitObject);
            StartCoroutine(WateringMotion());
        }
    }

    void Watering(GameObject parentObject)
    {
        if (parentObject != null)
        {
            foreach (Transform child in parentObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    IEnumerator WateringMotion()
    {
        Vector3 wateringVector = new Vector3(hitObject.transform.position.x+0.5f, hitObject.transform.position.y+0.5f, hitObject.transform.position.z);

        // 새로운 오브젝트 인스턴스화
        GameObject newObject = Instantiate(wateringMotion, wateringVector, hitObject.transform.rotation);

        // 일정 시간 대기
        yield return new WaitForSeconds(2.0f); // 예시로 1초 대기

        // 새로운 오브젝트 삭제
        Destroy(newObject);
    }

}
