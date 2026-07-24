using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class Enemigo : MonoBehaviour
{
    public static readonly List<Transform> All = new List<Transform>();

    void OnEnable()  => All.Add(transform);
    void OnDisable() => All.Remove(transform);



    private int vida = 10;

    private Vector2 baseCercana = Vector2.zero;
    
    private Rigidbody2D rb;
    public float velocidad = 5f;
    public int danio = 2;

    public float cooldownContacto = 1f;
    private float tiempoUltimoContacto = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if(baseCercana == Vector2.zero) BaseMasCercana();
        BaseMasCercana();
        rb.linearVelocity = baseCercana.normalized * velocidad;
    }

    private void BaseMasCercana()
    {
        Transform mejor = null;
        float mejorDistSq = float.MaxValue;
        Vector2 pos = transform.position;

        for (int i = 0; i < BaseDefensa.All.Count; i++)
        {
            var t = BaseDefensa.All[i];
            if (t == null) continue;
            float distSq = ((Vector2)t.position - pos).sqrMagnitude;
            if (distSq < mejorDistSq)
            {
                mejorDistSq = distSq;
                mejor = t;
            }
        }

        baseCercana = mejor != null ? (Vector2)mejor.position - (Vector2)transform.position : Vector2.zero;
    }


    public void RecibeDanio(int cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other!=null)
        {
            BaseDefensa aux = other.gameObject.GetComponent<BaseDefensa>();
            if(aux!=null)
            {
                if(Time.time - tiempoUltimoContacto >= cooldownContacto)
                {
                    if(aux.RecibeDanio(danio))
                        baseCercana = Vector2.zero;
                    tiempoUltimoContacto = Time.time;
                    Debug.Log("he atacao");
                }
            }
        }
    }
}