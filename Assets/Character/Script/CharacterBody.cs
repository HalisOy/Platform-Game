using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBody : MonoBehaviour
{
    CharacterScript Player;

    private void Awake()
    {
        Player = transform.parent.GetComponent<CharacterScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            collision.transform.GetChild(0).GetComponent<Animator>().SetTrigger("CheckTrigger");
            collision.transform.parent.GetComponent<CheckpointSystem>().SetLastCheckpoint(collision.transform);
            Destroy(collision.transform.GetComponent<BoxCollider2D>());
            Destroy(collision.transform.GetChild(0).GetComponent<Animator>(), 2);
        }

        if (collision.CompareTag("FinishMap") && Player.PlayerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Finish")))
        {
            Player.InputStartStop();
            Player.Gamemanager.Finish();
        }
        if (collision.CompareTag("Coin"))
        {
            Player.Gamemanager.CoinCollect();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.gameObject.CompareTag("MapOff"))
        {
            StartCoroutine(Player.Damage());
        }
        if (collision.collider.CompareTag("Trap"))
        {
            StartCoroutine(Player.Damage());
        }
        if (collision.collider.CompareTag("EnemyKill"))
        {
            collision.transform.GetComponentInParent<EnemyMovement>().Kill();
            Player.PlayerRigidbody.velocity = new Vector2(Player.PlayerRigidbody.velocity.x, 0f);
            Player.PlayerRigidbody.velocity += new Vector2(0f, (Player.JumpSpeed * 0.75f));
        }
    }
}
