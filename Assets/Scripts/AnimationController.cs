using System.Collections;
using System.Collections.Generic;
using Shinjingi;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Controller player;
    private Ground ground;
    private Animator animator;

    private void Start()
    {
        player = GetComponent<Controller>();
        ground = GetComponent<Ground>();
        animator = GetComponent<Animator>();
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
            //TODO: this is broken
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
            if (player.input.RetrieveMoveInput() != 0)
            {
                animator.SetBool("isWalking", true);
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
}