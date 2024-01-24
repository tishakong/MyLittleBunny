using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class GetBread : MonoBehaviour
{
    private Vector3 vector;
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;
    public bool getable;
    public GameObject hitObject;
    public int breadcount;
    public float breadheight;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        breadcount = 0;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
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
                print(hitObject.name);
                getable = true;
            }
            else
            {
                hitObject = null;
            }
        }

        if (getable && Input.GetKeyDown(KeyCode.Z))
        {
            GetBreadMotion();
        }
    }
    void GetBreadMotion()
    {
        // hitObject가 null이 아니고 SpriteRenderer 컴포넌트가 있다면
        if (hitObject != null && hitObject.TryGetComponent(out SpriteRenderer hitObjectRenderer))
        {
            breadcount++;
            breadheight = breadheight+hitObjectRenderer.sprite.bounds.size.y-0.1f;
            // 플레이어의 위치에 BreadMotion을 생성
            Vector3 breadMotionPosition = transform.position + new Vector3(0, 0.3f+breadheight, 0);
            GameObject breadMotionObject = new GameObject("BreadMotion", typeof(SpriteRenderer));
            breadMotionObject.transform.position = breadMotionPosition;

            // BreadMotion을 플레이어의 자식으로 설정
            breadMotionObject.transform.parent = transform;

            // BreadMotion의 SpriteRenderer에 hitObject의 Sprite 적용
            SpriteRenderer breadMotionRenderer = breadMotionObject.GetComponent<SpriteRenderer>();
            breadMotionRenderer.sprite = hitObjectRenderer.sprite;
            breadMotionRenderer.sortingOrder = 13+breadcount;

            // hitObject 삭제
            Destroy(hitObject);

            // 여기에 사운드 재생 코드 추가
        }
    }

}
