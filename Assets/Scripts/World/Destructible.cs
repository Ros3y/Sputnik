using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float detonationForce;
    public float detonationRadius;

    public GameObject detonationEffect;
    public GameObject _detonationPosition;
    public bool onDetonateDestroy;
    public bool hasDetonated { get; private set; }
    public GameObject player;
    private RigidbodyMovement _playerInfo;
    

        private void Awake()
        {
            this.hasDetonated = false;
            _playerInfo = player.GetComponent<RigidbodyMovement>();
        }
        public void Detonate()
        {
            OnDetonate();
        }

        public void Detonate(float delay)
        {
            Invoke(nameof(OnDetonate), delay);           
        }

    protected virtual void OnDetonate()
    {
        Instantiate(detonationEffect, _detonationPosition.transform.position, this.transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, detonationRadius);
    
        foreach(Collider nearByObject in colliders)
        {
            Rigidbody nearByRigidbody = nearByObject.GetComponent<Rigidbody>();
            if(nearByRigidbody != null)
            {
               if(nearByObject.tag == "Player" && GlobalControl.Instance.playerIsGrounded == true)
               {
                    nearByRigidbody.AddExplosionForce(this.detonationForce * 1.25f, _detonationPosition.transform.position, detonationRadius, 2.0f, ForceMode.Impulse); 
               }
               else
               {
                    nearByRigidbody.AddExplosionForce(this.detonationForce, _detonationPosition.transform.position, detonationRadius, 3.0f, ForceMode.Impulse); 
               }
            }   
        }

        this.hasDetonated = true;

        if(this.onDetonateDestroy)
        {
            Destroy(this.gameObject);
        }
    }

}
