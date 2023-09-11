using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [HideInInspector]
    public bool GameOver;

    public float FloatForce;
    private static readonly float _gravityModifier = 1.5f;
    private Rigidbody _playerRb;

    public ParticleSystem ExplosionParticle;
    public ParticleSystem FireworksParticle;

    private AudioSource _playerAudio;
    public AudioClip MoneySound;
    public AudioClip ExplodeSound;
    public AudioClip GroundTouchSound;

    private static readonly float _topBound = 8.0f;


    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= _gravityModifier;
        _playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        _playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }


    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !GameOver && transform.position.y < _topBound)
        {
            _playerRb.AddForce(Vector3.up * FloatForce);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            ExplosionParticle.Play();
            _playerAudio.PlayOneShot(ExplodeSound, 1.0f);
            GameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            FireworksParticle.Play();
            _playerAudio.PlayOneShot(MoneySound, 1.0f);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Ground"))
        {
            _playerAudio.PlayOneShot(GroundTouchSound, 1.0f);
            _playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
}
