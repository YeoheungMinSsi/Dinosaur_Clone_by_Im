using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    // restart버튼 클릭 시 호출
    public void OnClickRestart()
    {
        Time.timeScale = 1f; // 정지된 시간 다시 정상화
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 게임 재시작
    }

    // Home 버튼 클릭 시 호출
    public void OnClickHome()
    {
        Time.timeScale = 1f; // 정지된 시간 다시 정상화
        SceneManager.LoadScene("HomeScene"); // 홈 화면으로 이동
    }
}
