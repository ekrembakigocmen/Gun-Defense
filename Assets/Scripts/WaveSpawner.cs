using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public static int IsEnemies = 0;
    public static int won = 0;


    public Transform enemyPrefab;
    public Transform spawnPoint;
    public TextMeshProUGUI waveTimer;
    public float rateTimeDividing = 5f;
    public int timeBetweenIndisMiddleWave = 2;
    public Wave[] waves, middleWave;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    [HideInInspector] public int waveIndex = 0;
    [HideInInspector] public int middleWaveIndex = 0;

    public GameManager gameManager;
    private void Update()
    {

        if (IsEnemies > 0)// gelen dalga olene kadar bura caliscak oldukten sonra IsEnemies 1 olcak ve diger dalga gelcek
        {
            return;
        }

        if (won == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            if (won < waves.Length)
            {
                StartCoroutine(SpawnMiddleWave());
            }
            countdown = timeBetweenWaves;

            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveTimer.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {

        won++;
        PlayerStats.rounds++;
        Wave wave = waves[waveIndex];
        IsEnemies += wave.enemySpawnCount;

        if (waveIndex + 1 < waves.Length)
        {
            waveIndex++;
        }

        for (int i = 0; i < wave.enemySpawnCount; i++)
        {
            wave.Enemy.transform.GetComponent<Enemy>().isFirstSpawn = true;
            SpawnEnemy(wave.Enemy);
            
            IsEnemies--;
            yield return new WaitForSeconds(rateTimeDividing / wave.rate);


        }


    }

    IEnumerator SpawnMiddleWave()
    {

        middleWaveIndex = 0;
        for (int j = 0; j < middleWave.Length; j++)
        {
            Wave wave2 = middleWave[middleWaveIndex];

            for (int i = 0; i < wave2.enemySpawnCount; i++)
            {

                yield return new WaitForSeconds(rateTimeDividing / wave2.rate);
                wave2.Enemy.transform.GetComponent<Enemy>().isFirstSpawn = false;
                SpawnEnemy(wave2.Enemy);
                

            }
           
            yield return new WaitForSeconds(timeBetweenIndisMiddleWave);
            if (middleWaveIndex != middleWave.Length - 1)
            {
                middleWaveIndex++;
            }

        }
       



    }

    void SpawnEnemy(GameObject enemy)
    {
        IsEnemies++;
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

    }
}
