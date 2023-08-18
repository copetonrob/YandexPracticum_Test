using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : Obstacle
{
    float _speed;
    float _minPosY;
    float _maxPosY;

    bool isUp = false;

    public void Init(float minPosY, float maxPosY, float speed)
    {
        _minPosY = minPosY;
        _maxPosY = maxPosY;
        if (_maxPosY - _minPosY < 1f )
        {
            _maxPosY = 2 * Mathf.Abs(_maxPosY);
        }
        _speed = speed;
        transform.position = new Vector3(transform.position.x, Random.Range(_minPosY, _maxPosY), transform.position.z);
        isUp = Random.Range(0, 2) == 0;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        //moving between two points
        if (isUp)
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * _speed * Time.deltaTime;
        }

        if (transform.position.y > _maxPosY)
        {
            isUp = false;
        }
        else if (transform.position.y < _minPosY)
        {
            isUp = true;
        }
    }
}
