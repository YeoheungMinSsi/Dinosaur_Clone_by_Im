using UnityEngine;

public class GiantMeteor : MeteorBase
{
    // 추상 메서드
    protected override void InitializeMeteor()
    {
        // 느린 낙하 속도
        fallSpeed = 0.1f;
        Debug.Log("Time.timeScale: " + Time.timeScale);
        Debug.Log("Time.deltaTime: " + Time.deltaTime);
        Debug.Log("GiantMeteor fallSpeed: " + fallSpeed);
        damage = 2; // 데미지 2

        // 객체 크기를 크게
        transform.localScale = new Vector3(3f, 3f, 1f);

        // 이것도 있으면 안이쁨
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