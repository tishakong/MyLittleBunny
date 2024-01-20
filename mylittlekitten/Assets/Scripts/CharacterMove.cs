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


    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
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
                    spriteRenderer.flipX= false;
                }
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * speed, 0);
            }
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void FixedUpdate()
    {
        
    }


}
 