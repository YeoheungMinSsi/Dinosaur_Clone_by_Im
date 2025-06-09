using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    // restart��ư Ŭ�� �� ȣ��
    public void OnClickRestart()
    {
        Time.timeScale = 1f; // ������ �ð� �ٽ� ����ȭ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �����
    }

    // Home ��ư Ŭ�� �� ȣ��
    public void OnClickHome()
    {
        Time.timeScale = 1f; // ������ �ð� �ٽ� ����ȭ
        SceneManager.LoadScene("HomeScene"); // Ȩ ȭ������ �̵�
    }
}
