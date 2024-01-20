using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRabbit : MonoBehaviour
{
    public float speed;
    private Vector3 vector;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
            {
                transform.Translate(vector.x * speed, 0, 0);
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * speed, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    void Start()
    {
        
    }

}
