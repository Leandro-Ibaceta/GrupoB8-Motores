using UnityEngine;

public class SecurityGuard : Enemy
{

    [SerializeField] private Animator _animator;
    [SerializeField] private float _animationChangeFactor;

    private Enemy_agent _agent;
    private Rigidbody _rb;
    private float _animationBlend;
   
    private void Start()
    {
        _agent = GetComponent<Enemy_agent>();
        _rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        if (_agent.ActualState == Enemy_agent.ENEMY_STATE.ATTACKING)
        {
            _animator.speed = 1;
            _animationBlend += _animationChangeFactor * Time.deltaTime;
            _animator.SetFloat("Walk_Run", _animationBlend);
        }
        else
        {
            _animator.speed = _agent.Agent.velocity.magnitude / _agent.Agent.speed;
            _animationBlend = 0;
        }
    }
    public override void Attack()
    {
        _animator.SetTrigger("attack");
    }

    public override void Disable()
    {
        _agent.Agent.enabled = false;
        _agent.enabled = false;
        _rb.excludeLayers = LayerMask.NameToLayer("Everything");
    }
}
