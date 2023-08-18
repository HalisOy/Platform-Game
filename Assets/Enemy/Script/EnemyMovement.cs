using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(MoveSpeed, 0f) * Time.deltaTime);
    }
    private void Flip()
    {
        if (MoveSpeed > 0)
            transform.localScale = new Vector2(1, 1);
        if (MoveSpeed < 0)
            transform.localScale = new Vector2(-1, 1);
    }

    public void SetSpeed(int speed)
    {
        MoveSpeed = speed;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void FlipSpeed()
    {
        MoveSpeed = -MoveSpeed;
        Flip();
    }
}
