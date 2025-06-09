using UnityEngine;

// 각 운석 타입의 정보
[System.Serializable]
public class MeteorType
{
    public GameObject prefab;     // 생성할 메테오 프리팹
    public float spawnRate;       // 이 운석이 생성될 확률 비율
}

// 운석들을 일정 시간 간격으로 랜덤 위치에 생성하는 컴포넌트
public class MeteorSpawner : MonoBehaviour
{
    public MeteorType[] meteorTypes;     // 운석 종류 배열
    public float spawnInterval = 1f;     // 운석 생성 주기
    public float minX = -6f, maxX = 6f;  // 운석 생성 X 좌표 범위
    public float spawnY = 10f;           // 운석 생성 Y 좌표 고정값
    private float timer;                 // 시간 측정용 타이머

    private void Update()
    {
        // 매 프레임 시간 누적
        timer += Time.deltaTime;

        // 설정한 주기만큼 시간이 지나면 운석 생성
        if (timer >= spawnInterval)
        {
            SpawnMeteor();      // 운석 생성 호출
            timer = 0f;         // 타이머 초기화
        }
    }

    // 운석 하나를 선택하여 생성
    void SpawnMeteor()
    {
        GameObject meteorToSpawn = ChooseMeteorType();  // 확률 기반으로 운석 선택
        if (meteorToSpawn == null) return;

        // X축 랜덤 위치에서 운석 생성
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        // 선택된 운석 프리팹을 해당 위치에 생성
        Instantiate(meteorToSpawn, spawnPos, Quaternion.identity);
    }

    // 확률 기반으로 어떤 운석을 생성할지 선택
    GameObject ChooseMeteorType()
    {
        float total = 0f;

        // 전체 스폰 확률의 총합 계산
        foreach (var type in meteorTypes)
        {
            if (type.prefab != null)
                total += type.spawnRate;
        }

        float rand = Random.Range(0, total);  // 랜덤값 선택
        float cumulative = 0f;

        // 누적 확률로 비교하며 해당 운석을 선택
        foreach (var type in meteorTypes)
        {
            if (type.prefab == null) continue;

            cumulative += type.spawnRate;
            if (rand <= cumulative)
            {
                Debug.Log("Spawned Meteor: " + type.prefab.name);  // 확인용
                return type.prefab;
            }
        }

        // 혹시 설마 선택되지 않았을 때 첫 번째 운석으로 반환하기
        return meteorTypes.Length > 0 ? meteorTypes[0].prefab : null;
    }
}
