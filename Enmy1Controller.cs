using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enmy1Controller : MonoBehaviour

    
{
   
    public float velocity = 2f;
        private Rigidbody2D rb2d;       // instancio un tipo de dato rigid body para darle logica

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();   //recupero el elemento rigydbody  en el metodo start.
        rb2d.velocity = Vector2.left * velocity;        //el vector2 (dos dimensiones) left le indica que se mueva hacia el -1 del x por la velocidad
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
                    //MUY IMPORTANTE HACER DESAPARECER AL ENEMIGO SI NO LLENO LA RAM
    void OnTriggerEnter2D(Collider2D other)     //metodo sobrescrito para que cuando el ememigo lo encuentre desaparezca...collider para diferenciarlo de otros elementos
       
    {
        if (other.gameObject.tag  == "Destroyer") { //si el collider 2d es un game object que choca con el tag destroyer (le cambie el nombre tag al enemy destroyter)
        Destroy(gameObject);                                             //se destruye el objeto enemy
    }                                               
    }
}
