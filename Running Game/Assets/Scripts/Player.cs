using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Player1;
    private Animator AnimatorPlayer;
    public bool isJumping = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if (isJumping)
            {
                isJumping = false;
                AnimatorPlayer = Player1.GetComponent<Animator>();
                AnimatorPlayer.SetBool("isJumping", false);
            }

        }
        if (collision.gameObject.tag == "Death")
        {
            isJumping = true;
            GM.GameManager.isDeath = true;
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.transform.parent.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
