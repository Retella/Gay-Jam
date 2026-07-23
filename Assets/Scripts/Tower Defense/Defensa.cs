using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Defensa : MonoBehaviour
{
    public GameObject balaPrefab;
    public Vector2 posicionTracking = Vector2.zero;

    public float radioDeBusqueda = 5f;
    public float velocidadRotacion = 10f;
    public LayerMask capaEnemigos;

    void Update()
    {
        //ActualizarRaton();
        EnemigoMasCercano();
        ApuntarAlTracker();

        if(Mouse.current.leftButton.wasPressedThisFrame) DisparaBala();
    }

    private void ActualizarRaton()
    {
        Vector3 raton = Mouse.current.position.ReadValue();
        raton.z = 10f;

        Vector3 enMundo = Camera.main.ScreenToWorldPoint(raton);
        posicionTracking = new Vector2(enMundo.x, enMundo.y) - (Vector2)transform.position;
    }

    private void EnemigoMasCercano()
{
    Transform mejor = null;
    float mejorDistSq = radioDeBusqueda * radioDeBusqueda;
    Vector2 pos = transform.position;

    for (int i = 0; i < Enemy.All.Count; i++)
    {
        var t = Enemy.All[i];
        if (t == null) continue;
        float distSq = ((Vector2)t.position - pos).sqrMagnitude;
        if (distSq <= mejorDistSq)
        {
            mejorDistSq = distSq;
            mejor = t;
        }
    }

    posicionTracking = mejor != null ? (Vector2)mejor.position - (Vector2)transform.position : Vector2.zero;
}

    private void ApuntarAlTracker()
    {
        float angulo = Mathf.Atan2(posicionTracking.y, posicionTracking.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angulo);
    }

    private void DisparaBala()
    {
        GameObject bala = Instantiate(balaPrefab, transform.position, transform.rotation);
        bala.GetComponent<Bala>().direccion = posicionTracking.normalized;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioDeBusqueda);
    }
}
