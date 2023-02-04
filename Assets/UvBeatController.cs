using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UvBeatController : MonoBehaviour
{
    public float minSizeMultiplayer;
    public float maxSizeMultiplayer;
    public float timePerBeat;
    public EasingFunction.Ease ease;

    private bool inhale;
    private float currentTime;

    private SpriteShapeRenderer spriteShapeRenderer;
    private List<Material> mats;

    // Start is called before the first frame update
    void Start()
    {
        mats = new List<Material>();
        spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();

        spriteShapeRenderer.GetMaterials(mats);
    }

    // Update is called once per frame
    void Update()
    {

        if (inhale)
        {
            currentTime += Time.deltaTime;

            // reached max
            if (currentTime >= timePerBeat)
            {
                inhale = false;
                currentTime = timePerBeat;
            }
        }
        else if (!inhale)
        {
            currentTime -= Time.deltaTime;

            // reached max
            if (currentTime <= 0)
            {
                inhale = true;
                currentTime = 0;
            }
        }

        EasingFunction.Function easing = EasingFunction.GetEasingFunction(ease);
        float scale = easing(minSizeMultiplayer, maxSizeMultiplayer, currentTime / timePerBeat);
        Vector2 scaleV = scale * new Vector2(1, 1);

        mats[0].SetTextureScale("_MainTex", scaleV);
        mats[1].SetTextureOffset("_MainTex", scaleV);
    }
}
