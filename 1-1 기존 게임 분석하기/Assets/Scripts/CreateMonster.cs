using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour {

    private GameManager gameManager;

    public List<GameObject> respawnSpotList; // 자료형인 List를 public으로 선언해주면, 유니티가 자동으로
                                             // 인스펙터창에서 리스트의 각 원소들을 설정해줄 수 있는 형태로 바꿔준다.
    
    public GameObject monster1Prefab;
    public GameObject monster2Prefab;

    private GameObject monsterPrefab;

    private float lastSpawnTime;
    private int spawnCount = 0;

    void Start () {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        monsterPrefab = monster1Prefab;
        lastSpawnTime = Time.time;
	}
	
	void Update () {
		if(gameManager.round <= gameManager.totalRound)
        {
            float timeGap = Time.time - lastSpawnTime;
            if(((spawnCount == 0 && timeGap > gameManager.roundReadyTime) // 새 라운드가 시작
                || timeGap > gameManager.spawnTime)
                && spawnCount < gameManager.spawnNumber)
            {
                lastSpawnTime = Time.time;
                int index = Random.Range(0, 4); // 0~3 까지의 숫자 중에서 랜덤한 숫자의 리스폰스팟 선택
                GameObject respawnSpot = respawnSpotList[index];
                Instantiate(monsterPrefab, respawnSpot.transform.position, Quaternion.identity);
                spawnCount += 1;
            }
            if(spawnCount == gameManager.spawnNumber &&
               GameObject.FindGameObjectWithTag("Monster") == null)
            {
                if(gameManager.totalRound == gameManager.round)
                {
                    gameManager.gameClear();
                    gameManager.round += 1;
                    return;
                }
                gameManager.clearRound();
                spawnCount = 0;
                lastSpawnTime = Time.time;

                if(gameManager.round == 4)
                {
                    monsterPrefab = monster2Prefab;
                    gameManager.spawnTime = 2.0f;
                    gameManager.spawnNumber = 10;
                }
            }
        }
	}
}
