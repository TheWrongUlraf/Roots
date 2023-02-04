using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameloopManager : MonoBehaviour
{
    public List<Water> level1Water;
    public List<Water> level2Water;
    public List<Water> level3Water;

    RootCollisionHandler player;

    public void RemoveWater(Water water)
    {
        level1Water.Remove(water);
        level2Water.Remove(water);
        level3Water.Remove(water);

        CheckNewPermission();
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
                if (level2Water.Count == 0)
                {
                    return;
                }
                break;
            case 3:
                if (level3Water.Count == 0)
                {
                    return;
                }
                break;
        }

        // make the player move up a single permission
        player.layerPremission++;

        if (player.layerPremission == 4)
        {
            Won();
        }
    }

    private void Won()
    {
        throw new NotImplementedException("CONGRATS");
    }
}
