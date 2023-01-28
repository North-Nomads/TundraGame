﻿using UnityEngine;

namespace Camera
{
    public class CameraMovement2 : MonoBehaviour
    {
        
        [SerializeField] private float minimalInertiaThreshold = .125f;
        [SerializeField] private float maximalCameraMoveThreshold;
        [SerializeField] private float inertiaMultiplier;
        [SerializeField] private float minimalCameraSize = 1;
        [SerializeField] private float maximumCameraSize = 8;

        private Vector3 _inertiaDirection;
        private Vector3 _cameraSpeed;
        private Vector3 _touchStart;
        private UnityEngine.Camera _mainCamera;
        private float _timeSinceLastInput;

        private void Start()
        {
            _mainCamera = GetComponent<UnityEngine.Camera>();
        }

        private void Update()
        {
            HandleCameraInertialMovement();

            if (Input.touchCount == 1)
            {
                MoveCameraUsingPinch();
            }
            else if (Input.touchCount == 2)
            {
                var pinchDifference = CalculatePinchDifference();
                ApplyZoomOnCamera(pinchDifference);
            }
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

        private float CalculatePinchDifference()
        {
            var touchOne = Input.GetTouch(0);
            var touchTwo = Input.GetTouch(1);

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