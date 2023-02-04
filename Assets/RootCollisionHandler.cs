using System;
using System.Collections;
using System.Collections.Generic;
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

    PlayerRoot PlayerRoot;
    PlayerHead playerHead;

    private void NewRoot()
    {
        StopPlaying();
        SpawnRoot();
    }
    private void SpawnRoot()
    {
        StopPlaying();

        var location = new Vector3(UnityEngine.Random.Range(spawnLeft.position.x, spawnRight.position.x), groundLevel.position.y, 0);
        Instantiate(TreePrefab, location, Quaternion.identity);
        PlayerRoot = Instantiate(PlayerRootPrefab, location, Quaternion.identity);
        playerHead = GetComponent<PlayerHead>();
        playerHead.PlayerRoot = PlayerRoot;
        int numOfRoots = int.Parse(textMeshPro.text);
        textMeshPro.text = (numOfRoots + 1).ToString();

        Invoke("StartPlaying", 0.5f);
    }

    private void Start()
    {
        SpawnRoot();
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
}
