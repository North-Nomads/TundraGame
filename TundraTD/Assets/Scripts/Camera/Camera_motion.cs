using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Camera_motion : MonoBehaviour
{
    private Camera cam;
    private Vector2 startPos;
    private Vector2 changePos;
    private Vector2 changePos1;
    private Vector2 changePos2;
    private Touch touch1;
    private Touch touch2;
    private float lastPositionDistance;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        // Можно ли заменить Input.GetMouseButtonDown(0) (ПРОВЕРИТЬ)
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.touchCount == 2)
        {
            if (changePos1 != Input.GetTouch(0).position && changePos2 != Input.GetTouch(1).position)
            {
                touch1 = Input.GetTouch(0);
                touch2 = Input.GetTouch(1);
                Vector2 touchDirection1 = touch1.position - touch1.deltaPosition;
                Vector2 touchDirection2 = touch2.position - touch2.deltaPosition;
                float positionDistance = Vector2.Distance(touch1.position, touch2.position);
                float directionDistance = Vector2.Distance(touch1.deltaPosition, touch2.deltaPosition);
                if (Mathf.Abs(lastPositionDistance - positionDistance) > 15)
                    {
                    if (lastPositionDistance < positionDistance)
                    {
                        float zoom = positionDistance - directionDistance;
                        float camSize = cam.orthographicSize - zoom / 300;
                        cam.orthographicSize = camSize <= 3 ? 3 : camSize;
                    }
                    else if (lastPositionDistance >= positionDistance)
                    {
                        float zoom = directionDistance - positionDistance;
                        float camSize = cam.orthographicSize - zoom / 300;
                        cam.orthographicSize = camSize <= 3 ? 3 : camSize;
                    }
                    changePos1 = touch1.position;
                    changePos2 = touch2.position;
                    lastPositionDistance = positionDistance;
                }
                else
                {
                    touch1 = Input.GetTouch(0);
                    touch2 = Input.GetTouch(1);
                }
            }
        }
        if (Input.touchCount == 1 || Input.touchCount == 2)
        {
            if (Input.GetTouch(0).position != changePos)
            {
                // Если заменить mousePosition на GetTouch(0).position, то начнётся кавардак со скоростью.
                // Проверить, можно ли как-то учитывать тапы, а не (типо) мышку
                float Xpos = Input.mousePosition.x - startPos.x;
                float Zpos = Input.mousePosition.y - startPos.y;
                transform.position = new Vector3(transform.position.x - Xpos / 40, transform.position.y, transform.position.z - Zpos / 40);
                changePos = Input.GetTouch(0).position;
            }
            else
            {
                startPos = Input.mousePosition;
            }
        }
    }
}
