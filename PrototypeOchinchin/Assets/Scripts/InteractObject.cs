using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public UIObject InteractObjUI;
    private Door door;
    [SerializeField] private InteractType type;
    public InteractType Type 
    { 
        get
        { return type; }
    }
    public bool Completed { get; private set; } = false;
    private void Start()
    {
        if (type == InteractType.Animation && InteractObjUI == null)
        {
            enabled = false;
        }
        else if (type == InteractType.Door)
        {
            door = GetComponent<Door>();
        }
    }

    public delegate void Action();
    public event Action IsInteract;

    public void Interact()
    {
        
        switch (type)
        {
            case InteractType.Door:
                door.OpenOrClose();
                break;
            case InteractType.Pickup:
                gameObject.SetActive(false);
                InteractObjUI.Show();
                break;
            case InteractType.Animation:
                gameObject.SetActive(false);
                InteractObjUI.Show();
                break;
        }
        IsInteract?.Invoke();

    }
    public void QuitInteract()
    {
        switch (type)
        {
            case InteractType.Door:
                break;
            case InteractType.Pickup:
                InteractObjUI.Hide();
                break;
            case InteractType.Animation:
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
