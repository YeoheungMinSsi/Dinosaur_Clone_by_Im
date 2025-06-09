using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // 체력 이미지 배열
    public Image[] hearts;

    // 꽉 찬 하트
    public Sprite fullHeart;

    // 빈 하트
    public Sprite emptyHeart;

    // 남은 체력에 따라 하트 UI를 업데이트
    public void UpdateHearts(int remainingHealth)
    {
        // hearts 배열의 각 하트 이미지마다 반복하면서
        for (int i = 0; i < hearts.Length; i++)
        {
            // 현재 인덱스가 남은 체력보다 작으면 꽉 찬 하트,
            // 아니면 빈 하트로 변경
            hearts[i].sprite = (i < remainingHealth) ? fullHeart : emptyHeart;
        }
    }
}
