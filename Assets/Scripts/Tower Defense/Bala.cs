using UnityEngine;

public class Bala : MonoBehaviour
{
    public Vector2 direccion;

    public float velocidad = 5f;
    public int danio = 1;
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other!=null)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemigo"))
            {
                Enemigo aux = other.GetComponent<Enemigo>();
                aux.RecibeDanio(danio);
                Destroy(this.gameObject);
            }
        }
    }


}
