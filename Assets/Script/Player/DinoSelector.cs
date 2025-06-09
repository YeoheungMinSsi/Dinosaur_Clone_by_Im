using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoSelector : MonoBehaviour
{
    // 트리케라톱스
    public void SelectTriceratops()
    {
        // 선택한 캐릭터 정보를 "SelectedDino" 키로 저장(Triceratops)
        PlayerPrefs.SetString("SelectedDino", "Triceratops");
        // 게임 씬으로
        SceneManager.LoadScene("GameScene");
    }

    // 티라노사우루스
    public void SelectTyrannosaurus()
    {
        // 선택한 캐릭터 정보를 "SelectedDino" 키로 저장(Tyrannosaurus)
        PlayerPrefs.SetString("SelectedDino", "Tyrannosaurus");
        // 게임 씬으로
        SceneManager.LoadScene("GameScene");
    }

    // 브라키오사우루스
    public void SelectBrachiosaurus()
    {
        // 선택한 캐릭터 정보를 "SelectedDino" 키로 저장(Brachiosaurus)
        PlayerPrefs.SetString("SelectedDino", "Brachiosaurus");
        // 게임 씬으로
        SceneManager.LoadScene("GameScene");
    }
}
