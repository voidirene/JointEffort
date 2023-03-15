using System.Collections;
using System.Collections.Generic;
using Shinjingi;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Controller player;
    private Ground ground;
    private Animator animator;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform magnet;
    [SerializeField] private Transform magnetPoint;
    private float magnetXPos;
    private float magnetPointXPos;
    private int lastDirectionPressed = 1;

    private void Start()
    {
        player = GetComponent<Controller>();
        ground = GetComponent<Ground>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        magnetXPos = magnet.localPosition.x;
        magnetPointXPos = magnetPoint.localPosition.x;
    }

    private void Update()
    {
        // if (!player.paused && !player.won)
        // {
            // animator.speed = 1; //If the player hasn't won yet and hasn't paused the game, play animations
            // if (player.dead)
            // {
            //     animator.Play("Dead");
            // }

            //JUMP
            //TODO: this is broken (?)
            if (ground.OnGround == false)
            {
                animator.SetBool("isOnAir", true);
                return;
            } 
            else 
            {
                animator.SetBool("isOnAir", false);
            }

            //WALK
            float moveInput = player.input.RetrieveMoveInput();
            if (moveInput != 0)
            {
                animator.SetBool("isWalking", true);

                //For flipping the player sprite
                if (moveInput != lastDirectionPressed)
                {
                    Flip();
                    lastDirectionPressed = (int) moveInput;
                }
                return;
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        // }
        // else
        // {
        //     animator.speed = 0; //If the player has won, or has paused the game, pause the animator
        // }
    }

    //Flips the player to face the other way
    void Flip()
    {
        if (spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
            magnet.localPosition = new Vector2(magnetXPos, 0);
            magnetPoint.localPosition = new Vector2(magnetPointXPos, 0);
            magnet.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
            magnet.localPosition = new Vector2(-magnetXPos, 0);
            magnetPoint.localPosition = new Vector2(-magnetPointXPos, 0);
            magnet.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}