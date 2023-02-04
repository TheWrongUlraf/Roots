using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTree : MonoBehaviour
{
    float maxSize;
    float growSpeed = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        maxSize = UnityEngine.Random.Range(0.25f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += (Time.timeScale * new Vector3(growSpeed, growSpeed, growSpeed));
        }
    }
}
