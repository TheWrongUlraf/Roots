using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootCollisionHandler : MonoBehaviour
{
    public int layerPremission = 1;

    private void HitDeadEnd()
    {
        UnityEditor.EditorApplication.isPaused = true;

        //SceneManager.LoadScene(0);
    }

    private void GotWater()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Root")
        {
            Debug.Log("Collided with root");
            HitDeadEnd();
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Layer1Ground")
        {
            Debug.Log("Entered ground layer 1");
            if (layerPremission < 1)
            {
                HitDeadEnd();
            }
        }
        else if (other.gameObject.tag == "Layer2Ground")
        {
            Debug.Log("Entered ground layer 2");
            if (layerPremission < 2)
            {
                HitDeadEnd();
            }
        }
        else if (other.gameObject.tag == "Layer3Ground")
        {
            Debug.Log("Entered ground layer 3");
            if (layerPremission < 2)
            {
                HitDeadEnd();
            }
        }
        else if (other.gameObject.tag == "Water")
        {
            Debug.Log("Yummy yummy");
            GotWater();
        }
    }

}
