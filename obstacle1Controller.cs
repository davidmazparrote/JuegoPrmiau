using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle1Controller : MonoBehaviour
{
    public float velocity = 9f;
    private Rigidbody2D rb2do1;
    void Start()
    {
        rb2do1 = GetComponent<Rigidbody2D>();
        rb2do1.velocity = Vector2.left * velocity;

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {       //fuera del Update
        {       //fuera del Update

            if (other.gameObject.tag == "Destroyer")
            {
                Destroy(gameObject);
            }
        }
    }
}
