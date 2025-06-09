using UnityEngine;
using TMPro;

//��� ���ư����ϴµ� ����ϱ� �������� �ϴ� ����� �ڵ�

public class PlayerHealth : MonoBehaviour
{
    public int maxHits = 5; // �ִ� ü�� (���� �� �ִ� Ƚ��)
    private int currentHits = 0; // ������� ���� Ƚ��
    private bool isDead = false; // �׾����� ����

    public GameObject gameOverUI; // ���� ���� UI �г�
    public TextMeshProUGUI finalScoreText; // ���� ���� �ؽ�Ʈ UI

    private bool isInvincible = false; // ���� ���� ����
    public float invincibleDuration = 2f; // ���� ���� �ð�

    private float invincibleTimer = 0f; // ���� �ð� Ÿ�̸�

    void Start()
    {
        // ���� �ν����Ϳ��� UI�� �������� �ʾҴٸ�, ������ �ڵ����� ã��
        if (gameOverUI == null)
            gameOverUI = GameObject.Find("GameOverPanel");

        // ���� ���� �� ���ӿ��� UI �����
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        // ���� �ؽ�Ʈ�� ������ ������ �ڵ����� ã��
        if (finalScoreText == null)
        {
            GameObject scoreObj = GameObject.Find("FinalScoreText");
            if (scoreObj != null)
                finalScoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        // ���� ������ ��� Ÿ�̸� ����
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            // ���� �ð��� ������ �ٽ� ���� �� �ֵ��� ����
            if (invincibleTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    // �ܺο��� �ǰݵ� �� ȣ��
    public void TakeHit()
    {
        if (isDead) return; // �̹� �׾����� ó������ ����
        if (isInvincible) return; // ���� ���̸� �ǰ� ����

        currentHits++; // ���� Ƚ�� ����
        Debug.Log("����! ���� ü��: " + (maxHits - currentHits));

        // �ǰ� �� ���� ���� ����
        isInvincible = true;
        invincibleTimer = invincibleDuration;

        // ü�� �ʰ� �� ���� ó��
        if (currentHits >= maxHits)
        {
            isDead = true;
            Time.timeScale = 0f; // ���� ����

            // ���ӿ��� UI Ȱ��ȭ
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);

                // ���� ���� �ؽ�Ʈ ����
                if (finalScoreText != null)
                {
                    finalScoreText.text = "Final Score: " + 0;
                }
            }
        }
    }
}
