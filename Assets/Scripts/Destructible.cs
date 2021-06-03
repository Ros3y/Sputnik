using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private BoxCollider _collider;
    public GameObject powerCoreDestructible;
    public GameObject grenade;
    public float detonationForce;
    public float detonationRadius;
    public GameObject detonationEffect;
    private GameObject _detonationPosition;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
        _detonationPosition = this.transform.GetChild(0).gameObject;
    }

     private void OnCollisionEnter(Collision collision)
     {
        if(collision.collider.tag == "Grenade")
        {
            Instantiate(this.powerCoreDestructible, this.transform.position, this.transform.rotation);
            Invoke(nameof(detonate), 0.0f);
            Destroy(this.gameObject);
        }
     }

      private void detonate()
    {
        Instantiate(detonationEffect, _detonationPosition.transform.position, this.transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, detonationRadius);
    
        foreach(Collider nearByObject in colliders)
        {
            Rigidbody nearByRigidbody = nearByObject.GetComponent<Rigidbody>();
            if(nearByRigidbody != null)
            {
               nearByRigidbody.AddExplosionForce(detonationForce, _detonationPosition.transform.position, detonationRadius); 
            }    
        }
    }
}
