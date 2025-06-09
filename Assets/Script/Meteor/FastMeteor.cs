using UnityEngine;

public class FastMeteor : MeteorBase
{
    // �߻� �޼���
    protected override void InitializeMeteor()
    {
        // ���� ���� �ӵ�
        fallSpeed = Random.Range(10f, 14f);

        // ũ�� �۰�
        transform.localScale = new Vector3(0.8f, 0.8f, 1f);

        // ���� ȿ�� �Ⱥ��̰�(������ ���̻�)
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.material = new Material(Shader.Find("Sprites/Default"));
            trail.startColor = new Color(1f, 0f, 0f, 0f);
            trail.endColor = new Color(1f, 0f, 0f, 0f);
            trail.time = 0.01f;
        }
    }
}