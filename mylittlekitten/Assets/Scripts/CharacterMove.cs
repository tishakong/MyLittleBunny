using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    //걷기 위한 변수
    public float speed;
    private Vector3 vector;
    public float runSpeed;
    private float applyRunSpeed;


    //걷는 애니메이션 변수
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //오브젝트 감지 변수
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RaycastHit2D sorthit;

        Vector2 startDown = capsuleCollider.bounds.center;
        Vector2 endDown = startDown + Vector2.down * raycastLength;

        capsuleCollider.enabled = false;
        sorthit = Physics2D.Linecast(startDown, endDown, layerMask);
        Debug.DrawRay(startDown, Vector2.down * raycastLength, Color.blue);
        capsuleCollider.enabled = true;

        if (sorthit.collider != null)
        {
            GameObject hitObject = sorthit.collider.gameObject;
            print("아래로 무언가 맞음: " + hitObject.name);
            SpriteRenderer hitSpriteRenderer = hitObject.GetComponent<SpriteRenderer>();
            if (hitSpriteRenderer != null)
            {
                int sortingOrder = hitSpriteRenderer.sortingOrder;
                spriteRenderer.sortingOrder = sortingOrder-1;
            }
        }
        else
        {
            spriteRenderer.sortingOrder=3;
        }





        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
            }
            else
                applyRunSpeed = 0;

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x !=0)
                vector.y = 0;

            //왼쪽 이동 시 flipx 적용
            if (vector.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }


            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 inputDirection = new Vector2(horizontalInput, verticalInput).normalized;
            Vector2 start = capsuleCollider.bounds.center;
            Vector2 end = start + inputDirection* raycastLength ;

            capsuleCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            Debug.DrawRay(start, inputDirection * raycastLength, Color.red);
            capsuleCollider.enabled = true;

            

            if (hit.transform != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                print("무언가 맞음: " + hitObject.name);
                animator.SetBool("Walking", false);
            }
            else
            {
                animator.SetBool("Walking", true);


                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
            }
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
}
 