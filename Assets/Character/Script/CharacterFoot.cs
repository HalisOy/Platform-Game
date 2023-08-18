using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFoot : MonoBehaviour
{
    CharacterScript Player;
    private void Start()
    {
        Player = GetComponentInParent<CharacterScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            Player.TriggerEnterGround();

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            Player.TriggerExitGround();
    }
}
