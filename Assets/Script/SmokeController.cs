using UnityEngine;

public class SmokeController : MonoBehaviour
{
    public float lifetime = 1.0f;  // ����ũ �ð�

    void Start()
    {
        Destroy(gameObject, lifetime);  // lifetime�� �� �ڵ� ����
    }
}
