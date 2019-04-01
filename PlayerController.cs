using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject game;
    private Animator animator;
    private AudioSource AudioPlayer; 
    public AudioClip SoundJump, SoundDie, SoundSlide; 
    public AudioClip SoundPoint;
    public ParticleSystem Trail;

  

    void Start () {

        animator = GetComponent<Animator> (); 
        AudioPlayer = GetComponent<AudioSource> (); 
    }

    // Update is called once per frame
    void Update () {
        bool GameRun = game.GetComponent<gameController> ().gs == GameState.run; 
        if ((GameRun && Input.GetKeyDown (KeyCode.UpArrow) || Input.GetMouseButtonDown (0))) 
        {
            UpdateState ("PlayerJump"); 

            AudioPlayer.clip = SoundJump; 
            AudioPlayer.Play ();

          
        }
        if (GameRun && Input.GetKeyDown (KeyCode.DownArrow) ) {
            UpdateState ("PlayerSlide");
            AudioPlayer.clip = SoundSlide;
            AudioPlayer.Play ();

        }

    }

    public void UpdateState (string state = null) 
    {
        if (state != null) { 
            animator.Play (state);

        }
    }
    public void OnTriggerEnter2D (Collider2D other) 

    {

        if (other.gameObject.tag == "Enemy") 
        {
            UpdateState ("PlayerDead");
            game.GetComponent<gameController> ().gs = GameState.die; 
            game.SendMessage ("ResetTimeScale", 0.5f); 

            game.GetComponent<AudioSource> ().Stop (); 
            AudioPlayer.clip = SoundDie;
            AudioPlayer.Play (); 

        } else if (other.gameObject.tag == "Points" && !gameController.pointEnable==false ) {        
            game.SendMessage ("IncrementPoints");
            AudioPlayer.clip = SoundPoint;
            AudioPlayer.Play ();

        }

    }

    void TrailRun () {

        Trail.Play ();
    }

    void TrailStop () {

        Trail.Stop ();
    }

    void PlayerWin(){

        animator.SetBool("win", true);
        TrailStop ();
        
    }
}
