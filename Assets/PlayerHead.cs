using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    public PlayerRoot PlayerRoot;
    private Rigidbody2D rigid;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 prevRootPos = PlayerRoot.curSection.GetPosition(PlayerRoot.curSection.positionCount - 2);
        Vector3 curRootPos = PlayerRoot.curSection.GetPosition(PlayerRoot.curSection.positionCount - 1);

        Vector3 dir = (curRootPos - prevRootPos).normalized;

        rigid.SetRotation(Quaternion.LookRotation(dir, Vector3.up));
        rigid.MovePosition(curRootPos + (dir * 0.5f));
    }
}
