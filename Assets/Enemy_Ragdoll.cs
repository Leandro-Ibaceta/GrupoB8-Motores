using System.Collections;
using UnityEngine;

public class Enemy_Ragdoll : MonoBehaviour
{
    [SerializeField] private float _ragdollMinVelocityToFreeze = 0.1f;
    [SerializeField] private Transform _rigReference;
    private Animator _animator;
    private Rigidbody[] _rigidbodies;
    private bool _isRagdollStill = false;
    private bool _isRagdollActive = false;
    private void Start()
    {
        _animator = GetComponent<Animator>();

        _rigidbodies = _rigReference.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    private void Update()
    {
        if (_isRagdollActive)
        {
           // RagdollFreezer();
        }

    }

    public void ActivateRagdoll()
    {
        _animator.enabled = false;
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = false;
            _isRagdollActive = true;
        }
    }

    void RagdollFreezer()
    {
        _isRagdollStill = true;
        foreach (Rigidbody rb in _rigidbodies)
            {
                if (rb.linearVelocity.magnitude > _ragdollMinVelocityToFreeze)
                {
                    _isRagdollStill = false;
                    break;
                }
            }        
        if(_isRagdollStill)
            FreezeRigidBody();
    }
    
    public void FreezeRigidBody()
    {
        _animator.enabled = false;
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
    }

}
