using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : MonoBehaviour
{
    [Range(-10f, 0f)]
    [SerializeField] private float Left;
    [Range(0f, 10f)]
    [SerializeField] private float Right;
    [Range(0f, 20f)]
    [SerializeField] private float MoveSpeed = 1;

    private Vector2 movement;

    void Start()
    {
        movement = new Vector2(Left, transform.localPosition.y);
    }
    void Update()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, movement, MoveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.localPosition, movement) <= 0.1f)
        {
            if (movement.x != Left)
                movement.x = Left;
            else
                movement.x = Right;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 Origin = transform.parent.position;
        Origin.y -= 1;

        Gizmos.DrawWireSphere(new Vector3(Origin.x + Left, Origin.y, Origin.z), 1f);

        Gizmos.DrawWireSphere(new Vector3(Origin.x + Right, Origin.y, Origin.z), 1f);
    }

}
