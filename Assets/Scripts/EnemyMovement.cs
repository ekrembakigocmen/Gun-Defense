using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavePointIndex = 0;
    private int wavePointStaticIndex;
    private float turnSpeed = 10f;



    public GameObject waveSpawner;
    private Enemy enemy;
    private void Awake()
    {
        waveSpawner = GameObject.FindGameObjectWithTag("GameController");
    }
    void Start()
    {
        enemy = GetComponent<Enemy>();

        if (enemy.isFirstSpawn)
        {
            wavePointStaticIndex = waveSpawner.GetComponent<WaveSpawner>().waveIndex - 1;
            if (waveSpawner.GetComponent<WaveSpawner>().waves[waveSpawner.GetComponent<WaveSpawner>().waveIndex - 1].green)
            {

                target = Waypoints.greenPoints[0];

            }
            else if (waveSpawner.GetComponent<WaveSpawner>().waves[waveSpawner.GetComponent<WaveSpawner>().waveIndex - 1].yellow)
            {
                target = Waypoints.yellowPoints[0];
            }
            else if (waveSpawner.GetComponent<WaveSpawner>().waves[waveSpawner.GetComponent<WaveSpawner>().waveIndex - 1].grey)
            {
                target = Waypoints.greyPoints[0];
            }
            else
            {
                target = Waypoints.redPoints[0];
            }
        }
        else
        {

            wavePointStaticIndex = waveSpawner.GetComponent<WaveSpawner>().middleWaveIndex;
            
            if (waveSpawner.GetComponent<WaveSpawner>().middleWave[waveSpawner.GetComponent<WaveSpawner>().middleWaveIndex].green)
            {

                target = Waypoints.greenPoints[0];

            }
            else if (waveSpawner.GetComponent<WaveSpawner>().middleWave[waveSpawner.GetComponent<WaveSpawner>().middleWaveIndex].yellow)
            {
                target = Waypoints.yellowPoints[0];
            }
            else if (waveSpawner.GetComponent<WaveSpawner>().middleWave[waveSpawner.GetComponent<WaveSpawner>().middleWaveIndex].grey)
            {
                target = Waypoints.greyPoints[0];
            }
            else
            {

                target = Waypoints.redPoints[0];
            }
        }


    }

    private void Update()
    {

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        transform.GetChild(0).gameObject.transform.rotation = Quaternion.identity; // healthbar sabit acida kalmasi icin
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (enemy.isFirstSpawn)
        {
            if (waveSpawner.GetComponent<WaveSpawner>().waves[wavePointStaticIndex].green)
            {
                if (wavePointIndex >= Waypoints.greenPoints.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavePointIndex++;
                target = Waypoints.greenPoints[wavePointIndex];
                return;

            }
            else if (waveSpawner.GetComponent<WaveSpawner>().waves[wavePointStaticIndex].yellow)
            {
                if (wavePointIndex >= Waypoints.yellowPoints.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavePointIndex++;
                target = Waypoints.yellowPoints[wavePointIndex];
                return;
            }
            else if (waveSpawner.GetComponent<WaveSpawner>().waves[wavePointStaticIndex].grey)
            {
                if (wavePointIndex >= Waypoints.greyPoints.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavePointIndex++;
                target = Waypoints.greyPoints[wavePointIndex];
                return;
            }
            else
            {
                if (wavePointIndex >= Waypoints.redPoints.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavePointIndex++;
                target = Waypoints.redPoints[wavePointIndex];
            }
        }
        else
        {

            if (waveSpawner.GetComponent<WaveSpawner>().middleWave[wavePointStaticIndex].green)
            {
                if (wavePointIndex >= Waypoints.greenPoints.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavePointIndex++;
                target = Waypoints.greenPoints[wavePointIndex];
                return;

            }
            else if (waveSpawner.GetComponent<WaveSpawner>().middleWave[wavePointStaticIndex].yellow)
            {

                if (wavePointIndex >= Waypoints.yellowPoints.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavePointIndex++;
                target = Waypoints.yellowPoints[wavePointIndex];
                return;
            }
            else if (waveSpawner.GetComponent<WaveSpawner>().middleWave[wavePointStaticIndex].grey)
            {
                if (wavePointIndex >= Waypoints.greyPoints.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavePointIndex++;
                target = Waypoints.greyPoints[wavePointIndex];
                return;
            }
            else
            {
                if (wavePointIndex >= Waypoints.redPoints.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavePointIndex++;
                target = Waypoints.redPoints[wavePointIndex];
            }
        }


    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        WaveSpawner.IsEnemies--;
        Destroy(gameObject);
    }


}
