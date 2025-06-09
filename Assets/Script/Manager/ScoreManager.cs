using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // �̱���. �ٸ� ��ũ��Ʈ���� ScoreManager.Instance�� ���� ����
    public static ScoreManager Instance { get; private set; }

    public Text scoreText; // ���� ǥ�� UI �ؽ�Ʈ

    private int score = 0; // ���� ����
    private float timer = 0f; // �ð� ������ Ÿ�̸�
    private string currentSceneName; // ���� �� �̸� ����

    void Awake()
    {
        // �̱��� : �̹� �ν��Ͻ��� ������ ����, ������ ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        // GameScene������ ���� UI �ʱ�ȭ
        if (currentSceneName == "GameScene")
        {
            FindScoreText(); // ScoreText ������Ʈ ã��
            ResetScore(); // ���� �ʱ�ȭ
        }
    }

    void OnEnable()
    {
        // ���� �ε�� ������ OnSceneLoaded ����ǵ��� ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;

        if (currentSceneName == "GameScene")
        {
            // GameScene�� �ε�� ������ ���� �ʱ�ȭ
            FindScoreText();
            ResetScore();
        }
    }

    // ScoreText ������Ʈ�� ������ ã�Ƽ� ����
    void FindScoreText()
    {
        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
                scoreText = scoreObj.GetComponent<Text>();
        }
    }

    // ����, Ÿ�̸� �ʱ�ȭ + UI ����
    void ResetScore()
    {
        score = 0;
        timer = 0f;
        UpdateScoreUI();
    }

    void Update()
    {
        // GameScene�̰� ������ �Ͻ����� ���°� �ƴ� ���� ���� ����
        if (currentSceneName == "GameScene" && Time.timeScale > 0)
        {
            timer += Time.deltaTime; // ��� �ð� ����

            if (timer >= 1f)
            {
                timer = 0f;
                score++; // 1�ʸ��� ���� ����
                UpdateScoreUI(); // UI ����
            }
        }
    }

    // UI�� ���� ǥ��
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // �ܺο��� ���� ���� ��������
    public int GetScore()
    {
        return score;
    }
}
