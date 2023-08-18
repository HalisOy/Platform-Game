using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Animator BounceAnimator;
    private bool ReadyBounce;
    private void Start()
    {
        BounceAnimator = GetComponent<Animator>();
        ReadyBounce = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && ReadyBounce)
        {
            BounceAnimator.SetTrigger("Bounce");
            ReadyBounce = false;
            StartCoroutine(BounceJump(collision.GetComponentInParent<CharacterScript>()));
        }
    }
    IEnumerator BounceJump(CharacterScript Character)
    {
        Character.BounceJump();
        yield return new WaitForSeconds(0.3f);
        ReadyBounce = true;
    }
}
