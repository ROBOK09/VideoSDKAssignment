using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float walkingSpeed = 7.5f;

    [SerializeField]
    private float runningSpeed = 11.5f;

    [SerializeField]
    private Camera playerCamera;
    
    [SerializeField]
    private float lookSpeed = 2.0f;
    
    [SerializeField]
    private float lookXLimit = 45.0f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool canMove;

    private void Start()
    {
        // Subscribing to various events fired on Game State Changes (Pub - Sub Pattern)

        UIManager.Instance.GameStarted += EnableController;
        UIManager.Instance.GameResumed += EnableController;
        UIManager.Instance.GameRestarted += EnableController;
        UIManager.Instance.GamePaused += DisableController;
        UIManager.Instance.GameOver += DisableController;
    }

    private void Update()
    {
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        moveDirection = (transform.TransformDirection(Vector3.forward) * curSpeedX) + (transform.TransformDirection(Vector3.right) * curSpeedY);

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private void OnDisable()
    {
        // Desubscribing to various events fired on Game State Changes to prevent memory leaks (Pub - Sub Pattern)

        UIManager.Instance.GameStarted -= EnableController;
        UIManager.Instance.GameResumed -= EnableController;
        UIManager.Instance.GameRestarted -= EnableController;
        UIManager.Instance.GamePaused -= DisableController;
        UIManager.Instance.GameOver -= DisableController;
    }

    private void EnableController()
    {
        canMove = true;
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisableController()
    {
        canMove = false;
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
