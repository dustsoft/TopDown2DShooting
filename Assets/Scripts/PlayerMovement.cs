using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    public float acceleration;
    //public float playerMoveSpeed = 5;

    Vector2 mousePos;
    Vector2 playerMovement;

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        ApplyAcceleration();
        //ArrowKeysMovement();
        ClampPosition();
    }

    private void FixedUpdate()
    {
        #region Mouse Look
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        #endregion
        //rb.velocity = new Vector2(playerMovement.x * playerMoveSpeed, playerMovement.y * playerMoveSpeed);
    }

    void ApplyAcceleration()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector2 accelForce = transform.up * Time.deltaTime * acceleration * 10;
            rb.AddForce(accelForce, ForceMode2D.Force);
        }

    }

    void ArrowKeysMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        playerMovement = new Vector2(moveX, moveY).normalized;
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
