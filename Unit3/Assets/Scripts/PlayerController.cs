using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpForce = 0.0f;
    public bool IsOnGround { get { return _isOnGround; } }

    private Rigidbody _rigidbody;
    private bool _isOnGround = true;
    private Animator _animator;



    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Speed_f", 0.4f);
        Invoke(nameof(StartRun), Config.StartupDelay);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _isOnGround = false;
        }
    }


    private void StartRun()
    {
        _animator.SetFloat("Speed_f", 1.0f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        _isOnGround = true;
    }
}
