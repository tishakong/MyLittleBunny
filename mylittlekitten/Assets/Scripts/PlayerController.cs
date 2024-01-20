using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    RuntimeAnimatorController controller;
    public float walk_speed;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(Mathf.Abs(hor) * walk_speed * Time.deltaTime, 0, 0));

        if (hor > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            animator.SetBool("run", true);
        }
        else if (hor < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }
}
