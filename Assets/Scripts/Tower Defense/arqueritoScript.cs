using UnityEngine;

public class arqueritoScript : MonoBehaviour
{
    private bool isFollowing = false;
    private Transform targetToFollow;

    public void follow(Transform target)
    {
        isFollowing = true;
        targetToFollow = target;
    }

    public void Update()
    {
        if (isFollowing && targetToFollow != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetToFollow.position, 5f * Time.deltaTime);
        }
    }
}
