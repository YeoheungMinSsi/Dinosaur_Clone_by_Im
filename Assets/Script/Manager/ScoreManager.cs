using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤. 다른 스크립트에서 ScoreManager.Instance로 접근 가능
    public static ScoreManager Instance { get; private set; }

    public Text scoreText; // 점수 표시 UI 텍스트

    private int score = 0; // 현재 점수
    private float timer = 0f; // 시간 누적용 타이머
    private string currentSceneName; // 현재 씬 이름 저장

    void Awake()
    {
        // 싱글톤 : 이미 인스턴스가 있으면 삭제, 없으면 유지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        // GameScene에서만 점수 UI 초기화
        if (currentSceneName == "GameScene")
        {
            FindScoreText(); // ScoreText 오브젝트 찾기
            ResetScore(); // 점수 초기화
        }
    }

    void OnEnable()
    {
        // 씬이 로드될 때마다 OnSceneLoaded 실행되도록 연결
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;

        if (currentSceneName == "GameScene")
        {
            // GameScene이 로드될 때마다 점수 초기화
            FindScoreText();
            ResetScore();
        }
    }

    // ScoreText 오브젝트를 씬에서 찾아서 연결
    void FindScoreText()
    {
        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
                scoreText = scoreObj.GetComponent<Text>();
        }
    }

    // 점수, 타이머 초기화 + UI 갱신
    void ResetScore()
    {
        score = 0;
        timer = 0f;
        UpdateScoreUI();
    }

    void Update()
    {
        // GameScene이고 게임이 일시정지 상태가 아닐 때만 점수 증가
        if (currentSceneName == "GameScene" && Time.timeScale > 0)
        {
            timer += Time.deltaTime; // 경과 시간 누적

            if (timer >= 1f)
            {
                timer = 0f;
                score++; // 1초마다 점수 증가
                UpdateScoreUI(); // UI 갱신
            }
        }
    }

    // UI에 점수 표시
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // 외부에서 현재 점수 가져오기
    public int GetScore()
    {
        return score;
    }
}
