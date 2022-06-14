using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Attirbutes")]
    public float range ;
    public float fireRate ;
    private float fireCountdown =0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime;
    public float slowAmount;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Upgrade Parameter")]
    public static int increaseDamage;
    public int damage;
    public int gunLevel;
    public static float increaseRadius;
    public float radius;
    public int priceMultiplier;
    public int upgradePrice;
    public int sellPrice;



    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    private float turnSpeed = 10f;
    public Transform Rotator;

    public GameObject bulletPrefab;
    public Transform firePoint;





    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
        
    }
    void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
        }
       
        
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
            target = null;

        increaseDamage = damage;
        increaseRadius = radius;
    } // turretlerin hedefine giren dusmanlarin tespiti


    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }
        LockOnTarget();
        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }


    }


    void LockOnTarget()
    {
        // *********  Target lock on ************
        Vector3 dir = target.position - transform.position; // enemy pozisyonundan taretin pozisyonunu cikar
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(Rotator.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f); // Rotator objesinin rotasyonunu gelen yeni rotasyona esitle esitle
        // **************************************
    }


    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            impactEffect.Play();
            lineRenderer.enabled = true;
            impactLight.enabled = true;
        }


        lineRenderer.SetPosition(0, firePoint.position); // line renderer positionda 0 inci index , firePoint pozisyonundan
        lineRenderer.SetPosition(1, target.position); // line renderer positionda 1 inci index , target  pozisyonuna Line olustur.

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GunMachineBullet GMBulletL = bulletGo.GetComponent<GunMachineBullet>();
        if (GMBulletL != null)
        {
            GMBulletL.Seek(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
