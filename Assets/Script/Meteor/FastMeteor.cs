using UnityEngine;

public class FastMeteor : MeteorBase
{
    // 추상 메서드
    protected override void InitializeMeteor()
    {
        // 빠른 낙하 속도
        fallSpeed = Random.Range(10f, 14f);

        // 크기 작게
        transform.localScale = new Vector3(0.8f, 0.8f, 1f);

        // 꼬리 효과 안보이게(있으면 안이쁨)
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