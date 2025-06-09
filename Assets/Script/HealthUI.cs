using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // ü�� �̹��� �迭
    public Image[] hearts;

    // �� �� ��Ʈ
    public Sprite fullHeart;

    // �� ��Ʈ
    public Sprite emptyHeart;

    // ���� ü�¿� ���� ��Ʈ UI�� ������Ʈ
    public void UpdateHearts(int remainingHealth)
    {
        // hearts �迭�� �� ��Ʈ �̹������� �ݺ��ϸ鼭
        for (int i = 0; i < hearts.Length; i++)
        {
            // ���� �ε����� ���� ü�º��� ������ �� �� ��Ʈ,
            // �ƴϸ� �� ��Ʈ�� ����
            hearts[i].sprite = (i < remainingHealth) ? fullHeart : emptyHeart;
        }
    }
}
