using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;

    private Player player;
    private Spawner spawner;

    private float score;
    public float Score => score;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        // Kết nối các sự kiện nút Start và Retry
        startButton.onClick.AddListener(StartGame);
        retryButton.onClick.AddListener(NewGame);

        // Ẩn các thành phần khi game chưa bắt đầu
        startButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    // Phương thức StartGame gọi khi người chơi nhấn nút Start
    public void StartGame()
    {
        // Ẩn menu start
        startButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        // Bắt đầu game
        NewGame();
    }

    public void NewGame()
    {
        // Xóa các chướng ngại vật cũ trong scene
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // Reset các giá trị khi bắt đầu game
        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        // Kích hoạt Player và Spawner và điểm
        scoreText.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);

        // Ẩn game over và retry
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        UpdateHiscore();
    }

    public void GameOver()
    {
        // Dừng game khi game over
        gameSpeed = 0f;
        enabled = false;

        // Ẩn Player, Spawner và hiện game over
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false); // Ẩn nút Start khi game over

        // Hiển thị game over và retry
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

        

        UpdateHiscore();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        continueButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        continueButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Cập nhật game speed và điểm số
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHiscore()
    {
        // Lưu lại điểm cao nhất (hiscore)
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
