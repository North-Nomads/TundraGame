using UnityEngine;

public class SpiderWalkIK : MonoBehaviour
{
    [SerializeField] Transform[] legPositions;
    [SerializeField] GameObject[] legControllers;
    [SerializeField] float timeToStep;

    private int _step = 0;
    private float _timePassed;
    private Vector3[] _restPositions;
    private Vector3[] _targetPositions;
    private Vector3[] _startPositions;
    private Rigidbody _rigidbody;
    /*
     В класс заходит новый учитель с огромным косяком, садится, запаливает, тянет…
        — Дети, я ваш новый учитель биологии, Пётр Сергеевич…
    Затягивается, выдыхает…
        — Сегодня я расскажу вам про рыбу–пилу…
    Затягивается…
        — Эта рыба обитает на са–а–амом дне океана, у дна, в вечной мгле…
    …ещё тяжка…
        — Но иногда она выходит на берег, и начинает пилить деревья…
    Затягивается, выдыхает… задумчиво:
        — Пиздец… Ебанутая рыба…
     */


    //todo
    //поворачивать паука
    //альтернативная расстановка ног
    //поиграть с оффсетом

    void Start()
    {
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
    
    private Vector3 CenteredSlerp(Vector3 a, Vector3 b, float t)
    {
        Vector3 center = (a + b) * 0.5f;
        center -= new Vector3(0, 0.9f, 0);
        Vector3 relativeA = a - center; 
        Vector3 relativeB = b - center;

        return Vector3.Slerp(relativeA, relativeB, t) + center;
    }

    private bool GetPlacementPosition(Vector3 target, out Vector3 postion, bool bs)
    {
        var res = Physics.Raycast(gameObject.transform.position, target - transform.position, out RaycastHit hit, 4f, ~256);
        if (!bs)
            Debug.DrawRay(gameObject.transform.position, target, Color.green, 1, true);
        postion = hit.point;
        return res;
    }

    private void GetPlacementPositions()
    {
        for(int  i = 0; i < 8; i++)
            _startPositions[i] = legControllers[i].transform.position;
        for(int i = _step % 2; i < 8; i += 2)
        {
            if(!GetPlacementPosition(legPositions[i].position + _rigidbody.velocity * timeToStep, out _targetPositions[i], false))
            {
                Debug.Log($"Failed to get position of {i} leg on step");
                _targetPositions[i] = transform.position;
            }
        }
        for(int i = (1 + _step) % 2; i < 8; i += 2)
        {
            if(!GetPlacementPosition(gameObject.transform.TransformPoint(_restPositions[i]), out _targetPositions[i], true))
            {
                Debug.Log($"Failed to get position of {i} leg on recovery");
                _targetPositions[i] = transform.position;
            }
        }
    }

    private bool Step()
    {
        _timePassed += Time.deltaTime/timeToStep;
        for(int i = _step % 2; i < 8; i += 2)
        {   
            legControllers[i].transform.position = CenteredSlerp(_startPositions[i], _targetPositions[i],  _timePassed);
        }
        for (int i = (1 + _step) % 2; i < 8; i += 2)
        {
            legControllers[i].transform.position = Vector3.Lerp(_startPositions[i], _targetPositions[i], _timePassed);
        }
        return _timePassed < 1;
    }

    void RotateToGroundNormal()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity)){
            if(transform.up.y - rayHit.normal.y < 0.005f)
                return;
            transform.rotation = Quaternion.FromToRotation(transform.up, rayHit.normal) * transform.rotation;

	    }
    }

    void Update()
    {
        RotateToGroundNormal();
        qua
        if(!Step())
        {
            _step++;
            _timePassed = 0;
            GetPlacementPositions();
        }
    }
}
