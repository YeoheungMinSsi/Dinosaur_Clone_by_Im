using UnityEngine;

public class RainDrop : MeteorBase
{
    // �߻� �޼���
    protected override void InitializeMeteor()
    {
        // ���׿����� ���� �ӵ� 5~8 ���� ����
        fallSpeed = Random.Range(5f, 8f);

        // ������ 1
        damage = 1;
    }
}