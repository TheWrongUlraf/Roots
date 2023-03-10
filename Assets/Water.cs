using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float timeToDisapear = 2;
    public EasingFunction.Ease ease;

    private float timeSinceDrained = 0;

    public void Drain()
    {
        StartCoroutine(Disappear());
        GetComponent<AudioSource>().Play();
    }

    private IEnumerator Disappear()
    {
        var startScale = transform.localScale.x;
        while (timeSinceDrained < timeToDisapear)
        {
            timeSinceDrained += Time.deltaTime;

            EasingFunction.Function easeFunc = EasingFunction.GetEasingFunction(ease);
            transform.localScale = easeFunc(startScale, 0, timeSinceDrained / timeToDisapear) * new Vector3(1, 1, 1);

            yield return null;
        }

        Destroy(gameObject);
    }
}
