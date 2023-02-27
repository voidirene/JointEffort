using System.Collections;
using System.Collections.Generic;
using Shinjingi;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private Controller player;
    private Jump jumpScript;
    private Ground ground;
    // private int numberOfJumps = 0;
    // private bool jumped = false;
    
    private AudioSource source;
    [SerializeField] private AudioClip jumpAudioClip;
    
    void Start()
    {
        player = GetComponent<Controller>();
        source = GetComponent<AudioSource>();
        jumpScript = GetComponent<Jump>();
        ground = GetComponent<Ground>();
    }

    public void PlayJumpSound()
    {
        source.clip = jumpAudioClip;
        source.Play();
    }
}
