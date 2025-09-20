using UnityEngine;

public class SmokeGranade : MonoBehaviour
{
    [SerializeField] private ParticleSystem _smokeParticles;
    [SerializeField] private float _explodeDelay;
    [SerializeField] private float _disipationDelay;

    private Collider _collider;
    private Rigidbody _rb;

    public void Trhow()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        Invoke("Explode",_explodeDelay);
    }

   private void Explode()
    {
        _smokeParticles.Play();
        _rb.isKinematic = true;
        _collider.isTrigger=true;
        Invoke("StopSmoke", _disipationDelay);
    }

    private void StopSmoke()
    {
        Destroy(gameObject);
    }

}
