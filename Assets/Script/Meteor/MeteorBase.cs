using UnityEngine;

// ���� ����(�߻� Ŭ����)
public abstract class MeteorBase : MonoBehaviour
{
    protected float fallSpeed = 5f; // ���� �ӵ�(�ڽĿ��� ����)
    public int damage = 1; // ������
    public GameObject smokePrefab; // �浹 �� ���� ����Ʈ

    // ��ü ���� �� ȣ��
    protected virtual void Start()
    {
        // �ڽ� Ŭ�������� ��ü���� ����
        InitializeMeteor();
    }

    // �ڽ� Ŭ�������� �ݵ�� ����
    protected abstract void InitializeMeteor();

    // �����Ӹ��� ȣ��
    protected virtual void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // ȭ�� �Ʒ��� ���� �ڵ� ����
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    // �ٸ� �ݶ��̴��� �浹 �� ȣ��
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 contactPoint = collision.GetContact(0).point;

        // ���� �ε����� �ı�
        if (collision.collider.CompareTag("Ground"))
        {
            SpawnSmoke(contactPoint);
            Destroy(gameObject);
        }
        // �÷��̾�� �浹
        else if (collision.collider.CompareTag("Player"))
        {
            // PlayerBase ������Ʈ�� ���� Ȯ���ϰ�
            PlayerBase playerBase = collision.collider.GetComponent<PlayerBase>();

            if (playerBase != null)
            {
                // ����, ��� ���¸� ����
                if (playerBase.IsInvincible() || playerBase.IsDead()) return;

                // ���ظ� ������ ����
                playerBase.TakeDamage(damage);
                SpawnSmoke(contactPoint);
                Destroy(gameObject);
            }
            else
            {
                // ���� PlayerBase�� ���� PlayerHealth�� �ִٸ� (������ ȣȯ ���� �ڵ�)
                PlayerHealth health = collision.collider.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeHit();  // ü�� ó��
                    SpawnSmoke(contactPoint);
                    Destroy(gameObject);
                }
            }
        }
    }

    // ���� ����Ʈ
    protected void SpawnSmoke(Vector3 position)
    {
        if (smokePrefab != null)
        {
            // ���� ������ ����
            GameObject smoke = Instantiate(smokePrefab, position, Quaternion.identity);

            // ���� �ð���ŭ �� �ڵ� ����
            ParticleSystem ps = smoke.GetComponent<ParticleSystem>();
            Destroy(smoke, ps != null ? ps.main.duration : 1f);
        }
    }
}