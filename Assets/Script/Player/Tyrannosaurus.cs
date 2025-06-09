using UnityEngine;

//좀 빠름(나중에 체력 좀 깎을 예정)
public class Tyrannosaurus : PlayerBase
{
    protected override void Start()
    {
        // 티라노사우루스 스탯 설정 (빠르고 강한 특성)
        moveSpeed = 6.5f; // 이동 속도
        dashForce = 12f; // 대시 힘
        dashSpeed = 18f; // 대시 속도
        maxHits = 5; // 최대 체력 (맞을 수 있는 횟수)

        base.Start(); // 부모 클래스 Start() 호출로 기본 초기화 유지
    }

    // 데미지를 받았을 때
    protected override void OnTakeDamage()
    {
        Debug.Log("데미지를 받았습니다!(티라노)"); //확인용
    }
}
