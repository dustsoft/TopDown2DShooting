using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Info")]
    [SerializeField] GameObject _uiCanvas;

    [Header("Player Info")]
    public PlayerMovement player;
    public int playerLives = 3;
    public Text livesValue;
    public float respawnTime = 3.0f;

    [Header("Score Info")]
    public int score = 0;
    public Text scoreValue;
    public int highScore;
    public Text highScoreValue;

    [Header("Particles")]
    public ParticleSystem explosion;

    [Header("Asteroid Spawner Info")]
    public GameObject astreroidSpawner;

    void Update()
    {
        ScoreDisplay();
        LivesDisplay();
    }

    public void PlayerDied()
    {
        this.playerLives--;
        this.explosion.transform.position = this.player.transform.position;
        CMShake.Instance.CameraShake(5.5f, 0.75f);
        this.explosion.Play();

        if (this.playerLives < 1)
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
        SaveValues();
        LoadValues();

        _uiCanvas.SetActive(true);

        highScoreValue.text = highScore.ToString("#,#");

        GameObject[] killEmAll;
        killEmAll = GameObject.FindGameObjectsWithTag("Asteroid");
        for (int i = 0; i < killEmAll.Length; i++)
        {
            Destroy(killEmAll[i].gameObject);
        }

        FindObjectOfType<AstreroidSpawner>().DeactivateSpawn();
        astreroidSpawner.SetActive(false);
    }

    public void PlayAgain()
    {
        astreroidSpawner.SetActive(true);
        FindObjectOfType<AstreroidSpawner>().ActivateSpawn();

        GameObject[] killEmAll;
        killEmAll = GameObject.FindGameObjectsWithTag("Asteroid");
        for (int i = 0; i < killEmAll.Length; i++)
        {
            Destroy(killEmAll[i].gameObject);
        }

        _uiCanvas.SetActive(false);
        playerLives = 3;
        score = 0;
        Respawn();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ScoreDisplay()
    {
        scoreValue.text = score.ToString("#,#");
    }

    void LivesDisplay()
    {
        livesValue.text = playerLives.ToString();
    }

    void SaveValues()
    {

        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
        }

    }

    void LoadValues()
    {
        highScore = PlayerPrefs.GetInt("highScore");
    }

}