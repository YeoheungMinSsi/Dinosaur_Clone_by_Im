using UnityEngine;
using TMPro;

// 추상 클래스
public abstract class PlayerBase : MonoBehaviour
{
    // ===== 이동 관련 설정 =====
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // 기본 이동 속도
    public float dashForce = 10f; // 대시 힘(미사용)
    public float dashDistance = 3f; // 대시 이동 거리
    public float dashSpeed = 15f; // 대시 속도

    // ===== 체력 관련 설정 =====
    [Header("Health Settings")]
    public int maxHits = 5; // 최대 체력
    public float invincibleDuration = 2f; // 피격 후 무적 시간

    // ===== UI 연결 =====
    [Header("UI References")]
    public GameObject gameOverUI;
    public TextMeshProUGUI finalScoreText;
    public HealthUI healthUI;

    // ===== 내부 변수 및 컴포넌트 =====
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D playerCollider;

    private int currentHits = 0; // 현재 맞은 횟수
    private bool isDead = false; // 사망 여부
    private bool isInvincible = false; // 무적 상태
    private float invincibleTimer = 0f;
    private Color originalColor;

    private bool canDash = true; // 대시 가능 여부
    private bool isDashing = false; // 대시 중 여부
    private Vector3 dashTarget;   // 대시 목표 지점
    private float minX, maxX; // 화면 이동 범위 제한

    // ===== 초기화 =====
    protected virtual void Start()
    {
        InitializeComponents();  // 필수 컴포넌트 연결
        SetupCameraBounds(); // 화면 경계 계산
        SetupUI(); // UI 요소 연결
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    private void SetupCameraBounds()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;
        float spriteHalfWidth = spriteRenderer.bounds.size.x / 2f;

        minX = -camWidth / 2f + spriteHalfWidth;
        maxX = camWidth / 2f - spriteHalfWidth;
    }

    private void SetupUI()
    {
        if (gameOverUI == null)
            gameOverUI = GameObject.Find("GameOverPanel");

        if (finalScoreText == null)
        {
            GameObject scoreObj = GameObject.Find("FinalScoreText");
            if (scoreObj != null)
                finalScoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        }

        if (healthUI == null)
            healthUI = FindObjectOfType<HealthUI>(); //이거뭐여 나중에 수정

        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        UpdateHealthUI();  // 체력 UI 초기화
    }

    // ===== 매 프레임마다 실행 =====
    protected virtual void Update()
    {
        HandleInvincibility(); // 무적 상태 처리
        HandleMovement(); // 이동 처리
        HandleDash(); // 대시 처리
        EnforceCameraBounds(); // 화면 밖으로 나가지 않도록 제한
    }

    private void EnforceCameraBounds()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }

    private void HandleInvincibility()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
                EndInvincibility();
        }
    }

    // ===== 이동 처리 =====
    protected virtual void HandleMovement()
    {
        if (isDashing) return;

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        Move(input);

        // 방향에 따라 좌우 반전
        if (input.x < 0) spriteRenderer.flipX = true;
        else if (input.x > 0) spriteRenderer.flipX = false;
    }

    public virtual void Move(Vector2 input)
    {
        if (rb != null)
        {
            Vector2 newVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
            Vector3 nextPos = transform.position + new Vector3(newVelocity.x * Time.deltaTime, 0, 0);

            if (nextPos.x < minX || nextPos.x > maxX)
                newVelocity.x = 0;

            rb.linearVelocity = newVelocity;
        }
        else
        {
            Vector3 move = new Vector3(input.x, 0, 0).normalized;
            Vector3 newPos = transform.position + move * moveSpeed * Time.deltaTime;

            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            transform.position = newPos;
        }
    }

    // ===== 대시 처리 =====
    protected virtual void HandleDash()
    {
        if (isDashing)
        {
            DashMove();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        canDash = false;

        float direction = spriteRenderer.flipX ? -1f : 1f;
        dashTarget = transform.position + Vector3.right * dashDistance * direction;
        dashTarget.x = Mathf.Clamp(dashTarget.x, minX, maxX);

        OnDashStart();
        Invoke(nameof(ResetDash), 3f); // 3초 쿨타임
    }

    private void DashMove()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        transform.position = newPos;

        if (Vector3.Distance(transform.position, dashTarget) < 0.01f)
        {
            isDashing = false;
            OnDashEnd();
        }
    }

    private void ResetDash()
    {
        canDash = true;
    }

    // ===== 피격 처리 =====
    public virtual void TakeDamage(int damage)
    {
        if (isDead || isInvincible) return;

        currentHits += damage;
        Debug.Log($"{damage} 데미지! 현재 체력: {maxHits - currentHits}");

        StartInvincibility();
        UpdateHealthUI();
        OnTakeDamage();

        if (currentHits >= maxHits)
            Die();
    }

    private void StartInvincibility()
    {
        isInvincible = true;
        invincibleTimer = invincibleDuration;

        if (playerCollider != null)
            playerCollider.enabled = false;

        if (spriteRenderer != null)
        {
            Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.4f);
            spriteRenderer.color = transparentColor;
        }
    }

    private void EndInvincibility()
    {
        isInvincible = false;

        if (playerCollider != null)
            playerCollider.enabled = true;

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }

    // ===== 사망 처리 =====
    private void Die()
    {
        isDead = true;
        Time.timeScale = 0f;

        OnDie();

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);

            if (finalScoreText != null)
            {
                ScoreManager scoreManager = FindObjectOfType<ScoreManager>(); // 이것도 나중에 수정
                int finalScore = scoreManager != null ? scoreManager.GetScore() : 0;
                finalScoreText.text = "Final Score: " + finalScore;
            }
        }
    }

    private void UpdateHealthUI()
    {
        if (healthUI != null)
        {
            int remainingHealth = maxHits - currentHits;
            healthUI.UpdateHearts(remainingHealth);
        }
    }

    // ===== 상태 확인 =====
    public bool IsInvincible() => isInvincible;
    public bool IsDashing() => isDashing;
    public bool IsDead() => isDead;
    public int GetCurrentHits() => currentHits;
    public int GetRemainingHealth() => maxHits - currentHits;
    public float GetMinX() => minX;
    public float GetMaxX() => maxX;

    // ===== 자식 클래스 오버라이드 가능 =====
    protected virtual void OnDashStart() { }
    protected virtual void OnDashEnd() { }
    protected virtual void OnTakeDamage() { }
    protected virtual void OnDie() { }
}
