using UnityEngine;

public class RainDrop : MeteorBase
{
    // 추상 메서드
    protected override void InitializeMeteor()
    {
        // 메테오보다 낙하 속도 5~8 사이 랜덤
        fallSpeed = Random.Range(5f, 8f);

        // 데미지 1
        damage = 1;
    }
}