using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level
{
    /// <summary>
    ///	Controls: zoom & movement of the camera
    /// </summary>
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float minimalInertiaThreshold = .125f;
        [SerializeField] private float maximalCameraMoveThreshold;
        [SerializeField] private float inertiaMultiplier;
        [SerializeField] private float minimalCameraSize = 1;
        [SerializeField] private float maximumCameraSize = 8;
        [SerializeField] private float wasdCameraMoveSpeed;

        // for debug
        [SerializeField] private bool usingWASD;


        private Vector3 _inertiaDirection;
        private Vector3 _touchStart;
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = GetComponent<Camera>();
            if (!_mainCamera.orthographic)
                throw new Exception("Camera must be in orthographic mode");
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            HandleCameraInertialMovement();
            
            if (usingWASD)
            {
                MoveCameraOnWASD();
            }
            else
            {
                if (Input.touchCount == 1)
                {
                    MoveCameraUsingPinch();
                }
                else if (Input.touchCount == 2)
                {
                    var touchOne = Input.GetTouch(0);
                    var touchTwo = Input.GetTouch(1);
                    var pinchDifference = CalculatePinchDifference(touchOne, touchTwo);
                    MoveCameraUsingPinch();
                    ApplyZoomOnCamera(pinchDifference);
                }
            }
        }

        private void MoveCameraOnWASD()
        {
            var cameraTransform = _mainCamera.transform;
            if (Input.GetKey(KeyCode.A))
                cameraTransform.position += Time.deltaTime * wasdCameraMoveSpeed * -cameraTransform.right;
            if (Input.GetKey(KeyCode.D))
                cameraTransform.position += Time.deltaTime * wasdCameraMoveSpeed * cameraTransform.right;
            if (Input.GetKey(KeyCode.W))
                cameraTransform.position += Time.deltaTime * wasdCameraMoveSpeed * cameraTransform.up;
            if (Input.GetKey(KeyCode.S))
                cameraTransform.position += Time.deltaTime * wasdCameraMoveSpeed * -cameraTransform.up;
        }

        private void HandleCameraInertialMovement()
        {
            if (_inertiaDirection.magnitude < minimalInertiaThreshold)
            {
                _inertiaDirection = Vector3.zero;
                return;
            }

            var insertionOffset = _inertiaDirection * Time.deltaTime;
            _mainCamera.transform.position += insertionOffset;
            _inertiaDirection -= insertionOffset;
        }

        private float CalculatePinchDifference(Touch touchOne, Touch touchTwo)
        {
            var touchOnePositionBefore = touchOne.position - touchOne.deltaPosition;
            var touchTwoPositionBefore = touchTwo.position - touchTwo.deltaPosition;

            var magnitudeBefore = (touchOnePositionBefore - touchTwoPositionBefore).magnitude;
            var magnitudeNow = (touchOne.position - touchTwo.position).magnitude;

            return (magnitudeNow - magnitudeBefore) * Time.deltaTime;
        }

        private void ApplyZoomOnCamera(float zoomDifference)
        {
            _mainCamera.orthographicSize =
                Mathf.Clamp(_mainCamera.orthographicSize - zoomDifference, minimalCameraSize, maximumCameraSize);
        }

        private void MoveCameraUsingPinch()
        {
            var worldPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                _inertiaDirection = Vector3.zero;
                _touchStart = worldPoint;
            }

            var direction = _touchStart - worldPoint;
            if (direction.magnitude > maximalCameraMoveThreshold)
                return;

            if (Input.GetMouseButton(0))
            {
                _mainCamera.transform.position += direction;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _inertiaDirection = direction * inertiaMultiplier;
            }
        }
    }
}