using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class chavaloScript : MonoBehaviour
{
[SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private CinemachineFollow cinemachineFollow;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private Vector2 velocitySmoothing;

    private InputSystem_Actions inputActions; // clase generada

    private bool isCarrerita = false;
    private Vector2 carreritaDirection;

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
        if (isCarrerita)
        {
            rb.linearVelocity = carreritaDirection * moveSpeed * 2.5f;
        }
        else
        {
            Vector2 targetVelocity = moveInput * moveSpeed;
            currentVelocity = Vector2.SmoothDamp(currentVelocity, targetVelocity, ref velocitySmoothing, smoothTime);
            rb.linearVelocity = currentVelocity; // en Unity 6 es "linearVelocity"; en versiones anteriores usa "velocity"
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("carreritaTag"))
        {
            if (isCarrerita)
            {
                desactivarCarrerita();
            }
            else
            {
                activarCarrerita(collision.gameObject.transform);
            }
        }
    }

    private void activarCarrerita(Transform carreritaTransform)
    {
        isCarrerita = true;
        carreritaDirection = carreritaTransform.up;
        cinemachineFollow.FollowOffset.x = carreritaTransform.up.x * 15f; // Ajusta la cámara para seguir la carrerita
        cinemachineFollow.FollowOffset.y = carreritaTransform.up.y * 15f; // Ajusta la cámara para seguir la carrerita
    }

    private void desactivarCarrerita()
    {
        isCarrerita = false;
        carreritaDirection = Vector2.zero;
        cinemachineFollow.FollowOffset = new Vector3(0f, 0f, -10f); // Ajusta la cámara para dejar de seguir la carrerita
    }
}
