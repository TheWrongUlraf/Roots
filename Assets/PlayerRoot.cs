using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoot : MonoBehaviour
{
    public Material material;
    public LineRenderer curSection;

    float width;
    Color lineColor = new Color(99, 34, 16);

    float rotationSpeed = 3f;
    float moveSpeed = 50f;

    bool isHole = false;
    float holeSpacing;
    float fullSpacing;
    float curHoleSpacing = 0f;

    void Start()
    {
        width = 0.75f;
        holeSpacing = width * 1.75f;
        fullSpacing = holeSpacing * 5;
        curSection = CreateNextSection(Vector3.zero, new Vector3(0, -1, 0));
    }

    void Update()
    {
        Vector3 newAddition = GetNextAddition();
        curHoleSpacing += newAddition.magnitude;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            newAddition = Quaternion.Euler(0, 0, -Time.deltaTime * rotationSpeed * 50) * newAddition;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            newAddition = Quaternion.Euler(0, 0, Time.deltaTime * rotationSpeed * 50) * newAddition;
        }

        curSection.positionCount++;
        int posCount = curSection.positionCount;
        curSection.SetPosition(posCount - 1, curSection.GetPosition(posCount - 2) + newAddition);

        if (isHole && curHoleSpacing >= holeSpacing)
        {
            isHole = false;
            curHoleSpacing = 0;

            LineRenderer newSection = CreateNextSection(curSection.GetPosition(posCount - 2), curSection.GetPosition(posCount - 1));
            Destroy(curSection);
            curSection = newSection;
        }
        else if (!isHole && curHoleSpacing >= fullSpacing)
        {
            isHole = true;
            curHoleSpacing = 0;

            curSection = CreateNextSection(curSection.GetPosition(posCount - 2), curSection.GetPosition(posCount - 1));
            curSection.enabled= false;
        }
    }

    private LineRenderer CreateNextSection(Vector3 start0, Vector3 start1)
    {
        GameObject curLineSegmentParent = new GameObject("LineSegment");
        curLineSegmentParent.transform.parent = gameObject.transform;
        LineRenderer nextSection = curLineSegmentParent.AddComponent<LineRenderer>();

        nextSection.material = material;
        nextSection.startColor = lineColor;
        nextSection.endColor = lineColor;
        nextSection.sortingOrder = 10;
        nextSection.startWidth = width;
        nextSection.endWidth = width;

        nextSection.positionCount = 2;
        nextSection.SetPosition(0, start0);
        nextSection.SetPosition(1, start1);

        return nextSection;
    }

    Vector3 GetNextAddition()
    {
        int posCount = curSection.positionCount;
        Vector3 lastLineStart = curSection.GetPosition(posCount - 2);
        Vector3 lastLineEnd = curSection.GetPosition(posCount - 1);

        Vector3 lastLine = lastLineEnd - lastLineStart;
        float step = moveSpeed * Time.deltaTime / 10;
        lastLine *= step / lastLine.magnitude;

        return lastLine;
    }
}
