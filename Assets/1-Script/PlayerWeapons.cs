using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerWeapons : MonoBehaviour
{
    LinkedList<Weapon> weapons;

    [SerializeField] GameObject thunderWeaponObj;
    [SerializeField] GameObject schyteWeaponObj;

    private void Awake()
    {
        weapons = new();
        AddWeapon();
    }

    private void Update()
    {
        foreach (var weapon in weapons)
        {
            weapon.Update();
        }
    }

    public void AddWeapon()
    {
        weapons.AddLast(new ThunderWeapon(3.25f, 15, 4, transform, thunderWeaponObj));
        weapons.AddLast(new ScytheWeapon(1.5f, 10, 1, transform, schyteWeaponObj));
    }
}
 
public abstract class Weapon
{
    public float attackSpeed;
    public int damage;
    public GameObject prefab;
    public int count;
    public Transform transform;

    float timer;

    public Weapon(float attackSpeed, int damage, int count, Transform transform, GameObject prefab)
    {
        timer = 0;

        this.count = count;
        this.attackSpeed = attackSpeed;
        this.damage = damage;
        this.prefab = prefab;
        this.transform = transform;
    }

    public void Update()
    {
        if (timer > attackSpeed)
        {
            Debug.Log("Weapon");
            WeaponAttack();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    protected abstract void WeaponAttack();
}

class ThunderWeapon : Weapon
{
    public ThunderWeapon(float attackSpeed, int damage, int count, Transform transform, GameObject prefab) : base(attackSpeed, damage, count, transform, prefab)
    {
    }

    protected override void WeaponAttack()
    {
        var enemies = AIManager.s_Instance.GetCloseEnemies(10, Player.s_Instance.transform.position);
        if (enemies.Length == 0) return;
        var enemyCount = enemies.Length;

        var rand = new System.Random();
        var randomIndexes = Enumerable.Range(0, enemies.Length).OrderBy(i => rand.Next()).ToArray();

        for (int i = 0; i < count; i++)
        {
            if (enemyCount <= i) break;
            var enemy = enemies[randomIndexes[i]];
            var spawnGO = GameObject.Instantiate(prefab);
            spawnGO.transform.position = enemy.transform.position;
            spawnGO.gameObject.SetActive(true);
            enemy.DamageMelee(damage, new DamageMeleeData());
        }
    }
}

class MagicWandWeapon : Weapon
{
    public MagicWandWeapon(float attackSpeed, int damage, int count, Transform transform, GameObject prefab) : base(attackSpeed, damage, count, transform, prefab)
    {
    }

    protected override void WeaponAttack()
    {
        var enemies = AIManager.s_Instance.GetEnemies();
        var closeEnemy = enemies.OrderBy(enemy => Vector2.Distance(enemy.transform.position, Player.s_Instance.transform.position)).ToArray()[0];

    }

    IEnumerator DelayWeaponAttack(Vector2 dir)
    {
        var spawnGO = GameObject.Instantiate(prefab);
        yield return new WaitForSeconds(2);
    }
}

class ScytheWeapon : Weapon
{
    public ScytheWeapon(float attackSpeed, int damage, int count, Transform transform, GameObject prefab) : base(attackSpeed, damage, count, transform, prefab)
    {
    }

    protected override void WeaponAttack()
    {
        var go = GameObject.Instantiate(prefab, Player.s_Instance.transform.position, Quaternion.identity);
        var aSlash = go.GetComponent<AttackSlash>();
        aSlash.damage = damage;
        aSlash.SetDamageMeleeData(new DamageMeleeData() { meleePos = transform.position, knockbackPower = .5f });
        
        go.GetComponent<MeleeVFX>().SetGFXData(new MeleeGFXData() { 
            targetDir = Player.s_Instance.Right ? Vector3.right : Vector3.left, 
            baseDir =  Vector2.left, 
            scaleAttackBegin = 0, 
            scaleAttackEnd = 0, 
            time = .75f
        });
    }
}