using UnityEngine;

public class Triceratops : PlayerBase
{
    protected override void Start()
    {
        // Ʈ���ɶ��齺 �⺻ ���� ����
        moveSpeed = 5f; // �̵� �ӵ�
        dashForce = 10f; // ��� ��
        maxHits = 5;  // �ִ� ü��

        base.Start(); // �θ� Ŭ���� Start() ȣ��
    }

    // �������� �޾��� ��
    protected override void OnTakeDamage()
    {
        Debug.Log("�������� �޾ҽ��ϴ�!(Ʈ���ɶ�)"); //Ȯ�ο�
    }
}
