using City.Building.ElementPools;
using Mobs;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using Spells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BallLightning : MonoBehaviour
{
    private MobPortal _portal;
    private Vector3 _intialPosition;
    private List<Rigidbody> _mobs = new List<Rigidbody>();
    private float t = 0;
    private LineRenderer lightning;

    [SerializeField] private float timeToLive;
    [SerializeField] private float pullForce;
    
    
    private void Start()
    {
        _intialPosition = transform.position;
        _portal = FindObjectOfType<MobPortal>();
        lightning = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= timeToLive)
            Destroy(this);
        transform.position = Vector3.Lerp(_intialPosition, _portal.transform.position, t / 100);
    }

    private void FixedUpdate()
    {
        _mobs.RemoveAll(body => body == null);
        _mobs.ForEach(body => body.AddForce((transform.position - body.transform.position).normalized * pullForce));
    }

    private void PullObjects(Rigidbody objectToPull)
    {
        
    }

    private void OnTriggerEnter(Collider e)
    {
        Rigidbody enteredObject;
        if (!e.gameObject.TryGetComponent(out enteredObject))
            return;
        _mobs.Add(enteredObject);
    }
    private void OnTriggerExit(Collider other)
    {
        Rigidbody body;
        if(!other.gameObject.TryGetComponent(out body)) 
            return;
        _mobs.Remove(body);
    }
}
