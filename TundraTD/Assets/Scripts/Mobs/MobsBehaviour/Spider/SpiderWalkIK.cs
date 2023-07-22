using UnityEngine;

public class SpiderWalkIK : MonoBehaviour
{
    [SerializeField] Transform[] legPositions;
    [SerializeField] GameObject[] legControllers;

    private int _step = 0;

    private float _timePassed;

    private Vector3[] _restPositions;

    private Vector3[] _targetPositions;

    private Vector3[] _startPositions;
    
    void Start()
    {
        _restPositions = new Vector3[8];
        for(int i = 0; i < 8; i++)
        {
            _restPositions[i] = legControllers[i].transform.position;
        }
        _targetPositions = new Vector3[8];
        _startPositions = new Vector3[8];
        GetPlacementPositions();
    }
    
    private bool GetPlacementPosition(Vector3 target, out Vector3 postion){
        RaycastHit hit;
        var res = Physics.Raycast(gameObject.transform.position, target - transform.position, out hit, (transform.position - target).magnitude);
        postion = hit.point;
        return res;
    }

    private void GetPlacementPositions(){
        for(int  i = 0; i < 8; i++)
            _startPositions[i] = legPositions[i].position;
        for(int i = _step % 2; i < 8; i += 2)
        {
            if(!GetPlacementPosition(legPositions[i].position, out _targetPositions[i]))
            {
                Debug.Log($"Failed to get position of {i} leg on step");
                _targetPositions[i] = transform.position;
            }
        }
        for(int i = 1 + _step % 2; i < 8; i += 2)
        {
            if(!GetPlacementPosition(_restPositions[i], out _targetPositions[i]))
            {
                Debug.Log($"Failed to get position of {i} leg on recovery");
                _targetPositions[i] = transform.position;
            }
        }
    }

    private bool Step()
    {
        for(int i = 0; i < 8; i++)
        {
            legControllers[i].transform.position = Vector3.Lerp(_startPositions[i], _targetPositions[i],  _timePassed += Time.deltaTime/20);
        }
        print(_timePassed);
        return (legControllers[0].transform.position != _targetPositions[0]);
    }

    void Update()
    {
        // if(Input.anyKeyDown)
        // {
            if(!Step()){
                _step++;
                _timePassed = 0;
                GetPlacementPositions();
                Debug.Log($"{_step} - step");
            }
        //}
    }
}
