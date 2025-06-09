using UnityEngine;

//�� ����(���߿� ü�� �� ���� ����)
public class Tyrannosaurus : PlayerBase
{
    protected override void Start()
    {
        // Ƽ�����罺 ���� ���� (������ ���� Ư��)
        moveSpeed = 6.5f; // �̵� �ӵ�
        dashForce = 12f; // ��� ��
        dashSpeed = 18f; // ��� �ӵ�
        maxHits = 5; // �ִ� ü�� (���� �� �ִ� Ƚ��)

        base.Start(); // �θ� Ŭ���� Start() ȣ��� �⺻ �ʱ�ȭ ����
    }

    // �������� �޾��� ��
    protected override void OnTakeDamage()
    {
        Debug.Log("�������� �޾ҽ��ϴ�!(Ƽ���)"); //Ȯ�ο�
    }
}
