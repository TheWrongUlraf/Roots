using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Root")
        {
            Debug.Log("Collided with root");
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Layer1Ground")
        {
            Debug.Log("Entered ground layer 1");
        }
        else if (other.gameObject.tag == "Layer2Ground")
        {
            Debug.Log("Entered ground layer 2");
        }
        else if (other.gameObject.tag == "Layer3Ground")
        {
            Debug.Log("Entered ground layer 3");
        }
        else if (other.gameObject.tag == "Water")
        {
            Debug.Log("Yummy yummy");
        }
    }
}
