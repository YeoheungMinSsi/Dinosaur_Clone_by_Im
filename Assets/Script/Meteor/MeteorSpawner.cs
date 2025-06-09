using UnityEngine;

// �� � Ÿ���� ����
[System.Serializable]
public class MeteorType
{
    public GameObject prefab;     // ������ ���׿� ������
    public float spawnRate;       // �� ��� ������ Ȯ�� ����
}

// ����� ���� �ð� �������� ���� ��ġ�� �����ϴ� ������Ʈ
public class MeteorSpawner : MonoBehaviour
{
    public MeteorType[] meteorTypes;     // � ���� �迭
    public float spawnInterval = 1f;     // � ���� �ֱ�
    public float minX = -6f, maxX = 6f;  // � ���� X ��ǥ ����
    public float spawnY = 10f;           // � ���� Y ��ǥ ������
    private float timer;                 // �ð� ������ Ÿ�̸�

    private void Update()
    {
        // �� ������ �ð� ����
        timer += Time.deltaTime;

        // ������ �ֱ⸸ŭ �ð��� ������ � ����
        if (timer >= spawnInterval)
        {
            SpawnMeteor();      // � ���� ȣ��
            timer = 0f;         // Ÿ�̸� �ʱ�ȭ
        }
    }

    // � �ϳ��� �����Ͽ� ����
    void SpawnMeteor()
    {
        GameObject meteorToSpawn = ChooseMeteorType();  // Ȯ�� ������� � ����
        if (meteorToSpawn == null) return;

        // X�� ���� ��ġ���� � ����
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        // ���õ� � �������� �ش� ��ġ�� ����
        Instantiate(meteorToSpawn, spawnPos, Quaternion.identity);
    }

    // Ȯ�� ������� � ��� �������� ����
    GameObject ChooseMeteorType()
    {
        float total = 0f;

        // ��ü ���� Ȯ���� ���� ���
        foreach (var type in meteorTypes)
        {
            if (type.prefab != null)
                total += type.spawnRate;
        }

        float rand = Random.Range(0, total);  // ������ ����
        float cumulative = 0f;

        // ���� Ȯ���� ���ϸ� �ش� ��� ����
        foreach (var type in meteorTypes)
        {
            if (type.prefab == null) continue;

            cumulative += type.spawnRate;
            if (rand <= cumulative)
            {
                Debug.Log("Spawned Meteor: " + type.prefab.name);  // Ȯ�ο�
                return type.prefab;
            }
        }

        // Ȥ�� ���� ���õ��� �ʾ��� �� ù ��° ����� ��ȯ�ϱ�
        return meteorTypes.Length > 0 ? meteorTypes[0].prefab : null;
    }
}
