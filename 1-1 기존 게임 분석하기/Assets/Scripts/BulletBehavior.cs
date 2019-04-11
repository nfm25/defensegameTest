using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public BulletStat bulletStat { get; set; } // get; set; 다음에 생성자가 온다. 그러므로 아래 public BulletBehavior 부분이 생성자임.

    public float activeTime = 3.0f;
    public float spawnTime;

    public BulletBehavior() // BulletBehavior가 초기화 될때, bulletStat 또한 0, 0으로(스피드, 데미지) 초기화되도록 설정한다.
    {
        bulletStat = new BulletStat(0, 0);
    }
    public GameObject character;

    public void Spawn()
    {
        gameObject.SetActive(true);
        spawnTime = Time.time;
    }

    void Start()
    {
        Spawn();
    }
         
    void Update ()
    {
       if(Time.time - spawnTime >= activeTime)
        {
            gameObject.SetActive(false);
        }
       else
        {
            transform.Translate(Vector2.right * bulletStat.speed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            gameObject.SetActive(false);
            other.GetComponent<MonsterStat>().attacked(bulletStat.damage); 
        }
    }


}

