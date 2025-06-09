using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    public GameObject rainDropPrefab;
    public float spawnMinY = 15f; // 비 생성 최소 높이
    public float spawnMaxY = 20f; // 비 생성 최대 높이

    private float spawnMinX, spawnMaxX; // 화면 기준 가로 스폰 범위
    private float timer = 0f; // 스폰 타이머
    private float currentSpawnInterval; // 현재 스폰 간격

    void Start()
    {
        SetupSpawnBounds();  // 화면 크기에 맞춰 스폰 경계 설정
        SetNextSpawnInterval(); // 첫 스폰 간격 설정
    }

    void SetupSpawnBounds()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize * 2f; // 카메라 세로 크기
        float camWidth = camHeight * cam.aspect;  // 카메라 가로 크기

        // 화면 바깥까지 포함. 조금 여유있게 스폰 범위 지정
        spawnMinX = -camWidth / 2f - 1f; // 왼쪽으로 1 여유
        spawnMaxX = camWidth / 2f + 1f; // 오른쪽으로 1 여유

        Debug.Log($"Rain spawn bounds: minX={spawnMinX}, maxX={spawnMaxX}, camWidth={camWidth}");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnInterval)
        {
            timer = 0f;
            SpawnRainDrops(); // 화산 파편 생성
            SetNextSpawnInterval(); // 다음 생성 간격 설정
        }
    }

    void SetNextSpawnInterval()
    {
        // 0.1초에서 1초 사이 랜덤 스폰 간격 설정
        currentSpawnInterval = Random.Range(0.1f, 1f);
    }

    void SpawnRainDrops()
    {
        int count = Random.Range(2, 4); // 한 번에 2~3개 파편 생성

        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(spawnMinX, spawnMaxX);
            float randomY = Random.Range(spawnMinY, spawnMaxY);
            Vector2 spawnPos = new Vector2(randomX, randomY);

            GameObject raindrop = Instantiate(rainDropPrefab, spawnPos, Quaternion.identity);

            RainDrop rainDropScript = raindrop.GetComponent<RainDrop>();
            if (rainDropScript != null)
            {
                //나중에 코드
            }

            Debug.Log($"Raindrop spawned at: ({randomX:F2}, {randomY:F2})");
        }
    }

    // 화면 크기 변경 시 스폰 경계 재설정
    public void RefreshBounds()
    {
        SetupSpawnBounds();
    }

    // 에디터에서 스폰 영역을 시각적으로 확인하기 위해
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
