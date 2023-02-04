using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutControl : MonoBehaviour
{

    CinemachineVirtualCamera component;

    // Start is called before the first frame update
    void Start()
    {
        component = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            component.Priority = 11;
        }
        else
        {
            component.Priority = 9;
        }
    }
}
