using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{

    public Renderer renderer;
    public Animator animator;
    
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
        UIShow = false;
        renderer.enabled = false;
        animator.Play("Exit");
        animator.enabled = false;
        

    }
    public void Show()
    {
        UIShow = true;
        renderer.enabled = true;
        animator.enabled = true;
        Shows.Invoke();
        animator.Play("coolnewanimation");
        
    }

    public bool UIShow
    {
        get;
        set;
    }
    public delegate void ShowSubs();
    public event ShowSubs Shows;
    

}
