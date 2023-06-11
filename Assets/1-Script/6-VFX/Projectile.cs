using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileData
{
    public Vector3 dir;
    public float speed;
    public int damage;
    public float knockbackPower;
}

public class Projectile : MonoBehaviour
{
    ProjectileData projectileData;
    Rigidbody rb;
    public int count;

    LinkedList<GameObject> oldDamagedEnemies;

    private void Awake()
    {
        oldDamagedEnemies = new LinkedList<GameObject>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetProjetileData(in ProjectileData projectileData)
    {
        this.projectileData = projectileData;
    }

    private void Update()
    {
        if (!Utils.InMainCamera(transform.position, 1f))
        {
            gameObject.SetActive(false);
            return;
        }
        CommandStream.AddCommand(new MoveCommandTransform(transform, projectileData.dir * projectileData.speed));
        CommandStream.AddCommand(new RotateProjetileCommand(Vector2.left, projectileData.dir, transform.GetChild(0)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        if (oldDamagedEnemies.Contains(other.gameObject)) return;

        {
            DamageProjectileData damageProjectileData = new DamageProjectileData();
            damageProjectileData.projectilePos = transform.position;
            damageProjectileData.moveDir = projectileData.dir;
            damageProjectileData.knockbackPower = projectileData.knockbackPower;
            other.GetComponentInParent<Unit>().DamageProjectile(projectileData.damage, damageProjectileData);
            oldDamagedEnemies.AddLast(other.gameObject);
        }
        if (--count <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);   
    }
}
