using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            FindObjectOfType<GameController>().StartGame();
            this.gameObject.SetActive(false);
        }
    }
}
