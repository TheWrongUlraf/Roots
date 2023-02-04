using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootCollisionHandler : MonoBehaviour
{
    public PlayerRoot PlayerRootPrefab;
    public int layerPremission = 1;

    PlayerRoot PlayerRoot;
    PlayerHead playerHead;

    private void NewRoot()
    {
        if (PlayerRoot != null)
        {
            PlayerRoot.isRunning = false;
        }
        PlayerRoot = Instantiate(PlayerRootPrefab, new Vector3(UnityEngine.Random.Range(-70, 70), 0, 0), Quaternion.identity);
        playerHead = GetComponent<PlayerHead>();
        playerHead.PlayerRoot = PlayerRoot;
    }

    private void Start()
    {
        NewRoot();
    }

    private void HitDeadEnd()
    {
        NewRoot();
    }

    private void GotWater()
    {
        NewRoot();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Root")
        {
            Debug.Log("Collided with root");
            HitDeadEnd();
        }
        else if (other.gameObject.tag == "Layer1Ground")
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
        else
        {
            Debug.Log(other.name);
        }
    }

}
