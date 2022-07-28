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
        this.explosion.Play();

        //TODO: increase score
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
        //Game Is Over
    }
}
