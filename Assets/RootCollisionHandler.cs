using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootCollisionHandler : MonoBehaviour
{
    public PlayerRoot PlayerRootPrefab;
    public GameObject TreePrefab;
    public int layerPremission = 1;
    public TextMeshProUGUI textMeshPro;

    public Transform groundLevel;
    public Transform spawnLeft;
    public Transform spawnRight;

    public GameloopManager gm;

    public PlayerRoot PlayerRoot;
    PlayerHead playerHead;

    float minDistance = 10f;

    List<float> rootXs= new List<float>();
    private void Start()
    {
        rootXs.Add(PlayerRoot.transform.position.x);
        playerHead = GetComponent<PlayerHead>();
    }

    private void NewRoot()
    {
        StopPlaying();
        SpawnRoot();
    }

    private void SpawnRoot()
    {
        StopPlaying();

        Vector3 bestLocation = new Vector3(UnityEngine.Random.Range(spawnLeft.position.x, spawnRight.position.x), groundLevel.position.y, 0);
        float bestLocationDist = 0;
        for(int i = 0; i < 10; i++)
        {
            var location = new Vector3(UnityEngine.Random.Range(spawnLeft.position.x, spawnRight.position.x), groundLevel.position.y, 0);
            float dist = ClosestDistanceToNeighbor(location.x);
            if(dist > minDistance)
            {
                bestLocation = location;
                break;
            }

            if (bestLocationDist < dist)
            {
                bestLocation = location;
                bestLocationDist = dist;
            }
        }

        Instantiate(TreePrefab, bestLocation, Quaternion.identity);
        PlayerRoot = Instantiate(PlayerRootPrefab, bestLocation, Quaternion.identity);
        playerHead.PlayerRoot = PlayerRoot;

        rootXs.Add(bestLocation.x);
        int numOfRoots = int.Parse(textMeshPro.text);
        textMeshPro.text = (numOfRoots + 1).ToString();

        Invoke("StartPlaying", 0.5f);
    }

    private void HitDeadEnd()
    {
        StopPlaying();
        GetComponent<AudioSource>().Play();
        Invoke("SpawnRoot", 0.75f);
    }

    private void GotWater(Water water)
    {
        water.Drain();
        gm.RemoveWater(water);

        Invoke("NewRoot", 1f);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Root")
        {
            Debug.Log("Collided with root");
            other.gameObject.GetComponent<LineRenderer>().material.color = new Color(1,1,1,0.2f);
            other.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
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
            GotWater(other.GetComponent<Water>());
        }
        else
        {
            Debug.Log(other.name);
        }
    }

    public void StartPlaying()
    {
        PlayerRoot.isRunning = true;
    }

    public void StopPlaying()
    {
        if (PlayerRoot != null)
        {
            PlayerRoot.isRunning = false;
            PlayerRoot.CreateCollider();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Layer1Ground" && playerHead.transform.position.y >= groundLevel.position.y)
        {
            Debug.Log("Exit ground layer 1 up");
            HitDeadEnd();
        }
        else if (other.gameObject.tag == "ScreenZone")
        {
            Debug.Log("Exit the allowed screen zone");
            HitDeadEnd();
        }
    }

    private float ClosestDistanceToNeighbor(float newRootX)
    {
        float closest = 1000;
        foreach(float pos in rootXs)
        {
            float dist = Math.Abs(pos - newRootX);
            if (dist < closest)
            {
                closest = dist;
            }
        }
        return closest;
    }
}
