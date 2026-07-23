using UnityEngine;

public class Bala : MonoBehaviour
{
    public Vector2 direccion;

    public float velocidad = 5f;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = direccion * velocidad;
    }
}
