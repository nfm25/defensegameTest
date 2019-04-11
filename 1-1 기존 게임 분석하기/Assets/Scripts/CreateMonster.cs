using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour {

    
    public List<GameObject> respawnSpotList; // 자료형인 List를 public으로 선언해주면, 유니티가 자동으로
                                             // 인스펙터창에서 리스트의 각 원소들을 설정해줄 수 있는 형태로 바꿔준다.
    
    public GameObject monster1Prefab;
    public GameObject monster2Prefab;

    private GameObject monsterPrefab;

    private float lastSpawnTime;
    private int spawnCount = 0;

    void Start () {
        monsterPrefab = monster1Prefab;
        lastSpawnTime = Time.time;
	}
	
	void Update () {
		if(GameManager.instance.round <= GameManager.instance.totalRound)
        {
            float timeGap = Time.time - lastSpawnTime;
            if(((spawnCount == 0 && timeGap > GameManager.instance.roundReadyTime) // 새 라운드가 시작
                || timeGap > GameManager.instance.spawnTime)
                && spawnCount < GameManager.instance.spawnNumber)
            {
                lastSpawnTime = Time.time;
                int index = Random.Range(0, 4); // 0~3 까지의 숫자 중에서 랜덤한 숫자의 리스폰스팟 선택
                GameObject respawnSpot = respawnSpotList[index];
                Instantiate(monsterPrefab, respawnSpot.transform.position, Quaternion.identity);
                spawnCount += 1;
            }
            if(spawnCount == GameManager.instance.spawnNumber &&
               GameObject.FindGameObjectWithTag("Monster") == null)
            {
                if(GameManager.instance.totalRound == GameManager.instance.round)
                {
                    GameManager.instance.gameClear();
                    GameManager.instance.round += 1;
                    return;
                }
                GameManager.instance.clearRound();
                spawnCount = 0;
                lastSpawnTime = Time.time;

                if(GameManager.instance.round == 4)
                {
                    monsterPrefab = monster2Prefab;
                    GameManager.instance.spawnTime = 2.0f;
                    GameManager.instance.spawnNumber = 10;
                }
            }
        }
	}
}
