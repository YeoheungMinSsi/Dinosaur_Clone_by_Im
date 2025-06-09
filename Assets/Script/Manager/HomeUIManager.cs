using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour
{
    // strat 버튼 클릭 시 호출
    public void OnClickStart()
    {
        // GameScene 불러오기
        SceneManager.LoadScene("GameScene");
    }
}
