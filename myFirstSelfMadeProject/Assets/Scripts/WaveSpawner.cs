using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        //public GameObject sEnemie;
        //public GameObject llEnemie;
        //public GameObject bEnemie;
        public int scount;
        public int lcount;
        public int bcount;
        
        //public int count;
        public float timeBetweenSpawns;
    }

    public GameObject Pl;
    private Player ps;
    public GameObject sEnemie;
    public GameObject llEnemie;
    public GameObject bEnemie;


    private int count;
    

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finishedSpawning;

   
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
        ps = Pl.GetComponent<Player>();

    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        Debug.Log("Wave" + (currentWaveIndex + 1 ));
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        
        currentWave = waves[index];
        count = currentWave.scount + currentWave.bcount + currentWave.lcount;
        for (int i = 0; i < count; i++)
        {
            if(player == null)
            {
                yield break;
            }
            //GameObject randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            

            for (int b = 0; b < currentWave.bcount; b++)
            {
                if (GameObject.FindGameObjectWithTag("BEnemy") == null)
                {
                    Instantiate(bEnemie, randomSpot.position, randomSpot.rotation);
                    ps.BT = GameObject.FindGameObjectWithTag("BEnemy").GetComponent<Bt_enemy>();
                }
            }

            for (int l = 0; l < currentWave.lcount; l++)
            {
                if (GameObject.FindGameObjectWithTag("LEnemy") == null)
                {
                    Instantiate(llEnemie, randomSpot.position, randomSpot.rotation);
                    ps.LL = GameObject.FindGameObjectWithTag("LEnemy").GetComponent<LL_enemy>();
                }
            }


           
            for(int s = 0; s < currentWave.scount; s++)
            {
                if (GameObject.FindGameObjectWithTag("SEnemy") == null)
                {
                    Instantiate(sEnemie, randomSpot.position, randomSpot.rotation);
                    ps.ST = GameObject.FindGameObjectWithTag("SEnemy").GetComponent<St_enemy>();
                }
            }
            
            


            if (i == count - 1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(finishedSpawning == true && GameObject.FindGameObjectsWithTag("SEnemy").Length == 0 && GameObject.FindGameObjectsWithTag("BEnemy").Length == 0 && GameObject.FindGameObjectsWithTag("LEnemy").Length == 0)
        {
            
            finishedSpawning = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
    }
}
