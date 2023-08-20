using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;
    [SerializeField] float _maxSpawnPointY = 5f;
    [SerializeField] float _minObstacleSpeed = 0.5f;
    [SerializeField] float _maxObstacleSpeed = 3f;
    [SerializeField] int _minObstacleCount = 1;
    [SerializeField] int _maxObstacleCount = 3;
    [SerializeField] float _wavesTimeDelay = 2f;

    [SerializeField] float _minTimeBetweenCoins = 1f;
    [SerializeField] float _maxTimeBetweenCoins = 3f;

    [SerializeField] MovingObstacle _obstaclePrefab;
    [SerializeField] Coin _coinPrefab;
    [SerializeField] TextMeshProUGUI _coinText;

    bool isGameStart = false;
    int _coins = 0;
    Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        UpdateCoinText();
    }

    public void StartGame()
    {
        isGameStart = true;
        StartCoroutine(ObstaclesSpawner());
        StartCoroutine(CoinSpawner());
        _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player.enabled = true;
    }

    IEnumerator ObstaclesSpawner()
    {
        int waveCount = 0;
        while (true)
        {
            //spawn obstacles wave
            int obstacleCount = Random.Range(_minObstacleCount, _maxObstacleCount);
            for (int i = 0; i < obstacleCount; i++)
            {
                SpawnObstacle((float)i);
            }
            yield return new WaitForSeconds(_wavesTimeDelay);

            //increase difficulty
            waveCount++;
            if (waveCount % 5 == 0)
            {
                _maxObstacleCount++;
            }
        }
    }

    IEnumerator CoinSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minTimeBetweenCoins, _maxTimeBetweenCoins));
            float coinY = Random.Range(-_maxSpawnPointY * 0.8f, _maxSpawnPointY * 0.8f);
            Coin coinClone = Instantiate(_coinPrefab, new Vector3(_spawnPoint.position.x, coinY, _spawnPoint.position.z), Quaternion.identity);
        }
    }

    public void GameOver()
    {
        Invoke("RestartScene", 1f);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    void SpawnObstacle(float shiftX)
    {
        float obstacleY1 = Random.Range(-_maxSpawnPointY, _maxSpawnPointY);
        float obstacleY2 = Random.Range(-_maxSpawnPointY, _maxSpawnPointY);
        float speed = Random.Range(_minObstacleSpeed, _maxObstacleSpeed);
        Vector3 spawnPosition = _spawnPoint.position;
        spawnPosition.x += shiftX;
        MovingObstacle obstacleClone = Instantiate(_obstaclePrefab, spawnPosition, Quaternion.identity);
        obstacleClone.Init(Mathf.Min(obstacleY1, obstacleY2), Mathf.Max(obstacleY1, obstacleY2), speed);
    }

    public void CollectCoin()
    {
        _coins++;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        _coinText.text = _coins.ToString();
    }
}
