using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public Character character;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public SelectCharacter[] chars;
    public Button Select_button;


    void Start()
    {
        animator= GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        OnDeSelect();
    }

    private void OnMouseUpAsButton()
    {
        DataManager.Instance.currentCharacter = character;
        OnSelect();

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] != this)
            {
                chars[i].OnDeSelect();
            }
        }
    }

    void OnDeSelect()
    {
        animator.SetBool("Walking", false);
        spriteRenderer.transform.localScale = new Vector3(2f, 2f, 1f);
    }

    void OnSelect()
    {
        animator.SetBool("Walking", true);
        spriteRenderer.transform.localScale = new Vector3(2.5f,2.5f,1f);
        Select_button.gameObject.SetActive(true);
    }

} 
