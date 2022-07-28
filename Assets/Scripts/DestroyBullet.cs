using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y > 8f || transform.position.y < -8f || transform.position.x > 10f || transform.position.x < -10f)
        {
            Destroy(this.gameObject);
        }
    }
}
