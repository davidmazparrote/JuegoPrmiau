using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryController : MonoBehaviour
{
    public GameObject EnemyPrefabs;     //guardo enemyprefabs en variabe game object, hay que mober al enemy dentro de una carpeta prefabs
                                        //    mover desde prefabs al enemigo dentro del enemy factory para enlazarlo a este script
    public GameObject EnemyPrefabs2;
    public GameObject EnemyPrefabs3;
    public float TimerGenerator=2.75f;    //cada cuanto aparecen
    public float TimerGenerator2 = 15.75f;
    public float TimerGenerator3 = 3.6f;

    void Start()
    {
       
    }

  
    void Update()
    {
       
    }


    void createEnemy()
    {
        Instantiate(EnemyPrefabs,transform.position,Quaternion.identity);    //este metodo ya existe y recibe: de que tiene que crear la instancia, la poscicion del enemy generator,y este nombre por defecto que necesita el instanciate..
            
    }

    void createEnemy2()
    {
        Instantiate(EnemyPrefabs2, transform.position, Quaternion.identity);
    }


    void createObstacle1()
    {
        Instantiate(EnemyPrefabs3, transform.position, Quaternion.identity);    //este metodo ya existe y recibe: de que tiene que crear la instancia, la poscicion del enemy generator,y este nombre por defecto que necesita el instanciate..

    }

    public void StartFactory()
    {
        InvokeRepeating("createEnemy",1f,TimerGenerator);            //este metodo sobresctiro es para invocar y repetir algo varias veces y recibe un string con el nombre del metodo a repetir,
                                                         //un float con el tiempo de retardo de la primera vez que aparece y otro float con el tiempo de cada cuanto queremes que pase

    }
                                                    //estos 2 metodos los cree para controlar cuando empieza y para la generacion de enemigos
    public void StopFactory()
    {
         
       CancelInvoke("createEnemy");     //para desactivar la repeticion en animacion desmarcamos looptime
      
    }

    



    public void StartFactory2()
    {
        InvokeRepeating("createEnemy2", 7f, TimerGenerator2);
        InvokeRepeating("createObstacle1", 5f, TimerGenerator3);

    }

        public void StopFactory2()
    {

        CancelInvoke("createEnemy2");
        CancelInvoke("createObstacle1");
        

    }
}



