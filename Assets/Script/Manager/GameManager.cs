using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �̱���

    [Header("Dinosaur Prefabs")]
    public GameObject triceratopsPrefab;
    public GameObject tyrannosaurusPrefab;
    public GameObject brachiosaurusPrefab;

    private bool isGameOver = false; // ���� ���� ����
    private GameObject currentPlayer; // ���� ������ �÷��̾� ��ü
    private string currentSceneName; // ���� �� �̸� �����

    // ���� �Ŵ��� �̱��� �ʱ�ȭ
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �������� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }
    }

    // ���� ���� �� ȣ��
    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        // ���Ӿ��� ��쿡�� ĳ���� ����
        if (currentSceneName == "GameScene")
        {
            SpawnSelectedDino();
        }
    }

    // �� �ε� �̺�Ʈ�� �Լ� ���
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // �� �ε� �̺�Ʈ���� �Լ� ����
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�� ������ ȣ��Ǵ� �Լ�
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;

        if (currentSceneName == "GameScene")
        {
            // ���� �÷��̾ �ִٸ� ����(����� ���)
            if (currentPlayer != null)
            {
                DestroyImmediate(currentPlayer);
                currentPlayer = null;
            }

            // ���� ���� ���� �ʱ�ȭ
            isGameOver = false;
            Time.timeScale = 1f; // �Ͻ����� ����

            // ���õ� ĳ���� ����
            SpawnSelectedDino();
        }
    }

    // �����տ��� ���õ� ĳ���� �ҷ��� ����
    void SpawnSelectedDino()
    {
        string selectedDino = PlayerPrefs.GetString("SelectedDino", "Triceratops");
        Vector3 spawnPosition = new Vector3(0, -4.3f, 0); // ������ ���� ��ġ

        GameObject player = null;

        // ���õ� ĳ���Ϳ� ���� ������ ����
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
                Debug.LogWarning("���õ� ���� ����, �⺻ Ʈ���ɶ��齺 ����");
                player = Instantiate(triceratopsPrefab, spawnPosition, Quaternion.identity);
                break;
        }

        // ���� �÷��̾� ����
        currentPlayer = player;

        // PlayerBase �ý��� ����
        if (player != null)
        {
            PlayerBase playerBase = player.GetComponent<PlayerBase>();
            if (playerBase != null)
            {
                // UI ����
                playerBase.gameOverUI = GameObject.Find("GameOverPanel");

                GameObject finalScoreObj = GameObject.Find("FinalScoreText");
                if (finalScoreObj != null)
                    playerBase.finalScoreText = finalScoreObj.GetComponent<TMPro.TextMeshProUGUI>();

                playerBase.healthUI = FindObjectOfType<HealthUI>();
            }

            // PlayerHealth ��� �ÿ��� UI ����(��ü���� ���� ���� ������)
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

    // ���� ���� ó�� �Լ�
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("���� ����!");
        Time.timeScale = 0f; // �Ͻ�����

        // ���� ���� UI Ȱ��ȭ
        GameObject gameOverUI = GameObject.Find("GameOverPanel");
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    // ���� �÷��̾� ��ȯ
    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }
}
