using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Player _player;

    Vector3 shift;

    private void Start()
    {
        shift = transform.position - _player.transform.position;
    }

    void FollowPlayer()
    {
        transform.position = new Vector3(_player.transform.position.x + shift.x, transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        if (_player != null)
        {
            FollowPlayer();
        }
    }
}
