using UnityEngine;

public class PowerCoreDestructible : Destructible
{
    protected BoxCollider _collider;
    public float destroyDelay = 1.0f;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    protected override void OnDetonate()
    {
        base.OnDetonate();

        _collider.enabled = false;

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * this.detonationForce);
        }

        Destroy(this.gameObject, this.destroyDelay);
    }

}
