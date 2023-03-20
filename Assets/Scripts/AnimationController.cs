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

    private bool pauseAnimations = false;

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
        if (pauseAnimations)
            return;
            
        //JUMP
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
                lastDirectionPressed = (int)moveInput;
            }
            return;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    //Flips the player to face the other way
    void Flip()
    {
        if (spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
            magnet.localPosition = new Vector2(magnetXPos, 0);
            magnetPoint.localPosition = new Vector2(magnetPointXPos, -0.11f);
            magnet.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
            magnet.localPosition = new Vector2(-magnetXPos, 0);
            magnetPoint.localPosition = new Vector2(-magnetPointXPos, -0.11f);
            magnet.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void Explode()
    {
        pauseAnimations = true;
        animator.SetBool("isExploding", true);
    }
}