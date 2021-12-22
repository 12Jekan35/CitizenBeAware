using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpened = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool OpenOrClose()
    {
        if (!isOpened)
        {
            animator.SetBool("isOpen", isOpened = true);
        }
        else
        {
            animator.SetBool("isOpen", isOpened = false);
        }

        return isOpened;
    }

}
