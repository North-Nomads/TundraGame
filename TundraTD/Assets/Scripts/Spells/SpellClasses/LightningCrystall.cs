using Mobs.MobsBehaviour;
using Spells;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightningCrystall : MonoBehaviour
{
    private List<Rigidbody> _mobs = new List<Rigidbody>();
    private float t = 0;

    [SerializeField] float pullForce;
    [SerializeField] float timeToLive;

    private void OnTriggerEnter(Collider e)
    {
        if (!e.gameObject.TryGetComponent(out Rigidbody enteredMob))
            return;
        _mobs.Add(enteredMob);
    }

    private void OnTriggerExit(Collider e)
    {
        if (!e.gameObject.TryGetComponent(out Rigidbody enteredMob))
            return;
        _mobs.Remove(enteredMob);
    }

    private void FixedUpdate()
    {
        _mobs.Where(mob => !mob.gameObject.activeSelf).ToList().ForEach(mob => _mobs.Remove(mob));

        _mobs.ForEach(mob => mob.AddForce(new Vector3
            (mob.transform.position.x - transform.position.x, mob.transform.position.y - transform.position.y, mob.transform.position.z - transform.position.z).normalized * pullForce * -1, ForceMode.Force));
        
    }

    private void Start()
    {
        Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius).ToList().ForEach(coll =>
        {
            if (coll.gameObject.TryGetComponent(out Rigidbody mob))
                _mobs.Add(mob);
        });
    }

    private void Update()
    {
        t += Time.deltaTime;

        if (t > timeToLive)
            Destroy(gameObject);
    }
}
