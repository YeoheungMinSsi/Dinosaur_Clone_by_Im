using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤

    [Header("Dinosaur Prefabs")]
    public GameObject triceratopsPrefab;
    public GameObject tyrannosaurusPrefab;
    public GameObject brachiosaurusPrefab;

    private bool isGameOver = false; // 게임 오버 상태
    private GameObject currentPlayer; // 현재 생성된 플레이어 객체
    private string currentSceneName; // 현재 씬 이름 저장용

    // 게임 매니저 싱글톤 초기화
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 삭제되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    // 게임 시작 시 호출
    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        // 게임씬일 경우에만 캐릭터 생성
        if (currentSceneName == "GameScene")
        {
            SpawnSelectedDino();
        }
    }

    // 씬 로드 이벤트에 함수 등록
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 씬 로드 이벤트에서 함수 제거
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때마다 호출되는 함수
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;

        if (currentSceneName == "GameScene")
        {
            // 기존 플레이어가 있다면 제거(재시작 대비)
            if (currentPlayer != null)
            {
                DestroyImmediate(currentPlayer);
                currentPlayer = null;
            }

            // 게임 오버 상태 초기화
            isGameOver = false;
            Time.timeScale = 1f; // 일시정지 해제

            // 선택된 캐릭터 생성
            SpawnSelectedDino();
        }
    }

    // 프리팹에서 선택된 캐릭터 불러와 생성
    void SpawnSelectedDino()
    {
        string selectedDino = PlayerPrefs.GetString("SelectedDino", "Triceratops");
        Vector3 spawnPosition = new Vector3(0, -4.3f, 0); // 고정된 스폰 위치

        GameObject player = null;

        // 선택된 캐릭터에 따라 프리팹 생성
        switch (selectedDino)
        {
            case "Triceratops":
                player = Instantiate(triceratopsPrefab, spawnPosition, Quaternion.identity);
                break;
            case "Tyrannosaurus":
                player = Instantiate(tyrannosaurusPrefab, spawnPosition, Quaternion.identity);
                break;
            case "Brachiosaurus":
                player = Instantiate(brachiosaurusPrefab, spawnPosition, Quaternion.identity);
                break;
            default:
                Debug.LogWarning("선택된 공룡 없음, 기본 트리케라톱스 생성");
                player = Instantiate(triceratopsPrefab, spawnPosition, Quaternion.identity);
                break;
        }

        // 현재 플레이어 저장
        currentPlayer = player;

        // PlayerBase 시스템 연결
        if (player != null)
        {
            PlayerBase playerBase = player.GetComponent<PlayerBase>();
            if (playerBase != null)
            {
                // UI 연결
                playerBase.gameOverUI = GameObject.Find("GameOverPanel");

                GameObject finalScoreObj = GameObject.Find("FinalScoreText");
                if (finalScoreObj != null)
                    playerBase.finalScoreText = finalScoreObj.GetComponent<TMPro.TextMeshProUGUI>();

                playerBase.healthUI = FindObjectOfType<HealthUI>();
            }

            // PlayerHealth 사용 시에도 UI 연결(객체지향 적용 전에 쓰던거)
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.gameOverUI = GameObject.Find("GameOverPanel");
                var finalScoreObj = GameObject.Find("FinalScoreText");
                if (finalScoreObj != null)
                    ph.finalScoreText = finalScoreObj.GetComponent<TMPro.TextMeshProUGUI>();
            }
        }
    }

    // 게임 오버 처리 함수
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("게임 오버!");
        Time.timeScale = 0f; // 일시정지

        // 게임 오버 UI 활성화
        GameObject gameOverUI = GameObject.Find("GameOverPanel");
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    // 현재 플레이어 반환
    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }
}
