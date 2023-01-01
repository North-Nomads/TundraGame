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
    private Vector3 changePos;
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
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.touchCount == 2)
        {
            if (cam.orthographicSize < 3)
            {
                cam.orthographicSize = 3;
            }
            if (cam.orthographicSize >= 3 && changePos1 != Input.GetTouch(0).position && changePos2 != Input.GetTouch(1).position)
            {
                touch1 = Input.GetTouch(0);
                touch2 = Input.GetTouch(1);
                Vector2 touchDirection1 = touch1.position - touch1.deltaPosition;
                Vector2 touchDirection2 = touch2.position - touch2.deltaPosition;
                float positionDistance = Vector2.Distance(touch1.position, touch2.position);
                float directionDistance = Vector2.Distance(touch1.deltaPosition, touch2.deltaPosition);
                if (lastPositionDistance < positionDistance)
                {
                    float zoom = positionDistance - directionDistance;
                    float camSize = cam.orthographicSize - zoom / 1000;
                    cam.orthographicSize = camSize <= 3 ? 3 : camSize;
                }
                else
                {
                    float zoom = directionDistance - positionDistance;
                    float camSize = cam.orthographicSize - zoom / 1000;
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
        else if (Input.touchCount == 1)
        {
            if (Input.mousePosition != changePos)
            {
                float Xpos = Input.mousePosition.x - startPos.x;
                float Zpos = Input.mousePosition.y - startPos.y;
                transform.position = new Vector3(transform.position.x - Xpos / 50, transform.position.y, transform.position.z - Zpos / 50);
                changePos = Input.mousePosition;
            }
            else
            {
                startPos = Input.mousePosition;
            }
        }
        
    }
}
