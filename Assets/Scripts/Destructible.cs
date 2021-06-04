using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject detonationEffect;
    public float detonationForce;
    public float detonationUpwardsModifier;
    public float detonationRadius;
    public float detonationDelay;
    public bool destroyOnDetonate;

    public void DetonateImmediately()
    {
        OnDetonate();
    }

    public void Detonate()
    {
        Detonate(this.detonationDelay);
    }

    public void Detonate(float delay)
    {
        Invoke(nameof(OnDetonate), delay);
    }

    protected virtual void OnDetonate()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, this.detonationRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.attachedRigidbody != null) {
                collider.attachedRigidbody.AddExplosionForce(this.detonationForce, this.transform.position, this.detonationRadius, this.detonationUpwardsModifier);
            }
        }

        Instantiate(this.detonationEffect, this.transform.position, this.transform.rotation);

        if (this.destroyOnDetonate) {
            Destroy(this.gameObject);
        }
    }

}
