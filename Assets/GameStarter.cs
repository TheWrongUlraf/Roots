using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStarter : MonoBehaviour
{
    public RootCollisionHandler player;
    public UnityEvent onStart;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))   
        {
            // start the game
            player.StartPlaying();

            // set the camera
            CinemachineVirtualCamera virtCame = GetComponent<CinemachineVirtualCamera>();
            virtCame.Priority = -1;

            // tell other stuff to start
            onStart.Invoke();
        }
    }
}
