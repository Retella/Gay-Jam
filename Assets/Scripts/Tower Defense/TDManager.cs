using UnityEngine;
using System.Collections.Generic;

public class TWManager : MonoBehaviour
{
    public List<Transform> ciudades;
    public List<float> radios;
    public GameObject enemigo;

    public float maxX, maxY;

    public float cooldownRespawn = 1.5f;
    private float ultimoRespawn = 0f;

    private void Update()
    {
        if(Time.time-ultimoRespawn>=cooldownRespawn)
        {
            ultimoRespawn = Time.time;
            generaEnemigoAleatorio();
        }
    }

    private bool esquivaCiudad(Vector2 random)
    {
        bool libre = true;

        for(int i=0; i<ciudades.Count && libre; i++)
        {
            if(Vector2.Distance(random,ciudades[i].position) <= radios[i])
            {
                libre = false;
            }
        }

        return libre;
    }

    private void generaEnemigoAleatorio()
    {
        float x = Random.Range(-maxX,maxX);
        float y = Random.Range(-maxY,maxY);

        Vector2 posicion = new Vector2(x,y);

        if (esquivaCiudad(posicion))
        {
            Vector3 genera = new Vector3(posicion.x, posicion.y, 0f);
            Instantiate(enemigo, genera, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        for(int i=0; i<ciudades.Count; i++)
        {
            Gizmos.DrawWireSphere(ciudades[i].position, radios[i]);
        }
    }
}
