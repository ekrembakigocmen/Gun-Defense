using UnityEngine;

public class GunMachineBullet : MonoBehaviour
{
    private Transform target;
    public float speed;
    public float damage;

    [Header("Rocket")]
    public float explosionRadius;
    public GameObject explosionLight;
    public GameObject explosionPartical;

    // public GameObject impactEffect;


    public void Seek(Transform _target)
    {
        target = _target;
    }





    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            
            if (explosionRadius > 0f)
            {
                
                explosionRadius += Turret.increaseRadius;
                Explode();
                Destroy(gameObject);
            }
            else
            {
                
                Damage(target.transform);
                Destroy(gameObject);
                return;
            }
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }



    void Damage(Transform enemy)
    {
        
        if (this.gameObject.name == "BulletMachineGun(Clone)")
            damage += GunMachine.increaseDamage;

        else
            damage += Turret.increaseDamage;
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {

            e.TakeDamage(damage);
        }

    }


    void Explode()
    {
        
        GameObject ExpEffect = (GameObject)Instantiate(explosionLight, transform.position, transform.rotation);
        GameObject ParticalEffect = (GameObject)Instantiate(explosionPartical, transform.position, transform.rotation);
        Destroy(ExpEffect, .1f);
        Destroy(ParticalEffect, .5f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                
                Damage(collider.transform);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

    }
}
