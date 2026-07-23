using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public static readonly List<Transform> All = new List<Transform>();

    void OnEnable()  => All.Add(transform);
    void OnDisable() => All.Remove(transform);



    private int vida = 10;


    public void RecibeDanio(int cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}