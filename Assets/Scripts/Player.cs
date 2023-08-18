using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] GameObject _deathEffect;

    Rigidbody2D _rigidbody;
    GameController _gameController;
    bool isUp = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        //Player Input
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            isUp = true;
        }
        else
        {
            isUp = false;
        }
    }

    private void FixedUpdate()
    {
        //Physics Update
        if (isUp)
        {
            _rigidbody.AddForce(Vector2.up, ForceMode2D.Impulse);
        }
        Vector3 currentVelocity = _rigidbody.velocity;
        currentVelocity.x = _speed;
        _rigidbody.velocity = currentVelocity;
    }

    public void Die()
    {
        _gameController.GameOver();
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
