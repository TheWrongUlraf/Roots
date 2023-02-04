using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameloopManager : MonoBehaviour
{
    public List<Water> level1Water;
    public List<Water> level2Water;
    public List<Water> level3Water;

    public List<Material> levelsMaterials;

    public RootCollisionHandler player;

    public UnityEvent onWin;

    public void RemoveWater(Water water)
    {
        level1Water.Remove(water);
        level2Water.Remove(water);
        level3Water.Remove(water);

        CheckNewPermission();
    }

    public void Start()
    {
        for (int i = 1; i < levelsMaterials.Count; i++)
        {
            levelsMaterials[i].color = Color.black;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Won();
        }
    }

    private void CheckNewPermission()
    {
        switch (player.layerPremission)
        {
            case 1:
                if (level1Water.Count != 0)
                {
                    return;
                }
                break;
            case 2:
                if (level2Water.Count != 0)
                {
                    return;
                }
                break;
            case 3:
                if (level3Water.Count != 0)
                {
                    return;
                }
                break;
        }

        // make the player move up a single permission
        player.layerPremission++;
        levelsMaterials[player.layerPremission - 1].color = Color.white;


        if (player.layerPremission == 4)
        {
            Won();
        }
    }

    private void Won()
    {
        player.StopPlaying();
        onWin.Invoke();
    }
}
