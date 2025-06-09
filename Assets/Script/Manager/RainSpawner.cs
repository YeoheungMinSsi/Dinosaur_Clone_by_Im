using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    public GameObject rainDropPrefab;
    public float spawnMinY = 15f; // �� ���� �ּ� ����
    public float spawnMaxY = 20f; // �� ���� �ִ� ����

    private float spawnMinX, spawnMaxX; // ȭ�� ���� ���� ���� ����
    private float timer = 0f; // ���� Ÿ�̸�
    private float currentSpawnInterval; // ���� ���� ����

    void Start()
    {
        SetupSpawnBounds();  // ȭ�� ũ�⿡ ���� ���� ��� ����
        SetNextSpawnInterval(); // ù ���� ���� ����
    }

    void SetupSpawnBounds()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize * 2f; // ī�޶� ���� ũ��
        float camWidth = camHeight * cam.aspect;  // ī�޶� ���� ũ��

        // ȭ�� �ٱ����� ����. ���� �����ְ� ���� ���� ����
        spawnMinX = -camWidth / 2f - 1f; // �������� 1 ����
        spawnMaxX = camWidth / 2f + 1f; // ���������� 1 ����

        Debug.Log($"Rain spawn bounds: minX={spawnMinX}, maxX={spawnMaxX}, camWidth={camWidth}");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnInterval)
        {
            timer = 0f;
            SpawnRainDrops(); // ȭ�� ���� ����
            SetNextSpawnInterval(); // ���� ���� ���� ����
        }
    }

    void SetNextSpawnInterval()
    {
        // 0.1�ʿ��� 1�� ���� ���� ���� ���� ����
        currentSpawnInterval = Random.Range(0.1f, 1f);
    }

    void SpawnRainDrops()
    {
        int count = Random.Range(2, 4); // �� ���� 2~3�� ���� ����

        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(spawnMinX, spawnMaxX);
            float randomY = Random.Range(spawnMinY, spawnMaxY);
            Vector2 spawnPos = new Vector2(randomX, randomY);

            GameObject raindrop = Instantiate(rainDropPrefab, spawnPos, Quaternion.identity);

            RainDrop rainDropScript = raindrop.GetComponent<RainDrop>();
            if (rainDropScript != null)
            {
                //���߿� �ڵ�
            }

            Debug.Log($"Raindrop spawned at: ({randomX:F2}, {randomY:F2})");
        }
    }

    // ȭ�� ũ�� ���� �� ���� ��� �缳��
    public void RefreshBounds()
    {
        SetupSpawnBounds();
    }

    // �����Ϳ��� ���� ������ �ð������� Ȯ���ϱ� ����
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 topLeft = new Vector3(spawnMinX, spawnMaxY, 0);
        Vector3 topRight = new Vector3(spawnMaxX, spawnMaxY, 0);
        Vector3 bottomLeft = new Vector3(spawnMinX, spawnMinY, 0);
        Vector3 bottomRight = new Vector3(spawnMaxX, spawnMinY, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
