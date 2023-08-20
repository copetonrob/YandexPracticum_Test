using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject _collectEffect;
    [SerializeField] float _attaractionRadius = 2f;
    [SerializeField] float _attractionSpeed = 1f;

    Player _player;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            FindObjectOfType<GameController>().CollectCoin();
            Instantiate(_collectEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_player == null)
            return;

        if (Vector2.Distance(transform.position, _player.transform.position) < _attaractionRadius)
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _attractionSpeed * Time.deltaTime);
    }
}
