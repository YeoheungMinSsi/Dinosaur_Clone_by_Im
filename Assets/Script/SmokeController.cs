using UnityEngine;

public class SmokeController : MonoBehaviour
{
    public float lifetime = 1.0f;  // 스모크 시간

    void Start()
    {
        Destroy(gameObject, lifetime);  // lifetime초 후 자동 삭제
    }
}
