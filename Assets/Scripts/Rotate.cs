using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _rotationSpeed = 5f;

    void Update()
    {
        transform.Rotate(_rotation * _rotationSpeed * Time.deltaTime);
    }
}
