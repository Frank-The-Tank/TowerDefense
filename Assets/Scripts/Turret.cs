using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour {

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;
    public AudioSource fireSound;

    // Each hit and kill adds to mutationCounter, when threshold for mutation level hit turret mutates
    [Header("Mutation")]
    public float mutationCounter = 0f;
    public int mutationLevel = 0;
    public Text mutationText;
    public Animation mutationAnimation;
    private double mutationThreshold = 100;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowPct = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
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
        } else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (mutationCounter >= mutationThreshold)
        {
            mutationCounter = 0;
            mutationThreshold += mutationThreshold * 1.25;
            mutationLevel += 1;
            Mutate();
        }

		if (target == null)
        {

            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    fireSound.Stop();
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
        } else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

	}

    void Mutate()
    {
        Bullet bullet = bulletPrefab.GetComponent<Bullet>();
        switch (gameObject.name)
        {
            case "StandardTurret_UpgradedBlue(Clone)":
                // TODO
                fireRate += 0.2f;
                bullet.damage += 2;
                break;
            case "StandardTurret_UpgradedGreen(Clone)":
                // TODO
                fireRate += 0.2f;
                bullet.damage += 2;
                break;
            case "StandardTurret_UpgradedOrange(Clone)":
                // TODO
                fireRate += 0.2f;
                bullet.damage += 2;
                break;
            case "StandardTurret_UpgradedPurple(Clone)":
                // TODO
                fireRate += 0.2f;
                bullet.damage += 2;
                break;
            case "StandardTurret_UpgradedRed(Clone)":
                // TODO
                fireRate += 0.2f;
                bullet.damage += 2;
                break;
            case "StandardTurret_UpgradedYellow(Clone)":
                // TODO
                fireRate += 0.2f;
                bullet.damage += 2;
                break;
            default:
                fireRate += 0.1f;
                bullet.damage += 1;
                break;
        }
        mutationText.text = "Mutation";
        mutationAnimation.Play();
    }

    void LockOnTarget ()
    {
        // Target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; // Lerp used to smooth transition from one postion to another
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser ()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        if (!lineRenderer.enabled)
        {
            fireSound.Play();
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        } 

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        // Play fire audio
        fireSound.Play();

        if (bullet != null)
        {
            bullet.Seek(target);
            switch (bullet.name)
            {
                case "BulletBlue(Clone)":
                    mutationCounter += Random.Range(9.75f, 10.25f);
                    break;
                case "BulletGreen(Clone)":
                    mutationCounter += Random.Range(9.75f, 10.25f);
                    break;
                case "BulletOrange(Clone)":
                    mutationCounter += Random.Range(9.75f, 10.25f);
                    break;
                case "BulletPurple(Clone)":
                    mutationCounter += Random.Range(9.75f, 10.25f);
                    break;
                case "BulletRed(Clone)":
                    mutationCounter += Random.Range(9.75f, 10.25f);
                    break;
                case "BulletYellow(Clone)":
                    mutationCounter += Random.Range(13.75f, 14.25f);
                    break;
                default:
                    mutationCounter += Random.Range(4.25f, 7.75f);
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
