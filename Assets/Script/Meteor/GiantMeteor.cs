using UnityEngine;

public class GiantMeteor : MeteorBase
{
    // �߻� �޼���
    protected override void InitializeMeteor()
    {
        // ���� ���� �ӵ�
        fallSpeed = 0.1f;
        Debug.Log("Time.timeScale: " + Time.timeScale);
        Debug.Log("Time.deltaTime: " + Time.deltaTime);
        Debug.Log("GiantMeteor fallSpeed: " + fallSpeed);
        damage = 2; // ������ 2

        // ��ü ũ�⸦ ũ��
        transform.localScale = new Vector3(3f, 3f, 1f);

        // �̰͵� ������ ���̻�
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