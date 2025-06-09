using UnityEngine;

public class Brachiosaurus : PlayerBase
{
    // ���Ű�����罺 �ʱ� ���� ����
    protected override void Start()
    {
        // ���Ű�����罺�� �̵� �ӵ��� ����(ü���� ���Ŀ� �����Ұ���)
        moveSpeed = 3.5f; // �̼�
        dashForce = 8f; // �뽬 ��
        dashSpeed = 12f; // �뽬 �ӵ�
        maxHits = 5; // ü��

        base.Start(); // �θ� Ŭ���� Start() �޼��� ����
    }

    // �������� �޾��� ��
    protected override void OnTakeDamage()
    {
        Debug.Log("�������� �޾ҽ��ϴ�!(���Ű��)");  // Ȯ�ο�
    }
}
