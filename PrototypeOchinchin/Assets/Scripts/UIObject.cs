using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{

    Renderer renderer;
    Animator animator;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Hide();
    }

    private void Start()
    {
        
    }

    public void Hide()
    {
        renderer.enabled = false;
        animator.Play("Exit");
        animator.enabled = false;

    }
    public void Show()
    {
        renderer.enabled = true;
        animator.enabled = true;

        animator.Play("coolnewanimation");
    }
}
