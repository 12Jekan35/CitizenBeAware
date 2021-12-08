using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public UIObject interactObjUI;
    private void Start()
    {
        if (interactObjUI == null)
        {
            enabled = false;
        }
    }



    public void Interact()
    {
        interactObjUI.Show();
        gameObject.SetActive(false);
    }
    public void QuitInteract()
    {
        interactObjUI.Hide();
        gameObject.SetActive(true);
    }
}
