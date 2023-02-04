using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTree : MonoBehaviour
{
    float maxSize;
    float growSpeed = 0.001f;
    public float anglesAllowed = 15;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        maxSize = UnityEngine.Random.Range(0.5f, 1f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, Random.Range(-anglesAllowed, anglesAllowed)));
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + UnityEngine.Random.Range(-0.1f, 0.1f), transform.rotation.w);
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
