using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public ParticleSystem explosion;
    public int playerLives = 3;
    public float respawnTime = 3.0f;
    public int score = 0;


    public void PlayerDied()
    {
        this.playerLives--;
        this.explosion.transform.position = this.player.transform.position;
        CMShake.Instance.CameraShake(5.5f, 0.75f);
        this.explosion.Play();

        if (this.playerLives < 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }

    }

    public void AsteroidDestoryed(AsteroidController asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        CMShake.Instance.CameraShake(2f, 0.25f);
        this.explosion.Play();

        #region Score Info
        if (asteroid._size < 0.55f)
        {
            this.score += 100;
        }
        else if (asteroid._size < 1.0f)
        {
            this.score += 50;
        }
        else
        {
            this.score += 25;
        }

        #endregion
    }

    void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), 3.0f);
    }

    void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void GameOver()
    {
        this.playerLives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), respawnTime);
    }
}
