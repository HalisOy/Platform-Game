using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundEnd : MonoBehaviour
{
    EnemyMovement enemy;
    private void Start()
    {
        enemy = GetComponentInParent<EnemyMovement>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy.FlipSpeed();
    }
}
