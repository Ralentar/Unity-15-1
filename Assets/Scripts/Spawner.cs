using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _spawnTime = 5;
    [SerializeField] private SpawnPoint[] _spawnPoints;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_spawnTime);
    }

    private void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
            Spawn(_spawnPoints[i].transform.position);
    }

    private void CoinCollected(Coin coin)
    {
        coin.Collected -= CoinCollected;
        StartCoroutine(SpawnCoin(coin.transform.position));
    }

    private IEnumerator SpawnCoin(Vector2 position)
    {
        yield return _wait;

        Spawn(position);
    }

    private void Spawn(Vector2 position)
    {
        Coin coin = Instantiate(_coinPrefab, position, Quaternion.identity);
        coin.Collected += CoinCollected;
    }
}