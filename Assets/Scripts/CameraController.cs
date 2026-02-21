using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    private PlayerInputActions inputActions;

    private Vector2 moveInput;
    private float   zoomInput;
    private Vector2 panInput;

    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float zoomSpeed = 5.0f;
    [SerializeField] private float minZoom   = 5.0f;
    [SerializeField] private float maxZoom   = 20.0f;

    private Camera cam;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        cam = Camera.main;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = inputActions.Camera.Move.ReadValue<Vector2>();
        zoomInput = inputActions.Camera.Zoom.ReadValue<float>();
        panInput = inputActions.Camera.Pan.ReadValue<Vector2>();       

        HandleMovement();
        HandleZoom();
        HandlePan();

    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        transform.position += move * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float newSize = cam.orthographicSize - zoomInput * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
    }

    private void HandlePan()
    {
        if(Mouse.current.middleButton.isPressed)
        {
            Vector3 pan = new Vector3(-panInput.x, 0, -panInput.y);
            transform.position += pan * Time.deltaTime;
        }
    }
}
