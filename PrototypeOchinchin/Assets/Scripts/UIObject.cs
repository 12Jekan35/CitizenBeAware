using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{

    public Renderer Renderer { get; private set; }
    public Animator Animator { get; private set; }
    
    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Hide();
    }

    public void Hide()
    {
        UIShow = false;
        Renderer.enabled = false;
        Animator.Play("Exit");
        Animator.enabled = false;
        

    }
    public void Show()
    {
        UIShow = true;
        Renderer.enabled = true;
        Animator.enabled = true;
        Shows.Invoke();
        Animator.Play("coolnewanimation");
        
    }

    public bool UIShow
    {
        get;
        set;
    }
    public delegate void ShowSubs();
    public event ShowSubs Shows;
}
