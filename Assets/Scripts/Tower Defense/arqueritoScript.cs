using UnityEngine;

public class arqueritoScript : MonoBehaviour
{
    private bool isFollowing = false;
    private Transform targetToFollow;
    private float followSpeed;


    public void Start()
    {
        // Inicializa la velocidad de seguimiento aleatoria
        followSpeed = 2f * Random.Range(0.5f, 3f);
    }
    public void follow(Transform target)
    {
        isFollowing = true;
        targetToFollow = target;
    }

    public void Update()
    {
        if (isFollowing && targetToFollow != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetToFollow.position, (targetToFollow.position - transform.position).magnitude * followSpeed * Time.deltaTime);
        }
    }
}
