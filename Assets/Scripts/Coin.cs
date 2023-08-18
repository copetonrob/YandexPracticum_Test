using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject _collectEffect;

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
}
