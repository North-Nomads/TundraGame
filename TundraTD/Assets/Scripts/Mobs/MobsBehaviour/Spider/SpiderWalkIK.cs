using System.Runtime.CompilerServices;
using UnityEngine;

public class SpiderWalkIK : MonoBehaviour
{
    [SerializeField] Transform[] legPositions;
    [SerializeField] GameObject[] legControllers;
    [SerializeField] float timeToStep;

    private int _step = 0;
    private float _timePassedStep;
    private Vector3[] _restPositions;
    private Vector3[] _targetPositions;
    private Vector3[] _startPositions;
    private Rigidbody _rigidbody;
    private bool _stunned = false;

    //todo
    //поиграть с оффсетом

    static private Vector3[] _directions;

    public bool Stunned { get => _stunned; set => _stunned = value; }

    void Start()
    {
        BakeDirections();
        _restPositions = new Vector3[8];
        for(int i = 0; i < 8; i++)
        {
            _restPositions[i] = legControllers[i].transform.parent.parent.parent.parent.InverseTransformPoint(legControllers[i].transform.position);
        }
        _targetPositions = new Vector3[8];
        _startPositions = new Vector3[8];
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        GetPlacementPositions();
    }

    private static void BakeDirections()
    {
        _directions = new Vector3[50];
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;
        for (int i = 0; i < 50; i++)
        {
            float t = (float)i / 50;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);
            _directions[i] = new Vector3(x, y, z);
        }
    }
    
    private Vector3 CenteredSlerp(Vector3 a, Vector3 b, float t)
    {
        Vector3 center = (a + b) * 0.5f;
        center += 0.9f * transform.TransformDirection(Vector3.down);
        Vector3 relativeA = a - center; 
        Vector3 relativeB = b - center;

        return Vector3.Slerp(relativeA, relativeB, t) + center;
    }

    private Vector3 MultiRayCast(Vector3 point)
    {
        var angle = Quaternion.FromToRotation(_directions[0], point - transform.position);
        for (int i = 0; i < 50; i++)
        {
            if (Physics.Raycast(transform.position, angle * _directions[i], out RaycastHit hit, 3f, 2049))
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }

    private bool GetPlacementPosition(Vector3 target, out Vector3 postion)
    {
        postion = MultiRayCast(target);
        return postion != Vector3.zero;
    }

    private void GetPlacementPositions()
    {
        for(int  i = 0; i < 8; i++)
            _startPositions[i] = legControllers[i].transform.position;
        for(int i = _step % 2; i < 8; i += 2)
        {
            if(!GetPlacementPosition(legPositions[i].position + _rigidbody.velocity * timeToStep, out _targetPositions[i]))
            {
                _targetPositions[i] = transform.position;
            }
        }
        for(int i = (1 + _step) % 2; i < 8; i += 2)
        {
            if(!GetPlacementPosition(gameObject.transform.TransformPoint(_restPositions[i]), out _targetPositions[i]))
            {
                _targetPositions[i] = transform.position;
            }
        }
    }

    private bool Step()
    {
        _timePassedStep += Time.deltaTime/timeToStep;
        for(int i = _step % 2; i < 8; i += 2)
        {   
            legControllers[i].transform.position = CenteredSlerp(_startPositions[i], _targetPositions[i],  _timePassedStep);
        }
        for (int i = (1 + _step) % 2; i < 8; i += 2)
        {
            legControllers[i].transform.position = Vector3.Lerp(_startPositions[i], _targetPositions[i], _timePassedStep);
        }
        return _timePassedStep < 1;
    }

    void RotateToGroundNormal()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity)){
            if(transform.up.y - hit.normal.y < 0.005f)
                return;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, Time.deltaTime * 30);
        }
    }

    void Update()
    {
        if (Stunned)
        {
            print("stun");
            return;
        }

        RotateToGroundNormal();
        
        if(!Step())
        {
            _step++;
            _timePassedStep = 0;
            GetPlacementPositions();
        }
    }
}
