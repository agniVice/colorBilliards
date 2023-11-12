using UnityEngine;

public class LineMovement : MonoBehaviour
{
    public Transform Ball;

    private void Update()
    {
        transform.position = Ball.position;
    }
}