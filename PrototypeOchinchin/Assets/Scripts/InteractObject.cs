using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public UIObject InteractObjUI;
    [SerializeField] private InteractType type;
    public bool Completed { get; private set; } = false;
    private void Start()
    {
        if (type == InteractType.Animation && InteractObjUI == null)
        {
            enabled = false;
        }
    }

    public delegate void Action();
    public event Action IsInteract;

    public void Interact()
    {
        if 
        switch (type)
        {
            case InteractType.Door:
                break;
            case InteractType.Animation:
                break;
            case InteractType.Pickup:
                InteractObjUI.Show();
                gameObject.SetActive(false);
                break;
        }

    }
    public void QuitInteract()
    {
        switch (type)
        {
            case InteractType.Door:
                break;
            case InteractType.Animation:

                break;
            case InteractType.Pickup:
                InteractObjUI.Hide();
                gameObject.SetActive(true);
                break;
        }
        Completed = true;
    }
}
public enum InteractType
{
    Door,
    Animation,
    Pickup

}
