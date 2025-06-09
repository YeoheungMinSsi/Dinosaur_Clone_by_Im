using UnityEngine;
using TMPro;

//없어도 돌아가야하는데 지우니까 오류나니 일단 살려둔 코드

public class PlayerHealth : MonoBehaviour
{
    public int maxHits = 5; // 최대 체력 (맞을 수 있는 횟수)
    private int currentHits = 0; // 현재까지 맞은 횟수
    private bool isDead = false; // 죽었는지 여부

    public GameObject gameOverUI; // 게임 오버 UI 패널
    public TextMeshProUGUI finalScoreText; // 최종 점수 텍스트 UI

    private bool isInvincible = false; // 무적 상태 여부
    public float invincibleDuration = 2f; // 무적 지속 시간

    private float invincibleTimer = 0f; // 무적 시간 타이머

    void Start()
    {
        // 만약 인스펙터에서 UI를 연결하지 않았다면, 씬에서 자동으로 찾음
        if (gameOverUI == null)
            gameOverUI = GameObject.Find("GameOverPanel");

        // 게임 시작 시 게임오버 UI 숨기기
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        // 점수 텍스트도 없으면 씬에서 자동으로 찾기
        if (finalScoreText == null)
        {
            GameObject scoreObj = GameObject.Find("FinalScoreText");
            if (scoreObj != null)
                finalScoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        // 무적 상태일 경우 타이머 감소
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            // 무적 시간이 끝나면 다시 맞을 수 있도록 설정
            if (invincibleTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    // 외부에서 피격될 때 호출
    public void TakeHit()
    {
        if (isDead) return; // 이미 죽었으면 처리하지 않음
        if (isInvincible) return; // 무적 중이면 피격 무시

        currentHits++; // 맞은 횟수 증가
        Debug.Log("맞음! 현재 체력: " + (maxHits - currentHits));

        // 피격 후 무적 상태 진입
        isInvincible = true;
        invincibleTimer = invincibleDuration;

        // 체력 초과 시 죽음 처리
        if (currentHits >= maxHits)
        {
            isDead = true;
            Time.timeScale = 0f; // 게임 정지

            // 게임오버 UI 활성화
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);

                // 최종 점수 텍스트 설정
                if (finalScoreText != null)
                {
                    finalScoreText.text = "Final Score: " + 0;
                }
            }
        }
    }
}
