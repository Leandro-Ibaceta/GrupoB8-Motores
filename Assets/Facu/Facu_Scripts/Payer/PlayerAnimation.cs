using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _speedParameterName;
    [SerializeField] private string _crouchParameterName;
    [SerializeField] private string _crawlParameterName;
    [SerializeField] private string _attackTriggerName;
    [SerializeField] private string _granadeTriggerName;

    [Header("Audio")]
    [SerializeField] private AudioSource _shootAudioSource;
    [SerializeField] private AudioSource _grenadeAudioSource;

    private PlayerManager _playerManager;

    private bool _locked;

    private void Start()
    {
        _playerManager = GameManager.instance.PlayerManager;
    }

    public void SetLocked(bool locked)
    {
        _locked = locked;

        if (locked)
        {
            
            _animator.SetFloat(_speedParameterName, 0f);
            _animator.SetBool(_crouchParameterName, false);
            _animator.SetBool(_crawlParameterName, false);
            _animator.ResetTrigger(_attackTriggerName);
            _animator.ResetTrigger(_granadeTriggerName);
            
            _animator.applyRootMotion = false;
        }
        else
        {
            _animator.applyRootMotion = true;
        }
    }
    public void ChangePlayerSpeed(float newValue)
    {
        if (_locked) return;
        _animator.SetFloat(_speedParameterName,Mathf.Clamp(newValue,0,2));
    }


    public void ChangeStanceValue(int stanceStep)
    {
        if (_locked) return;

        switch (stanceStep)
        {
            case 0:
                _animator.SetBool(_crouchParameterName, false);
                _animator.SetBool(_crawlParameterName, false);
                break;
            case 1:
                _animator.SetBool(_crouchParameterName, true);
                _animator.SetBool(_crawlParameterName, false);
                break;
            case 2:
                _animator.SetBool(_crawlParameterName, true);
                break;

        }
    }

    public void ChangeAnimationSpeed(float newValue)
    {
        newValue = Mathf.Clamp(newValue, 0f, 10);
        _animator.speed = newValue;

    }

    public void InvertAnimation()
    {
        _animator.speed *= -1;
    }
    public void SetAttackTrigger()
    {
        ChangeStanceValue(0);
        _animator.SetTrigger(_attackTriggerName);
    }

    public void BeginShooting()
{


    if (_shootAudioSource != null)
    {

        _shootAudioSource.pitch = Random.Range(0.95f, 1.05f);
        _shootAudioSource.PlayOneShot(_shootAudioSource.clip);
    }
    

    _playerManager.Attack.Attack();
}
    public void EndedShooting()
    {
        _playerManager.Movement.enabled = true;
        _playerManager.Attack.IsAttacking = false;
        _playerManager.GFX.transform.Rotate(0,90,0);
        _playerManager.GunGFX.SetActive(false);
    }
   public void SetGranadeTrigger()
{
    _animator.SetTrigger(_granadeTriggerName);

    if (_grenadeAudioSource != null && _grenadeAudioSource.clip != null)
    {
        _grenadeAudioSource.pitch = Random.Range(0.95f, 1.05f);
        _grenadeAudioSource.PlayOneShot(_grenadeAudioSource.clip);
    }
}

    public void BeginStandingUp()
    {
        _playerManager.Movement.enabled = false;
        _animator.speed = 2;
    }

    public void EndedStandedUp()
    {
        _playerManager.Movement.enabled = true;
        _animator.speed = 1;
    } 


}
