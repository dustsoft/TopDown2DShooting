using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Transform _target;

    Rigidbody2D _rigidBody;

    public float _size = 1.0f;
    public float _minSize = 0.5f;
    public float _maxSize = 1.5f;
    public float _speed = 50.0f;
    public float _maxLifeTime = 30.0f;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        this.transform.localScale = Vector3.one * this._size;
        _rigidBody.mass = this._size;
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            if ((this._size * 0.5f) > this._minSize)
            {
                CreateSplit();
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestoryed(this);

            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        AsteroidController half = Instantiate(this, position, this.transform.rotation);
        half._size = this._size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * this._speed * 1.15f);
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidBody.AddForce(direction * this._speed);
        Destroy(this.gameObject, this._maxLifeTime);
    }
}
