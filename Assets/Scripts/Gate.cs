using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{   
    private BoxCollider2D boxCollider;

    private Animator animator;

    [SerializeField] private AudioClip openSFX;
    [SerializeField] private AudioClip closeSFX;
    private AudioSource source;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void OpenGate()
    {
        boxCollider.enabled = false;
        animator.SetBool("isOpen", true);
        PlaySound(openSFX);
    }

    public void CloseGate()
    {
        boxCollider.enabled = true;
        animator.SetBool("isOpen", false);
        PlaySound(closeSFX);
    }

    public void PlaySound(AudioClip soundClip)
    {
        source.clip = soundClip;
        source.Play();
    }
}
