using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager GameManager;
    public ParticleSystem ExplosionParticle;
    public ParticleSystem DirtParticle;

    public float JumpForce = 0.0f;

    private Rigidbody _rigidbody;

    private bool IsOnGround {
        get { return _jumpCount == 0; }
    }
    private int _jumpCount = 0;

    private Animator _animator;

    private bool _startWalk = true;
    private static readonly float _walkSpeed = 3.0f;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Speed_f", 0.4f);
        DirtParticle.Stop();
        Invoke(nameof(StartRun), Config.StartupDelay);
    }


    void Update()
    {
        if (GameManager.GameOver)
        {
            return;
        }

        if (_startWalk)
        {
            float deltaX = Time.deltaTime * _walkSpeed;
            transform.position = new Vector3(transform.position.x + deltaX,
                                             transform.position.y,
                                             transform.position.z);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_jumpCount < 2)
            {
                _jumpCount++;
                _rigidbody.AddForce( Vector3.up * JumpForce * (_jumpCount == 2 ? 0.5f : 1.0f), ForceMode.Impulse);
                _animator.SetTrigger("Jump_trig");
                DirtParticle.Stop();
            }
        }
    }


    private void StartRun()
    {
        _startWalk = false;
        _animator.SetFloat("Speed_f", 1.0f);
        DirtParticle.Play();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {           
            _jumpCount = 0;

            if (!_startWalk && GameManager.GameOver == false)
            {
                DirtParticle.Play();
            }
        }
        else if (collision.gameObject.GetComponent<Obstacle>())
        {
            if (GameManager.GameOver == false)
            {
                _animator.SetBool("Death_b", true);
                _animator.SetInteger("DeathType_int", 1);
                ExplosionParticle.Play();
                DirtParticle.Stop();
                GameManager.GameOver = true;
                Debug.Log("Game Over!");
            }
        }
    }
}
