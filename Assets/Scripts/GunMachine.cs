using UnityEngine;

public class GunMachine : MonoBehaviour
{

    private Transform target;

    [Header("Attirbutes")]
    public float range;
    public float fireRate;
    public static int increaseDamage;
    public int priceMultiplier;
    public int damage;
    public int gunLevel;
    public int upgradePrice;
    public int sellPrice;
    private float fireCountdown = 0f;
    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    private float turnSpeed = 10f;
    public Transform Rotator;

    public GameObject bulletPrefab;
    public Transform LeftfirePoint;
    public Transform RightfirePoint;




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
        }
        else
            target = null;
    } // turretlerin hedefine giren dusmanlarin tespiti


    private void Update()
    {
        if (target == null)
            return;
        // *********  Target lock on ************
        Vector3 dir = target.position - transform.position; // enemy pozisyonundan taretin pozisyonunu cikar
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(Rotator.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f); // Rotator objesinin rotasyonunu gelen yeni rotasyona esitle esitle
        // **************************************

        if (fireCountdown <= 0f)
        {
            increaseDamage = damage;
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject LbulletGo = (GameObject)Instantiate(bulletPrefab, LeftfirePoint.position, LeftfirePoint.rotation);
        GameObject RbulletGo = (GameObject)Instantiate(bulletPrefab, RightfirePoint.position, RightfirePoint.rotation);
        GunMachineBullet GMBulletL = LbulletGo.GetComponent<GunMachineBullet>();
        GunMachineBullet GMBulletR = RbulletGo.GetComponent<GunMachineBullet>();

        if (GMBulletL != null && GMBulletR != null)
        {
            GMBulletL.Seek(target);
            GMBulletR.Seek(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);


    }
}
