using Mobs;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using Spells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BallLightning : MonoBehaviour
{
    private float t = 0;

    [SerializeField] private float timeToDetonate;
    [SerializeField] private float detonationRadius;
    [SerializeField] private float maxDamage;
    [SerializeField] private float minDamage;

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= timeToDetonate)
            Detonate();
    }
    private void Detonate()
    {
        Physics.OverlapSphere(transform.position, detonationRadius).ToList().ForEach(coll => 
        { 
            if(coll.gameObject.TryGetComponent(out MobBehaviour mob)) 
            mob.HitThisMob(Mathf.Lerp(minDamage, maxDamage, t/timeToDetonate), BasicElement.Lightning, "Ball lightning"); 
        });
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Detonate();
    }

    public event EventHandler Detonated;
}
