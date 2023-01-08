using System;
using UnityEngine;

// У нас по-настоящему инклюзивная, гендерно нейтральная команда!
public class CameraMotion : MonoBehaviour
{
    private Camera Cam;
    private bool Flag;
    private float XPos;
    private float ZPos;
    private Vector2 ChangePos1;
    private Vector2 ChangePos2;
    private Touch Touch;
    private Touch Touch1;
    private Touch Touch2;
    private float LastPositionDistance;
    private float Zoom;
    private void Start()
    {
        Cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        { 
            // Получаем координаты касаний
            Touch1 = Input.GetTouch(0);
            Touch2 = Input.GetTouch(1);

            // Получаем координаты начала касаний
            Vector2 TouchDirection1 = Touch1.position - Touch1.deltaPosition;
            Vector2 TouchDirection2 = Touch2.position - Touch2.deltaPosition;

            // Вычисляем расстояния между текущими касаниями
            float PositionDistance = Vector2.Distance(Touch1.position, Touch2.position);

            // Вычисляем рассояние между начальными касаниями
            float DirectionDistance = Vector2.Distance(
                TouchDirection1,
                TouchDirection2
            );
            
            // Если позиция изменилась
            if (ChangePos1 != Touch1.position && ChangePos2 != Touch2.position)
            {
                
                // Если разница между расстояниями существенна, значит делается зум
                if (Mathf.Abs(LastPositionDistance - PositionDistance) > 5)
                {
                    Zoom = PositionDistance - DirectionDistance;
                        
                    // Получаем новый зум камеры
                    float CamSize = Cam.orthographicSize - Zoom / 75;
                    // Если зум не слишком маленький ставим его, иначе 3
                    if (CamSize <= 3)
                    {
                        Cam.orthographicSize = 3;
                    }
                    else if (CamSize >= 16)
                    {
                        Cam.orthographicSize = 16;
                    }
                    else
                    {
                        Cam.orthographicSize = CamSize;
                    }
                    ChangePos1 = Touch1.position;
                    ChangePos2 = Touch2.position;
                    LastPositionDistance = PositionDistance;
                }
            }
            else if ((ChangePos1 == Touch1.position && ChangePos2 != Touch2.position) || (ChangePos1 != Touch1.position && ChangePos2 == Touch2.position))
            {

                // Если разница между расстояниями существенна, значит делается зум
                if (Mathf.Abs(DirectionDistance - PositionDistance) > 5)
                {
                    Zoom = PositionDistance - DirectionDistance;

                    // Получаем новый зум камеры
                    float CamSize = Cam.orthographicSize - Zoom / 75;
                    // Если зум не слишком маленький ставим его, иначе 3
                    if (CamSize <= 3)
                    {
                        Cam.orthographicSize = 3;
                    }
                    else if (CamSize >= 16)
                    {
                        Cam.orthographicSize = 16;
                    }
                    else
                    {
                        Cam.orthographicSize = CamSize;
                    }
                    // 7
                    ChangePos1 = Touch1.position;
                    ChangePos2 = Touch2.position;
                    LastPositionDistance = PositionDistance;
                }
            }
            else
            {
                Zoom = 0;
            }
        }
        // Если идет хотя бы одно касание
        if (Input.touchCount >= 1)
        {
            if (Input.touchCount == 1 || Input.touchCount > 2)
            {
            Touch1 = Input.GetTouch(0);

                // Если изменилась позиция первого пальца
                if (Touch1.position != ChangePos1)
                {
                    // Двигаем камеру
                    // HACK
                    XPos = Touch1.deltaPosition.x * Mathf.Sin(120) - Touch1.deltaPosition.y;
                    ZPos = Touch1.deltaPosition.x + Touch1.deltaPosition.y * Mathf.Sin(120);
                    
                    transform.position = new Vector3(
                        transform.position.x + XPos * Cam.orthographicSize / 300,
                        transform.position.y,
                        transform.position.z + ZPos * Cam.orthographicSize / 300
                    );

                    // Сохраняем текущую позицию
                    ChangePos1 = Touch1.position;
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch1 = Input.GetTouch(0);
                Touch2 = Input.GetTouch(1);
                if (Touch1.position != ChangePos1)
                {
                    Touch = Touch1;
                    // Сохраняем текущую позицию
                    ChangePos1 = Touch1.position;
                    Flag = true;
                }
                else if (Touch2.position != ChangePos2)
                {
                    Touch = Touch2;
                    // Сохраняем текущую позицию
                    ChangePos2 = Touch2.position;
                    Flag = true;
                }
            if (Flag) {
                XPos = Touch.deltaPosition.x * Mathf.Sin(120) - Touch.deltaPosition.y;
                ZPos = Touch.deltaPosition.x + Touch.deltaPosition.y * Mathf.Sin(120);
                transform.position = new Vector3(
                    transform.position.x + XPos * Cam.orthographicSize / 300,
                    transform.position.y,
                    transform.position.z + ZPos * Cam.orthographicSize / 300
                    );
                Flag = false;
            }
            }

        }
        else
        {
            // Уменьшаешь значения чтобы не слишком быстро менялись
            if (Math.Abs(Zoom) > 1 || Math.Abs(XPos) > 1 || Math.Abs(ZPos) > 1) {
                Zoom = Zoom / 1.2f;
                XPos = XPos / 2f;
                ZPos = ZPos / 2f;
            }
            if (Math.Abs(Zoom) > 1)
            {
                float CamSize = Cam.orthographicSize - Zoom / 200;
                if (CamSize <= 3)
                    {
                        Cam.orthographicSize = 3;
                    }
                    else if (CamSize >= 16)
                    {
                        Cam.orthographicSize = 16;
                    }
                    else
                    {
                        Cam.orthographicSize = CamSize;
                    }
                Zoom -= Math.Sign(Zoom);
            }
            if (Math.Abs(XPos) > 1 || Math.Abs(ZPos) > 1)
            {
                if (Math.Abs(XPos) > 1)
                {
                    transform.position = new Vector3(transform.position.x + XPos / 300 * Cam.orthographicSize, transform.position.y, transform.position.z);
                }
                if (Math.Abs(ZPos) > 1)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + ZPos / 300 * Cam.orthographicSize);
                }
            }
        }
    }
}