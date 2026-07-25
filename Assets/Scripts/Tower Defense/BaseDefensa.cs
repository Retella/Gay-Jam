using UnityEngine;
using System.Collections.Generic;

public class BaseDefensa : MonoBehaviour
{
    public static readonly List<Transform> All = new List<Transform>();

    void OnEnable()  => All.Add(transform);
    void OnDisable() => All.Remove(transform);
    public bool tieneDefensa = false;
    private GameObject defensaActual = null;
    private int vida = 10;

    public bool RecibeDanio(int cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
    
}
