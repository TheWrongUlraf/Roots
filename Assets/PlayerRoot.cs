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
    public Vector3 startPoint;

    float width;
    public Color lineColor;

    float rotationSpeed = 3f;
    float moveSpeed = 50f;
    float tolarance = 0.025f;

    bool isHole = false;
    float holeSpacing;
    float fullSpacing;
    float curHoleSpacing = 0f;

    void Start()
    {
        width = 0.75f;
        holeSpacing = width * 1.75f;
        fullSpacing = holeSpacing * 5;

        Vector3 startPoint = transform.position;
        curSection = CreateNextSection(startPoint, startPoint + new Vector3(0, -1, 0));
    }

    void FixedUpdate()
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
            Destroy(curSection.gameObject);
            curSection = newSection;
        }
        else if (!isHole && curHoleSpacing >= fullSpacing)
        {
            isHole = true;
            curHoleSpacing = 0;

            var nextSection = CreateNextSection(curSection.GetPosition(posCount - 2), curSection.GetPosition(posCount - 1));

            curSection.Simplify(tolarance);
            CreateCollider();
            curSection = nextSection;
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
    private void CreateCollider()
    {
        PolygonCollider2D polyCollider = curSection.AddComponent<PolygonCollider2D>();
        polyCollider.CreateMesh(true, true);
        Vector2[] points = new Vector2[curSection.positionCount * 2];
        for (int i = 0; i < curSection.positionCount; i++)
        {
            Vector2 curPos = curSection.GetPosition(i);
            Vector2 curDir;
            if (i == 0)
            {
                Vector2 nextPos = curSection.GetPosition(i + 1);
                curDir = nextPos - curPos;
            }
            else
            {
                Vector2 prevPos = curSection.GetPosition(i - 1);
                curDir = curPos - prevPos;
            }

            Vector2 offset = (Quaternion.Euler(0, 0, 90) * curDir).normalized * width / 2;
            points[i] = curPos + offset;
            points[curSection.positionCount * 2 - 1 - i] = curPos - offset;
        }

        polyCollider.points = points;
    }
}
