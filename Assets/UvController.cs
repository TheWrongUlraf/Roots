using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UvController : MonoBehaviour
{
    public Vector2 offsetSpeedMainTexture;
    public Vector2 offsetSpeedBorderTexture;

    private Vector2 offsetMainTexture;
    private Vector2 offsetBorderTexture;
    private SpriteShapeRenderer spriteShapeRenderer;
    private List<Material> mats;

    // Start is called before the first frame update
    void Start()
    {
        mats = new List<Material>();
        spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();
        if (spriteShapeRenderer != null)
        {
            spriteShapeRenderer.GetMaterials(mats);
        }
        else
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            mats.Add(sr.material);
        }
    }

    // Update is called once per frame
    void Update()
    {
        offsetMainTexture += offsetSpeedMainTexture * Time.deltaTime;
        offsetBorderTexture += offsetSpeedBorderTexture * Time.deltaTime;

        if (spriteShapeRenderer)
        {
            mats[0].SetTextureOffset("_MainTex", offsetMainTexture);
            mats[1].SetTextureOffset("_MainTex", offsetBorderTexture);
        }
        else
        {
            foreach(Material mat in mats)
            {
                mat.SetTextureOffset("_MainTex", offsetMainTexture);
            }
        }
    }
}
