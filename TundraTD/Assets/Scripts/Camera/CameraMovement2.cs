using UnityEngine;

namespace Camera
{
    public class CameraMovement2 : MonoBehaviour
    {
        [SerializeField] private float minimalCameraSize = 1;
        [SerializeField] private float maximumCameraSize = 8;

        public bool IS_DEBUG = true;
        
        private Vector3 _touchStart;
        private UnityEngine.Camera _mainCamera;

        private void Start()
        {
            _mainCamera = GetComponent<UnityEngine.Camera>();
        }

        private void Update()
        {

            // PC controls
            if (IS_DEBUG)
            {
                MoveCameraUsingPinch(); // works both for PC and mobile
                var scrollDifference = CalculateMouseWheelDifference(); 
                // Zoom doesn't work (I suppose it's because we are in android mode)
                Debug.Log(scrollDifference);
                ApplyZoomOnCamera(scrollDifference);

            }
            else {
                switch (Input.touchCount)
                {
                    case 1:
                        MoveCameraUsingPinch();
                        return;
                    case 2:
                        var pinchDifference = CalculatePinchDifference();
                        ApplyZoomOnCamera(pinchDifference);
                        return;
                }
            }
        }

        private float CalculateMouseWheelDifference()
        {
            return Input.GetAxis("Mouse ScrollWheel");
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
                _touchStart = worldPoint;

            if (Input.GetMouseButton(0))
                _mainCamera.transform.position += _touchStart - worldPoint;
        }
    }
}

/*private void Update()
{
    var touch = Input.GetTouch(0);
    if (touch.phase != TouchPhase.Moved)
        return;

    MoveCamera(touch);
}

private void MoveCamera(Touch touch)
{
    _touchStart = _mainCamera.ScreenToWorldPoint(touch.position);
}*/

