using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    public GameManager GameManager;

    private Camera _camera;
    private TrailRenderer _trail;
    private BoxCollider _collider;

    private bool _swiping = false;


    private void Awake()
    {
        _camera = Camera.main;

        _trail = GetComponent<TrailRenderer>();
        _trail.enabled = false;

        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
    }


    private void UpdateMousePosition()
    {
        var mousePos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }


    private void UpdateComponents()
    {
        _trail.enabled    =
        _collider.enabled = _swiping;
    }


    void Update()
    {
        if(GameManager.isGameActive)
        {
            if(Input.GetMouseButtonDown(0))
            {
                _swiping = true;
                UpdateComponents();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                _swiping = false;
                UpdateComponents();
            }

            if(_swiping)
            {
                UpdateMousePosition();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();

        if(target)
        {
            target.DestroyTarget();
        }
    }
}
