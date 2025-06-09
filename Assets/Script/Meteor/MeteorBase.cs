using UnityEngine;

// 공통 파편(추상 클래스)
public abstract class MeteorBase : MonoBehaviour
{
    protected float fallSpeed = 5f; // 낙하 속도(자식에서 수정)
    public int damage = 1; // 데미지
    public GameObject smokePrefab; // 충돌 시 연기 이펙트

    // 객체 생성 시 호출
    protected virtual void Start()
    {
        // 자식 클래스에서 구체적인 설정
        InitializeMeteor();
    }

    // 자식 클래스에서 반드시 구현
    protected abstract void InitializeMeteor();

    // 프레임마다 호출
    protected virtual void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // 화면 아래로 가면 자동 삭제
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    // 다른 콜라이더와 충돌 시 호출
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 contactPoint = collision.GetContact(0).point;

        // 땅에 부딪히면 파괴
        if (collision.collider.CompareTag("Ground"))
        {
            SpawnSmoke(contactPoint);
            Destroy(gameObject);
        }
        // 플레이어와 충돌
        else if (collision.collider.CompareTag("Player"))
        {
            // PlayerBase 컴포넌트로 상태 확인하고
            PlayerBase playerBase = collision.collider.GetComponent<PlayerBase>();

            if (playerBase != null)
            {
                // 무적, 사망 상태면 무시
                if (playerBase.IsInvincible() || playerBase.IsDead()) return;

                // 피해를 입히고 삭제
                playerBase.TakeDamage(damage);
                SpawnSmoke(contactPoint);
                Destroy(gameObject);
            }
            else
            {
                // 만약 PlayerBase가 없고 PlayerHealth만 있다면 (구버전 호환 위한 코드)
                PlayerHealth health = collision.collider.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeHit();  // 체력 처리
                    SpawnSmoke(contactPoint);
                    Destroy(gameObject);
                }
            }
        }
    }

    // 연기 이펙트
    protected void SpawnSmoke(Vector3 position)
    {
        if (smokePrefab != null)
        {
            // 연기 프리팹 생성
            GameObject smoke = Instantiate(smokePrefab, position, Quaternion.identity);

            // 지속 시간만큼 후 자동 삭제
            ParticleSystem ps = smoke.GetComponent<ParticleSystem>();
            Destroy(smoke, ps != null ? ps.main.duration : 1f);
        }
    }
}