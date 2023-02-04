using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoot : MonoBehaviour
{
    // Draws a line from "startVertex" var to the current mouse position.
    public LineRenderer lineRenderer;
    public float lineWidth = 0.25f;

    public float rotationSpeed = 5f;
    public float moveSpeed = 10f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, new Vector3(1, 0, 0));
    }

    void Update()
    {
        lineRenderer.widthMultiplier = lineWidth;

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ContinueLastLine(Quaternion.Euler(0, 0, Time.deltaTime * rotationSpeed * 50));
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ContinueLastLine(Quaternion.Euler(0, 0, -Time.deltaTime * rotationSpeed * 50));
        }

        ContinueLastLine();
    }

    void ContinueLastLine()
    {
        int posCount = lineRenderer.positionCount;
        Vector3 lastLineStart = lineRenderer.GetPosition(posCount - 2);
        Vector3 lastLineEnd = lineRenderer.GetPosition(posCount - 1);

        float step = moveSpeed * Time.deltaTime / 10;
        Vector3 lastLine = lastLineEnd - lastLineStart;
        lastLine *= 1 + (step / lastLine.magnitude);
        lineRenderer.SetPosition(posCount - 1, lastLineStart + lastLine);
    }

    void ContinueLastLine(Quaternion newAngle)
    {
        int posCount = lineRenderer.positionCount;
        Vector3 lastLineStart = lineRenderer.GetPosition(posCount - 2);
        Vector3 lastLineEnd = lineRenderer.GetPosition(posCount - 1);

        float step = moveSpeed * Time.deltaTime / 50;
        Vector3 lastLine = lastLineEnd - lastLineStart;
        lastLine = newAngle * lastLine;
        lastLine *= step / lastLine.magnitude;

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(posCount, lastLineEnd + lastLine);
    }
}
