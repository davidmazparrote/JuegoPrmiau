using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Idle, run, die,win }; 

public class gameController : MonoBehaviour
{
    
    public static bool pointEnable =true;                   



    [Range(0f, 0.23f)]     
    public float ParallaxSpeed = 0.23f;
    public RawImage Backgraund;
    public RawImage Plataform;
    public GameObject UiIdle;
    public GameObject Score;
    public GameObject MenuWin;
    public GameObject DeadMenu;
    public Text TextPoints;             
    public GameObject player;      
    public GameObject EnemyFactory;    
    public GameState gs= GameState.Idle;
    private AudioSource MusicPrincipal;             
    public float TimeScale= 10f;    
    public float IncScale= 0.15f;                                          
    private int Points= 0;
    

    void Start()
    {
            MusicPrincipal= GetComponent<AudioSource>();       

    }

    

    void Update()


    {
        

        //empieza el juego
          bool UserAction= Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0);
        if (gs== GameState.Idle && UserAction)   
                                                                                                   
        {                                                                                                  
          gs = GameState.run;                                             
            
            pointEnable = true;

            UiIdle.SetActive(false);                              
            player.SendMessage("UpdateState", "PlayerRun");                   
            EnemyFactory.SendMessage("StartFactory");           
            EnemyFactory.SendMessage("StartFactory2");
            MusicPrincipal.Play();  
            player.SendMessage("TrailRun");
            Score.SetActive(true);
            DeadMenu.SetActive(false);
            InvokeRepeating("GameTimeScale", TimeScale,TimeScale); 
        }

        else if (gs== GameState.run) {                               
            Parallax();                                            
        }   
                if(gs==GameState.Idle ){
                    Score.SetActive(false);    
                    DeadMenu.SetActive(false); 
                    MenuWin.SetActive(false);   
                    player.SendMessage("TrailStop");
                }


        if (gs==GameState.die ||gs== GameState.win) {   
                                                        
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
            Time.timeScale += IncScale;                       
            Debug.Log("Ritmo:"+ Time.timeScale.ToString());

        }
            void ResetTimeScale(float SlowScale= 1f){           

                CancelInvoke("GameTimeScale");
                Time.timeScale= SlowScale;             
                Debug.Log("Ritmo normal:"+ Time.timeScale.ToString());
            }
    
                


        void IncrementPoints(){
                Points++;
                TextPoints.text= Points.ToString();                 
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
