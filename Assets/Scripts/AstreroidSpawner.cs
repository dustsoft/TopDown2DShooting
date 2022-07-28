using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstreroidSpawner : MonoBehaviour
{
    public AsteroidController _asteroidPrefab;
    public float _trajectoryVariance = 15.0f;
    public float _spawnRate = 2.0f;
    public int _spawnAmount = 1;
    public float _spawnDistance = 15.0f;

    [SerializeField] private GameObject _asteroidContainer;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), this._spawnRate, this._spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        for (int i = 0; i < this._spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this._spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this._trajectoryVariance, this._trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            AsteroidController asteroid = Instantiate(this._asteroidPrefab, spawnPoint, rotation);
            asteroid.transform.parent = _asteroidContainer.transform;
            asteroid._size = Random.Range(asteroid._minSize, asteroid._maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
