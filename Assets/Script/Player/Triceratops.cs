using UnityEngine;

public class Triceratops : PlayerBase
{
    protected override void Start()
    {
        // 트리케라톱스 기본 스탯 설정
        moveSpeed = 5f; // 이동 속도
        dashForce = 10f; // 대시 힘
        maxHits = 5;  // 최대 체력

        base.Start(); // 부모 클래스 Start() 호출
    }

    // 데미지를 받았을 때
    protected override void OnTakeDamage()
    {
        Debug.Log("데미지를 받았습니다!(트리케라)"); //확인용
    }
}
