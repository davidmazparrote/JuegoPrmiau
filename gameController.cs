using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Idle, run, die,win }; //estados del juego parado y jugando//lo declaro afuera para poder usarlo en otras clases

public class gameController : MonoBehaviour
{
    
    public static bool pointEnable =true;                   //variable estatica para llamarlo desde metodos estaticos



    [Range(0f, 0.23f)]     //genera un rango para la parallaxspeed 
    public float ParallaxSpeed = 0.23f;
    public RawImage Backgraund;
    public RawImage Plataform;
    public GameObject UiIdle;
    public GameObject Score;
    public GameObject MenuWin;
    public GameObject DeadMenu;
    public Text TextPoints;             //enlazarlo en gamecanvas
    public GameObject player;       //lo declaro y luego lo vinculo en gamecanvas del unity
    public GameObject EnemyFactory;     //lo declaro y luego lo vinculo en gamecanvas del unity
    public GameState gs= GameState.Idle;
    private AudioSource MusicPrincipal;             //para manipular la musica
    public float TimeScale= 10f;     //cada cuanto lo escalamos el juego
    public float IncScale= 0.15f;// el porcentaje que se va incrementando                                            // El inicio se llama antes de la primera actualización del cuadro
    private int Points= 0;
    

    void Start()
    {
            MusicPrincipal= GetComponent<AudioSource>();        // que se ejecute cuando comienza el juego


    }

    // Update is called once per frame..La actualización se llama una vez por cuadro

    void Update()


    {
        

        //empieza el juego
          bool UserAction= Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0);
        if (gs== GameState.Idle && UserAction)   //para que comienze, el juego tiene que estara parado ==
                                                                                                    //el juego no empieza hasta que no le presione
        {                                                                                         // arriba o click der tambien sirve para android         
          gs = GameState.run;                                              //si se cumple el if cambia el state y arranca el juego 
            
            pointEnable = true;

            UiIdle.SetActive(false);                               //una vez que arranca se desactiva los textos
            player.SendMessage("UpdateState", "PlayerRun");                    // string con el nombre del metodo que vamos a usar +el nombre de la animacion
            EnemyFactory.SendMessage("StartFactory");           // nombre del metodo que vamos a usar + la llamda al metodo para que empieze a generar cuando empieze eljuego
            EnemyFactory.SendMessage("StartFactory2");
            MusicPrincipal.Play();  //enpieza la musica
            player.SendMessage("TrailRun");
            Score.SetActive(true);
            DeadMenu.SetActive(false);
            InvokeRepeating("GameTimeScale", TimeScale,TimeScale); //cada tantos segundos se va incrementando
        }

        else if (gs== GameState.run) {                               //si el juego pasa a stado run se ejecuta el parallax 
            Parallax();                                             //llamo al metodo paralax en la comprobacion, solo si el juego esta corriendo
    
        }   
                if(gs==GameState.Idle ){
                    Score.SetActive(false);    
                    DeadMenu.SetActive(false); 
                    MenuWin.SetActive(false);   
                    player.SendMessage("TrailStop");
                }


        if (gs==GameState.die ||gs== GameState.win) {   
                                                         //Debug.Log();para imprimir por consola
            pointEnable = false;


            EnemyFactory.SendMessage("StopFactory");          
            EnemyFactory.SendMessage("StopFactory2");
            
            
            if (UserAction){

            RestartGame();
            ResetTimeScale();

           
            }if(gs== GameState.win){
                MenuWin.SetActive(true);
            }else{
                DeadMenu.SetActive(true);
                }


        }
                         

        

    
    }
            void Parallax() {
            float FinalSpeed = ParallaxSpeed * Time.deltaTime;
            Backgraund.uvRect = new Rect(Backgraund.uvRect.x + FinalSpeed / 10, 0f, 1f, 1f);
            Plataform.uvRect = new Rect(Plataform.uvRect.x + FinalSpeed * 5, 0f, 1f, 1f);
        }


            void RestartGame(){
                SceneManager.LoadScene("SampleScene");

        }

        void GameTimeScale(){
            Time.timeScale += IncScale;                        //Time.timeScale es una propiedad interna de unity para el tiempo
            Debug.Log("Ritmo:"+ Time.timeScale.ToString());

        }
            void ResetTimeScale(float SlowScale= 1f){           //le paso este parametro para que despues al morir lo pueda reducir a la mitad

                CancelInvoke("GameTimeScale");
                Time.timeScale= SlowScale;              // velocidad por defecto de inicio
                Debug.Log("Ritmo normal:"+ Time.timeScale.ToString());
            }
    
                


        void IncrementPoints(){
                Points++;
                TextPoints.text= Points.ToString();                 // lo convierto en una cadena y luego lo guardo en texpoints
                         if(Points==20){
                     EnemyFactory.SendMessage("StopFactory");          
                     EnemyFactory.SendMessage("StopFactory2");
                         player.SendMessage("TrailStop");
                        
                         MenuWin.SetActive(true);

                                player.SendMessage("PlayerWin");
              
                                
                                ResetTimeScale();
                                
                            gs= GameState.win;

                                player.SendMessage("PlayerWin");

                         
                         

                            
              
                         

                                  GameObject[] allEnemies; 
                                allEnemies =  GameObject.FindGameObjectsWithTag ("Enemy");
                        
                                for(int i = 0 ; i < allEnemies.Length ; i ++){
                                    Destroy(allEnemies[i]);

                                
                         
                                }
                                
                         
    
                         }          
    
        }
}
