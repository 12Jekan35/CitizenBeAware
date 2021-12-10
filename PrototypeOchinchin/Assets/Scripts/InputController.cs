using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private MainActions actions;
    private Vector2 moveVector;
    private Vector2 mouseVector;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity = 10.0f;
    [SerializeField] private float moveSpeed = 12.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3.81f;
    [SerializeField] private Scenes scenesController;
    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private float interactDistance = 25f;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool inInteract = false;
    [SerializeField] private Pause pauseController;

    private float RotationX = 0f;

    private InteractObject interactObject = null;

    private CharacterController character;

    

    void OnEnable()
    {
        actions.Enable();
    }

    private void OpenMenu_performed(InputAction.CallbackContext obj)
    {
        if(pauseController != null && !inInteract)
        {
            if (pauseController.GameIsPaused)
            {
                pauseController.Resume();
                actions.PlayerControls.QuitInMenu.performed -= QuitInMenu;
            }
            else
            {
                pauseController.Paused();
                actions.PlayerControls.QuitInMenu.performed += QuitInMenu;
            }
        }
    }

    void OnDisable()
    {
        actions.Disable();
    }

    void Awake()
    {
        actions = new MainActions();

        actions.PlayerControls.Move.performed += SetMoveDirection;
        actions.PlayerControls.Move.canceled += SetMoveDirection;

        actions.PlayerControls.Look.performed += SetLookDirection;
        actions.PlayerControls.Look.canceled += SetLookDirection;

        actions.PlayerControls.Jump.performed += MakeJump;
        actions.PlayerControls.Jump.canceled += MakeJump;

        actions.PlayerControls.Interact.performed += Interact;
        actions.PlayerControls.OpenMenu.performed += OpenMenu_performed;

        
    }

    private void QuitInMenu(InputAction.CallbackContext obj)
    {
        scenesController.Scene(0);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!inInteract || !pauseController.GameIsPaused)
        {
            MakeMove();
        }
        
        if (character.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else 
        {
            velocity.y += gravity * Time.deltaTime;
        }
        character.Move(velocity * Time.deltaTime);
        
    }

    private void MakeMove()
    {
        if (moveVector != Vector2.zero)
        {
            Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;
            character.Move(move * moveSpeed * Time.deltaTime);
        }
        if (mouseVector != Vector2.zero)
        {
            RotationX -= mouseVector.y * mouseSensitivity * Time.deltaTime;
            RotationX = Mathf.Clamp(RotationX, -90f, 90f);
            playerCamera.transform.localRotation = Quaternion.Euler(RotationX, 0f, 0f);
            gameObject.transform.Rotate(Vector3.up * mouseVector.x * mouseSensitivity * Time.deltaTime);
        }
    }

    private void SetMoveDirection(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
    private void SetLookDirection(InputAction.CallbackContext context)
    {
        mouseVector = context.ReadValue<Vector2>();
    }
    private void MakeJump(InputAction.CallbackContext context)
    {
        if (character.isGrounded)
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (pauseController.GameIsPaused)
            return;

        RaycastHit hit;
        
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, interactDistance));

        if (Physics.Raycast(ray, out hit))
        {
            var intObj = hit.transform.gameObject;
            Debug.Log(intObj.name);
            if (intObj.tag == "InteractObject")
            {
                inInteract = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                Debug.Log($"Switching to the Interact mode");
                interactObject = intObj.GetComponent<InteractObject>();
                interactObject.Interact();
                actions.PlayerControls.QuitInteract.performed += QuitInteract;
            }
        }
    }

    private void QuitInteract(InputAction.CallbackContext context)
    {
        if (inInteract)
        {
            inInteract = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log($"Switching to the Moving mode");
            interactObject.QuitInteract();
            actions.PlayerControls.QuitInteract.performed -= QuitInteract;

        }
    }
}
