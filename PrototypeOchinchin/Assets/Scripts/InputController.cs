using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private MainActions actions;
    [SerializeField] private Vector2 moveVector;
    [SerializeField] private Vector2 mouseVector;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] private float moveSpeed = 12.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3.81f;

    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private float interactDistance = 25f;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool inInteract = false;

    private float RotationX = 0f;

    private CharacterController character;
    void OnEnable()
    {
        actions.Enable();
        actions.PlayerControls.Move.performed += SetMoveDirection;
        actions.PlayerControls.Move.canceled += SetMoveDirection;

        actions.PlayerControls.Look.performed += SetLookDirection;
        actions.PlayerControls.Look.canceled += SetLookDirection;

        actions.PlayerControls.Jump.performed += MakeJump;
        actions.PlayerControls.Jump.canceled += MakeJump;

        actions.PlayerControls.Interact.performed += GetMousePosition;
    }
    void OnDisable()
    {
        actions.Disable();
    }

    void Awake()
    {
        actions = new MainActions();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        character = GetComponent<CharacterController>();
        Debug.Log($"Здарова заебал");
    }

    void Update()
    {
        if (!inInteract)
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
        Debug.Log($"mouse = {mouseVector}");
    }
    private void MakeJump(InputAction.CallbackContext context)
    {
        if (character.isGrounded)
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void GetMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = actions.PlayerControls.MousePosition.ReadValue<Vector2>();
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(mousePosition.x, interactDistance, mousePosition.y));

        if (Physics.Raycast(ray, out hit))
        {
            var obj = hit.transform.gameObject;
            if (obj.tag == "InteractObject")
            {
                inInteract = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                Debug.Log($"Switching to the Interact mode");
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
            actions.PlayerControls.QuitInteract.performed -= QuitInteract;
        }
    }
}
