using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject startScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI finalScoreText;
    public Button startButton;
    public Button restartButton;

    public PulpitManager pulpitManager;
    public DoofusController doofusController;
    public ScoreManager scoreManager;

    private void Awake()
    {


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        ShowStartScreen();
    }

    public void ShowStartScreen()
    {
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        Time.timeScale = 0; // Pause the game
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        Time.timeScale = 1; // Resume the game
        pulpitManager.StartGame();
        doofusController.DoofusStartGame();
        scoreManager.ResetScore();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        finalScoreText.text = "Final Score: " + scoreManager.GetScore();
        Time.timeScale = 0; // Pause the game
    }

    public void RestartGame()
    {
        StartGame();
    }
}