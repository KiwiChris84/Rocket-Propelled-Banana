using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosionEffect;
    public GameObject gameOverUI;

    public int score { get; private set; }
    public Text scoreText;

    public int lives { get; private set; }
    public Text livesText;
    public int destroyed;
    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return)) {
            NewGame();
        }
    }

    public void NewGame()
    {
        MissileJellyfish[] MissileJellyfishs = FindObjectsOfType<MissileJellyfish>();

        for (int i = 0; i < MissileJellyfishs.Length; i++) {
            Destroy(MissileJellyfishs[i].gameObject);
        }

        gameOverUI.SetActive(false);

        SetScore(0);
        SetLives(1);
        Respawn();
    }

    public void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    public void MissileJellyfishDestroyed(MissileJellyfish MissileJellyfish)
    {
        explosionEffect.transform.position = MissileJellyfish.transform.position;
        explosionEffect.Play();

        if (MissileJellyfish.size < 0.7f) {
            SetScore(score + 100); // small MissileJellyfish
            destroyed = destroyed + 1;
        } else if (MissileJellyfish.size < 1.4f) {
            SetScore(score + 50); // medium MissileJellyfish
            destroyed = destroyed + 1;
        } else {
            SetScore(score + 25); // large MissileJellyfish
            destroyed = destroyed + 1;
        }
    }

    public void PlayerDeath(Player player)
    {
        explosionEffect.transform.position = player.transform.position;
        explosionEffect.Play();

        SetLives(lives - 1);

        if (lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), player.respawnDelay);
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

}
