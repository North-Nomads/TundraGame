using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticCharge : MonoBehaviour
{
    private List<MobBehaviour> _mobs = new List<MobBehaviour>();
    private float t = 0;

    [SerializeField] private int directDamage;
    [SerializeField] private int coolDownTime;

    private void Start()
    {
        
    }


    private void Update()
    {
        t += Time.deltaTime;
        if(Convert.ToInt32(t % coolDownTime) == 0)
        {
            _mobs.ForEach(mob => HitMob(mob));
        }
    }

    private void HitMob(MobBehaviour mobToStrike)
    {
        if(mobToStrike == null)
        {
            _mobs.Remove(mobToStrike);
            return;
        }

            mobToStrike.HitThisMob(directDamage, BasicElement.Lightning, "BallLightning");
    }

    private void OnTriggerEnter(Collider e)
    {
        MobBehaviour enteredMob;
        if (!e.gameObject.TryGetComponent(out enteredMob))
            return;
        _mobs.Add(enteredMob);
    }
}
