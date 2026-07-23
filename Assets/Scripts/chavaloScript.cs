using UnityEngine;
using UnityEngine.InputSystem;

public class chavaloScript : MonoBehaviour
{
[SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private Vector2 velocitySmoothing;

    private InputSystem_Actions inputActions; // clase generada

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = moveInput * moveSpeed;
        currentVelocity = Vector2.SmoothDamp(currentVelocity, targetVelocity, ref velocitySmoothing, smoothTime);
        rb.linearVelocity = currentVelocity; // en Unity 6 es "linearVelocity"; en versiones anteriores usa "velocity"

    }
}
