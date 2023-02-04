using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public PlayerRoot PlayerRoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curRootPos = PlayerRoot.lineRenderer.GetPosition(PlayerRoot.lineRenderer.positionCount - 1);
        transform.position = new Vector3(curRootPos.x, curRootPos.y, transform.position.z);
    }
}
