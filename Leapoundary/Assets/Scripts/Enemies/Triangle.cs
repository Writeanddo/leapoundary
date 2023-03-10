using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(PlayerSettings.instance.ballState == BallState.Launched)
            ChaseBall();
        else
            StopTriangle();
    }

    private void StopTriangle()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    private void ChaseBall()
    {
        Vector2 direction = (Vector2)PlayerSettings.instance.ball.transform.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * PlayerSettings.instance.triangleRotate;
        rb.velocity = transform.up * PlayerSettings.instance.triangleSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            PlayerSettings.instance.ResetBall();
            PlayerSettings.instance.HurtBall();
        }
    }
}
