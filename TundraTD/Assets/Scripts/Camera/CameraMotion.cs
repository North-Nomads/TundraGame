using System;
using UnityEngine;

// У нас по-настоящему инклюзивная, гендерно нейтральная команда!
public class CameraMotion : MonoBehaviour
{
    private Vector2 _previousCordsOfFirstFinger;
    private Vector2 _previousCordsOfSecondFinger;
    private Vector3 _pointToZoom;
    private Touch _cordsOfFirstFinger;
    private Touch _cordsOfSecondFinger;
    private Camera _mainCamera;
    private float _previousDistanceBetweenTouch;
    private float _zoom;
    private float _xDistance;
    private float _zDistance;
    private float _xDistanceToMove;
    private float _zDistanceToMove;
    private const float _denominatorOfMovementSpeedOnScreen = 300;
    private const float _cameraMaxSize = 16;
    private const float _cameraMinSize = 3;
    private const float _pixelDifferenceToMove = 15;
    private const float _denominatorOfZoomSpeedOnScreen = 25;
    private const float _xDenominatorOfZoomSpeedToPoint = 7;
    private const float _zDenominatorOfZoomSpeedToPoint = 10;
    private const float _shortageOfZoomToInertia = 1.2f;
    private const float _shortageOfMovementToInertia = 2;
    private const float _denominatorOfZoomSpeedOnScreenOnInertia = 200;
    private const float _xDenominatorOfZoomSpeedToPointOnInertia = 80;
    private const float _zDenominatorOfZoomSpeedToPointOnInertia = 90;

    private void Start()
    {
        _mainCamera = GetComponent<Camera>();
    }

    private void MoveCamera(Touch _touchToMove)
    {
        _xDistance =
            _touchToMove.deltaPosition.x * Mathf.Sin(transform.rotation.y)
            - _touchToMove.deltaPosition.y;
        _zDistance =
            _touchToMove.deltaPosition.x
            + _touchToMove.deltaPosition.y * Mathf.Sin(transform.rotation.y);
        transform.position +=
            new Vector3(
                _xDistance * _mainCamera.orthographicSize,
                0,
                _zDistance * _mainCamera.orthographicSize
            ) / _denominatorOfMovementSpeedOnScreen;
    }

    private void ChangeZoom(float _cameraZoomSize)
    {
        if (_cameraZoomSize <= _cameraMinSize)
        {
            _mainCamera.orthographicSize = _cameraMinSize;
        }
        else if (_cameraZoomSize >= _cameraMaxSize)
        {
            _mainCamera.orthographicSize = _cameraMaxSize;
        }
        else
        {
            _mainCamera.orthographicSize = _cameraZoomSize;
        }
    }

    private Vector3 GetPointToZoom()
    {
        return new Vector3(
            (
                _mainCamera.ScreenToWorldPoint(_cordsOfFirstFinger.position).x
                + _mainCamera.ScreenToWorldPoint(_cordsOfSecondFinger.position).x
            ) / 2,
            transform.position.y,
            (
                _mainCamera.ScreenToWorldPoint(_cordsOfFirstFinger.position).z
                + _mainCamera.ScreenToWorldPoint(_cordsOfSecondFinger.position).z
            ) / 2
        );
    }

