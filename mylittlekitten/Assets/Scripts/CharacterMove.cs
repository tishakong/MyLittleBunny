using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    //�ȱ� ���� ����
    public float speed;
    public Vector3 Movevector;
    public float runSpeed;
    private float applyRunSpeed;


    //�ȴ� �ִϸ��̼� ����
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //������Ʈ ���� ����
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;


    public bool fishing;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //sorting layer ��ȯ�� raycast
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




        // ĳ���� �����¿� �̵�, ������Ʈ ���� �ʴ� �뵵 raycast
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput != 0 || verticalInput != 0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
            }
            else
                applyRunSpeed = 0;

            Movevector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (Movevector.x !=0)
                Movevector.y = 0;

            if (Movevector.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }


            animator.SetFloat("DirX", Movevector.x);
            animator.SetFloat("DirY", Movevector.y);

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
                animator.SetBool("Walking", false);
            }
            else
            {
                animator.SetBool("Walking", true);


                if (Movevector.x != 0)
                {
                    transform.Translate(Movevector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (Movevector.y != 0)
                {
                    transform.Translate(0, Movevector.y * (speed + applyRunSpeed), 0);
                }
            }
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fishing"))
        {
            fishing = true;
            Debug.Log(fishing);
        }
    }
}
 