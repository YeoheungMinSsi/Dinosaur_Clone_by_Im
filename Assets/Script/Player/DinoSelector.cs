using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoSelector : MonoBehaviour
{
    // Ʈ���ɶ��齺
    public void SelectTriceratops()
    {
        // ������ ĳ���� ������ "SelectedDino" Ű�� ����(Triceratops)
        PlayerPrefs.SetString("SelectedDino", "Triceratops");
        // ���� ������
        SceneManager.LoadScene("GameScene");
    }

    // Ƽ�����罺
    public void SelectTyrannosaurus()
    {
        // ������ ĳ���� ������ "SelectedDino" Ű�� ����(Tyrannosaurus)
        PlayerPrefs.SetString("SelectedDino", "Tyrannosaurus");
        // ���� ������
        SceneManager.LoadScene("GameScene");
    }

    // ���Ű�����罺
    public void SelectBrachiosaurus()
    {
        // ������ ĳ���� ������ "SelectedDino" Ű�� ����(Brachiosaurus)
        PlayerPrefs.SetString("SelectedDino", "Brachiosaurus");
        // ���� ������
        SceneManager.LoadScene("GameScene");
    }
}
