using UnityEngine;

public class Brachiosaurus : PlayerBase
{
    // 브라키오사우루스 초기 스탯 설정
    protected override void Start()
    {
        // 브라키오사우루스는 이동 속도가 느림(체력은 추후에 설정할거임)
        moveSpeed = 3.5f; // 이속
        dashForce = 8f; // 대쉬 힘
        dashSpeed = 12f; // 대쉬 속도
        maxHits = 5; // 체력

        base.Start(); // 부모 클래스 Start() 메서드 실행
    }

    // 데미지를 받았을 때
    protected override void OnTakeDamage()
    {
        Debug.Log("데미지를 받았습니다!(브라키오)");  // 확인용
    }
}