    private void FixedUpdate()
    {
        if (Input.touchCount == 2)
        {
            _cordsOfFirstFinger = Input.GetTouch(0);
            _cordsOfSecondFinger = Input.GetTouch(1);

            if (_cordsOfSecondFinger.position != _previousCordsOfSecondFinger)
            {
                MoveCamera(_cordsOfSecondFinger);
            }

            float _distanceBetweenTouch = Vector2.Distance(
                _cordsOfFirstFinger.position,
                _cordsOfSecondFinger.position
            );

            float _distanceBetweenMovedFingers = Vector2.Distance(
                _cordsOfFirstFinger.position - _cordsOfFirstFinger.deltaPosition,
                _cordsOfSecondFinger.position - _cordsOfSecondFinger.deltaPosition
            );
            if (
                _previousCordsOfFirstFinger != _cordsOfFirstFinger.position
                || _previousCordsOfSecondFinger != _cordsOfSecondFinger.position
            )
            {
                if (
                    Mathf.Abs(_previousDistanceBetweenTouch - _distanceBetweenTouch)
                    > _pixelDifferenceToMove
                )
                {
                    _zoom = _distanceBetweenTouch - _distanceBetweenMovedFingers;
                    float _cameraZoomSize = _mainCamera.orthographicSize;
                    if (_zoom > 0)
                    {
                        _pointToZoom = GetPointToZoom();
                        _cameraZoomSize =
                            _mainCamera.orthographicSize - _zoom / _denominatorOfZoomSpeedOnScreen;
                        transform.position = new Vector3(
                            transform.position.x
                                - (transform.position.x - _pointToZoom.x)
                                    / _xDenominatorOfZoomSpeedToPoint,
                            transform.position.y,
                            transform.position.z
                                - (transform.position.z - _pointToZoom.z)
                                    / _zDenominatorOfZoomSpeedToPoint
                        );
                    }
                    else
                    {
                        _cameraZoomSize =
                            _mainCamera.orthographicSize - _zoom / _denominatorOfZoomSpeedOnScreen;
                    }
                    ChangeZoom(_cameraZoomSize);
                    _previousCordsOfFirstFinger = _cordsOfFirstFinger.position;
                    _previousCordsOfSecondFinger = _cordsOfSecondFinger.position;
                    _previousDistanceBetweenTouch = _distanceBetweenTouch;
                }
            }
            else
            {
                _zoom = 0;
            }
        }
        // Если есть одно касание
        if (Input.touchCount == 1)
        {
            _cordsOfFirstFinger = Input.GetTouch(0);

            if (_cordsOfFirstFinger.position != _previousCordsOfFirstFinger)
            {
                MoveCamera(_cordsOfFirstFinger);
                _previousCordsOfFirstFinger = _cordsOfFirstFinger.position;
            }
        }
        else
        {
            if (
                Math.Abs(_zoom) > 1
                || Math.Abs(_xDistanceToMove) > 1
                || Math.Abs(_zDistanceToMove) > 1
            )
            {
                _zoom = _zoom / _shortageOfZoomToInertia;
                _xDistanceToMove = _xDistanceToMove / _shortageOfMovementToInertia;
                _zDistanceToMove = _zDistanceToMove / _shortageOfMovementToInertia;
            }
            if (Math.Abs(_zoom) > 1)
            {
                float _cameraZoomSize = _mainCamera.orthographicSize;

                if (_zoom > 0)
                {
                    _pointToZoom = GetPointToZoom();

                    _cameraZoomSize =
                        _mainCamera.orthographicSize
                        - _zoom / _denominatorOfZoomSpeedOnScreenOnInertia;

                    transform.position = new Vector3(
                        transform.position.x
                            - (transform.position.x - _pointToZoom.x)
                                / _xDenominatorOfZoomSpeedToPointOnInertia,
                        transform.position.y,
                        transform.position.z
                            - (transform.position.z - _pointToZoom.z)
                                / _zDenominatorOfZoomSpeedToPointOnInertia
                    );
                }
                else
                {
                    _cameraZoomSize =
                        _mainCamera.orthographicSize
                        - _zoom / _denominatorOfZoomSpeedOnScreenOnInertia;
                }
                ChangeZoom(_cameraZoomSize);
                _zoom -= Math.Sign(_zoom);
            }
            if (Math.Abs(_xDistanceToMove) > 1 || Math.Abs(_zDistanceToMove) > 1)
            {
                transform.position = new Vector3(
                    transform.position.x
                        + _xDistanceToMove
                            / _denominatorOfMovementSpeedOnScreen
                            * _mainCamera.orthographicSize,
                    transform.position.y,
                    transform.position.z
                        + _zDistanceToMove
                            / _denominatorOfMovementSpeedOnScreen
                            * _mainCamera.orthographicSize
                );
            }
        }
    }
}
