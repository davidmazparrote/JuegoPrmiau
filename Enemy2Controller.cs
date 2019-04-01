using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public float velocity = 25f;
    private Rigidbody2D rb2d2;
    void Start()
    {
        rb2d2 = GetComponent<Rigidbody2D>();
        rb2d2.velocity = Vector2.left * velocity;

    }

    // Update is called once per frame
    void Update()
    {
        
                
            }

        void OnTriggerEnter2D(Collider2D other) {      
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }


    }
}
