using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject game;
    private Animator animator;
    private AudioSource AudioPlayer; //  para los audios del personaje
    public AudioClip SoundJump, SoundDie, SoundSlide; //los enlazo en el unity en el playerController
    public AudioClip SoundPoint;
    public ParticleSystem Trail;

    //variable para importar y usar el gameObject en otra clase...en unity lo arrastro el gamecanvas al script game para poder usarlo
    // Start is called before the first frame update

    void Start () {

        animator = GetComponent<Animator> (); //se detecte automaticamente en el start del player el componente animator se ejecuta este metodo()
        AudioPlayer = GetComponent<AudioSource> (); //recupero el audio source
    }

    // Update is called once per frame
    void Update () {
        bool GameRun = game.GetComponent<gameController> ().gs == GameState.run; //comprobacion para ver en que estado esta el juego 
        if ((GameRun && Input.GetKeyDown (KeyCode.UpArrow) || Input.GetMouseButtonDown (0))) //si el estado es run y misma sentencia clik y up para saltar
        {
            UpdateState ("PlayerJump"); // llamo al mismo metodo, y le cambio el estado. recibe un string con el estado de la animacion que quiero, sino es null.

            AudioPlayer.clip = SoundJump; //para que no se quede saltando en el animator hago una transicion y lo enlazo con el run para que siga corriendo
            AudioPlayer.Play ();

            //las opciones de duracion de la trancision estan en la animacion- inspector- lopp
        }
        if (GameRun && Input.GetKeyDown (KeyCode.DownArrow) ) {
            UpdateState ("PlayerSlide");
            AudioPlayer.clip = SoundSlide;
            AudioPlayer.Play ();

        }

    }

    public void UpdateState (string state = null) //actualizar el estado,este metodo recibe un string state si no le paso ninguno es null por defecto
    {
        if (state != null) { //si el estado es diferente de null
            animator.Play (state); //el nombre del estado que queremos cambiar

        }
    }
    public void OnTriggerEnter2D (Collider2D other) //metodo sobrescrito para collisionar

    {

        if (other.gameObject.tag == "Enemy") //el tag con el que colisionamos es un enemy y en el unity en prefabs le cambiamos el tag al enemigo
        {
            UpdateState ("PlayerDead");
            game.GetComponent<gameController> ().gs = GameState.die; //dentro de game obtengo el componente de la clase principal,la instancia gs, el estado mas el estado de morir
            game.SendMessage ("ResetTimeScale", 0.5f); //reseteo el tiempo y el 0.5 es para que se reduzca a la mitad la velocidad al morir

            game.GetComponent<AudioSource> ().Stop (); //metodo stop para parar la musica cuando muere
            AudioPlayer.clip = SoundDie;
            AudioPlayer.Play (); //para reproducirlo una vez

        } else if (other.gameObject.tag == "Points" && !gameController.pointEnable==false ) {         //si choca con un point incremeta punto...importante cambiarle el tag a points en el unity...tiene que ser distino de false para que sume el punto
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