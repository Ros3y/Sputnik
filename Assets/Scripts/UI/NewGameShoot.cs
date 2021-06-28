using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameShoot : MonoBehaviour
{
    public GameObject grenadePrefab;
    public float power;
    public ParticleSystem muzzleFlash;     


    public void ShootGrenade()
    {
        GameObject grenade = Instantiate(this.grenadePrefab, this.transform.position, this.transform.rotation);
        Rigidbody _rigidbodyGrenade = grenade.GetComponent<Rigidbody>();

        ForceMode mode = ForceMode.Impulse;
        _rigidbodyGrenade.AddForce( this.transform.forward * this.power, mode);

        Instantiate(muzzleFlash, this.transform.position, this.transform.rotation);

    }
}
