using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class GameloopManager : MonoBehaviour
{
    public GameObject level1Waters;
    public GameObject level2Waters;
    public GameObject level3Waters;

    private List<Water> level1Water;
    private List<Water> level2Water;
    private List<Water> level3Water;

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
        isWon = false;

        level1Water = new List<Water>(level1Waters.GetComponentsInChildren<Water>());
        level2Water = new List<Water>(level2Waters.GetComponentsInChildren<Water>());
        level3Water = new List<Water>(level3Waters.GetComponentsInChildren<Water>());


        CreateMatsList();
        for (int i = 1; i < levelsMaterials.Count; i++)
        {
            SetAllMaterialByOriginalMaterial(levelsMaterials[i], Color.black);
        }

        SetWater(level2Water, false);
        SetWater(level3Water, false);
    }

    private void SetWater(List<Water> waters, bool enabled)
    {
        foreach(Water w in waters)
        {
            w.gameObject.SetActive(enabled);
        }
    }

    private List<Material> _allMaterial = new List<Material>();
    public bool isWon;

    private void CreateMatsList()
    {
        SpriteShapeRenderer[] spriteShapes = GetComponentsInChildren<SpriteShapeRenderer>();
        foreach (SpriteShapeRenderer spriteShape in spriteShapes)
        {
            List<Material> mats = new List<Material>();
            spriteShape.GetMaterials(mats);

            _allMaterial.AddRange(mats);
        }
    }

    private void SetAllMaterialByOriginalMaterial(Material ogMat, Color newColor)
    {
        foreach (Material mat in _allMaterial)
        {
            if (mat.name.Contains(ogMat.name))
            {
                mat.color = newColor;
            }
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
                SetWater(level2Water, true);
                break;
            case 2:
                if (level2Water.Count != 0)
                {
                    return;
                }
                SetWater(level3Water, true);
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
        if (player.layerPremission <= levelsMaterials.Count)
        {
            SetAllMaterialByOriginalMaterial(levelsMaterials[player.layerPremission - 1], Color.white);
            //levelsMaterials[player.layerPremission - 1].material.color = Color.white;
        }


        if (player.layerPremission == 4)
        {
            Won();
        }
    }

    private void Won()
    {
        isWon = true;
        player.StopPlaying();
        onWin.Invoke();
    }
}
