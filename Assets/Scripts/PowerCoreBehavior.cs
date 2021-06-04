using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCoreBehavior : Destructible
{
    protected BoxCollider _collider;
    public GameObject core;
    public float destroyDelay;
    

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    protected override void OnDetonate()
    {
        base.OnDetonate();

        _collider.enabled = false;

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
     {
        if(collision.collider.tag == "Grenade")
        {
            Detonate();
            Destroy(collision.gameObject);
            Destroy(this.core);
            Destroy(this.gameObject, this.destroyDelay);
        }
    }    
}
