using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour
{
    // strat ��ư Ŭ�� �� ȣ��
    public void OnClickStart()
    {
        // GameScene �ҷ�����
        SceneManager.LoadScene("GameScene");
    }
}
