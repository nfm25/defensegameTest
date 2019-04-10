using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStat //VO ==> Value Object, 단순히 다른 스크립트에 값을 부여하기 위한 데이터만 저장, 설정하는 스크립트
    // 그러므로, MonoBehaviour가 필요하지 않다.
{
    public float speed { get; set; }
    public int damage { get; set; }

    public BulletStat(float speed, int damage)
    {
        this.speed = speed; // this ==> 생성자
        this.damage = damage;
    }
}
  
