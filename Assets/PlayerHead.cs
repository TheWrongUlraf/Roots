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
        Vector3 curRootPos = PlayerRoot.curSection.GetPosition(PlayerRoot.curSection.positionCount - 1);

        rigid.MovePosition(new Vector3(curRootPos.x, curRootPos.y, transform.position.z));
        //transform.position = new Vector3(curRootPos.x, curRootPos.y, transform.position.z);
    }
}
