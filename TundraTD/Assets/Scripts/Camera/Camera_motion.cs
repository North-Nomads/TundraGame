using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

// У нас по-настоящему инклюзивная, гендерно нейтральная команда!
public class Camera_motion : MonoBehaviour
{
    private Camera cam;
    private bool flag;
    private float Xpos;
    private float Zpos;
    private Vector2 changePos1;
    private Vector2 changePos2;
    private Touch touch;
    private Touch touch1;
    private Touch touch2;
    private float lastPositionDistance;
    private float zoom;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        { 
            // Получаем координаты касаний
            touch1 = Input.GetTouch(0);
            touch2 = Input.GetTouch(1);

            // Получаем координаты начала касаний
            Vector2 touchDirection1 = touch1.position - touch1.deltaPosition;
            Vector2 touchDirection2 = touch2.position - touch2.deltaPosition;

            // Вычисляем расстояния между текущими касаниями
            float positionDistance = Vector2.Distance(touch1.position, touch2.position);

            // Вычисляем рассояние между начальными касаниями
            float directionDistance = Vector2.Distance(
                touchDirection1,
                touchDirection2
            );
            
            // Если позиция изменилась
            if (changePos1 != touch1.position && changePos2 != touch2.position)
            {
                
                // Если разница между расстояниями существенна, значит делается зум
                if (Mathf.Abs(lastPositionDistance - positionDistance) > 5)
                {
                    zoom = positionDistance - directionDistance;
                        
                    // Получаем новый зум камеры
                    float camSize = cam.orthographicSize - zoom / 75;
                    // Если зум не слишком маленький ставим его, иначе 3
                    cam.orthographicSize = camSize <= 3 ? 3 : camSize;
                    changePos1 = touch1.position;
                    changePos2 = touch2.position;
                    lastPositionDistance = positionDistance;
                }
            }
            else if ((changePos1 == touch1.position && changePos2 != touch2.position) || (changePos1 != touch1.position && changePos2 == touch2.position))
            {

                // Если разница между расстояниями существенна, значит делается зум
                if (Mathf.Abs(directionDistance - positionDistance) > 5)
                {
                    zoom = positionDistance - directionDistance;

                    // Получаем новый зум камеры
                    float camSize = cam.orthographicSize - zoom / 75;
                    // Если зум не слишком маленький ставим его, иначе 3
                    cam.orthographicSize = camSize <= 3 ? 3 : camSize;
                    changePos1 = touch1.position;
                    changePos2 = touch2.position;
                    lastPositionDistance = positionDistance;
                }
            }
            else
            {
                zoom = 0;
            }
        }
        // Если идет хотя бы одно касание
        if (Input.touchCount >= 1)
        {
            if (Input.touchCount == 1 || Input.touchCount > 2)
            {
            touch1 = Input.GetTouch(0);

                // Если изменилась позиция первого пальца
                if (touch1.position != changePos1)
                {
                    // Двигаем камеру
                    // HACK
                    Xpos = touch1.deltaPosition.x - touch1.deltaPosition.y;
                    Zpos = touch1.deltaPosition.x + touch1.deltaPosition.y;
                    transform.position = new Vector3(
                        transform.position.x + Xpos / 50,
                        transform.position.y,
                        transform.position.z + Zpos / 50
                    );

                    // Сохраняем текущую позицию
                    changePos1 = touch1.position;
                }
            }
            else if (Input.touchCount == 2)
            {
                touch1 = Input.GetTouch(0);
                touch2 = Input.GetTouch(1);
                if (touch1.position != changePos1)
                {
                    touch = touch1;
                    // Сохраняем текущую позицию
                    changePos1 = touch1.position;
                    flag = true;
                }
                else if (touch2.position != changePos2)
                {
                    touch = touch2;

                    // Сохраняем текущую позицию
                    changePos2 = touch2.position;
                    flag = true;
                }
            if (flag) {
                Xpos = touch.deltaPosition.x - touch.deltaPosition.y;
                Zpos = touch.deltaPosition.x + touch.deltaPosition.y;
                transform.position = new Vector3(
                    transform.position.x + Xpos / 50,
                    transform.position.y,
                    transform.position.z + Zpos / 50
                    );
                flag = false;
            }
            }

        }
        else
        {
            // Уменьшаешь значения чтобы не слишком быстро менялись
            if (Math.Abs(zoom) > 1 || Math.Abs(Xpos) > 1 || Math.Abs(Zpos) > 1) {
                zoom = zoom / (float)1.2;
                Xpos = Xpos / (float)1.2;
                Zpos = Zpos / (float)1.2;
            }
            if (Math.Abs(zoom) > 1)
            {
                float camSize = cam.orthographicSize - zoom / 200;
                cam.orthographicSize = camSize <= 3 ? 3 : camSize;
                zoom -= Math.Sign(zoom);
            }
            if (Math.Abs(Xpos) > 1 || Math.Abs(Zpos) > 1)
            {
                if (Math.Abs(Xpos) > 1)
                {
                    transform.position = new Vector3(transform.position.x + Xpos / 75, transform.position.y, transform.position.z);
                }
                if (Math.Abs(Zpos) > 1)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Zpos / 75);
                }
            }
        }
    }
}