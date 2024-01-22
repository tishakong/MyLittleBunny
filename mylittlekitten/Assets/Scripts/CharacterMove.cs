using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    //걷기 위한 변수
    public float speed;
    private Vector3 vector;


    //걷는 애니메이션 변수
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //오브젝트 감지용
    Rigidbody2D rigid;
    CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    Vector3 dirVec;
    GameObject scanObject;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sortingOrder = 0;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 start = capsuleCollider.transform.position;

            Vector2 end = start + new Vector2(vector.x * speed, vector.y * speed);

            capsuleCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            Debug.DrawRay(start, end, new Color(0, 1, 0));
            print("${layerMask}");
            capsuleCollider.enabled = true;

            if (hit.transform != null)
            {

            }
            else
            {
                animator.SetBool("Walking", true);


                if (vector.x != 0)
                {
                    transform.Translate(vector.x * speed, 0, 0);

                    if (vector.x < 0)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        spriteRenderer.flipX = false;
                    }
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * speed, 0);
                }
            }
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void FixedUpdate()
    {
        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
}
