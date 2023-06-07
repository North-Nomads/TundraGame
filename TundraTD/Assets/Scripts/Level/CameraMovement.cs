using System;
using ModulesUI;
using UnityEngine;
using CanvasGroup = ModulesUI.CanvasGroup;

namespace Level
{
    /// <summary>
    ///	Controls: zoom & movement of the camera
    /// </summary>
    public class CameraMovement : MonoBehaviour
    {
        [Tooltip("Up Right Down Left")][SerializeField] private Transform[] border;
        [SerializeField] private float minimalInertiaThreshold = .125f;
        [SerializeField] private float minimalCameraMoveThreshold;
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
        
        // Camera borders
        private float _upBorderPosition;
        private float _rightBorderPosition;
        private float _bottomBorderPosition;
        private float _leftBorderPosition;
        private float _cameraMovingTime;

        public bool CameraMoving { get; private set; }

        private void Start()
        {
            if (border.Length != 4)
                throw new FormatException("Border list in camera must contain 4 objects for coordinates of borders");

            _upBorderPosition = border[0].position.x;
            _rightBorderPosition = border[1].position.z;
            _bottomBorderPosition = border[2].position.x;
            _leftBorderPosition = border[3].position.z;

            _mainCamera = GetComponent<Camera>();
            if (!_mainCamera.orthographic)
                throw new NotSupportedException("Camera must be in orthographic mode");
        }

        private void Update()
        {
            HandleCameraInertialMovement();

            if (UIToggle.BlockedGroups.HasFlag(CanvasGroup.Camera))
                return;

            if (usingWASD)
            {
                MoveCameraOnWASD();
            }
            else
            {
                MoveCameraUsingPinch();
                //if (Input.touchCount == 1)
                //{

                //}
                if (Input.touchCount == 2)
                {
                    var touchOne = Input.GetTouch(0);
                    var touchTwo = Input.GetTouch(1);
                    var pinchDifference = CalculatePinchDifference(touchOne, touchTwo);
                    MoveCameraUsingPinch();
                    ApplyZoomOnCamera(pinchDifference);
                }
            }
        }

        private void ClampCameraMovement(Vector3 newPosition)
        {
            var position = transform.position;

            position.x = Mathf.Clamp(newPosition.x, _bottomBorderPosition, _upBorderPosition);
            position.z = Mathf.Clamp(newPosition.z, _leftBorderPosition, _rightBorderPosition);
            
            _mainCamera.transform.position = position;
        }

        private void MoveCameraOnWASD()
        {
            var cameraTransform = _mainCamera.transform;
            var newPos = _mainCamera.transform.position;
            if (Input.GetKey(KeyCode.A))
                newPos += Time.deltaTime * wasdCameraMoveSpeed * -cameraTransform.right;
            if (Input.GetKey(KeyCode.D))
                newPos += Time.deltaTime * wasdCameraMoveSpeed * cameraTransform.right;
            if (Input.GetKey(KeyCode.W))
                newPos += Time.deltaTime * wasdCameraMoveSpeed * cameraTransform.up;
            if (Input.GetKey(KeyCode.S))
                newPos += Time.deltaTime * wasdCameraMoveSpeed * -cameraTransform.up;

            ClampCameraMovement(newPos);
        }

        private void HandleCameraInertialMovement()
        {
            if (_inertiaDirection.magnitude < minimalInertiaThreshold)
            {
                _inertiaDirection = Vector3.zero;
                return;
            }

            var insertionOffset = _inertiaDirection * Time.deltaTime;
            ClampCameraMovement(_mainCamera.transform.position + insertionOffset);
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
            if (Input.GetMouseButtonUp(0))
            {
                _cameraMovingTime = 0;
                CameraMoving = false;
            }
            var direction = _touchStart - worldPoint;
            //if (direction.magnitude > maximalCameraMoveThreshold)
            //    return;

            //if (Input.GetMouseButton(0))
            //{
            //    ClampCameraMovement(_mainCamera.transform.position + direction);
            //}

            if (Input.GetMouseButton(0))
            {
                if (_cameraMovingTime < minimalCameraMoveThreshold)
                {
                    _cameraMovingTime += Time.deltaTime;
                }
                else
                {
                    CameraMoving = true;
                    _inertiaDirection = direction * inertiaMultiplier;
                }
            }
        }
    }
}