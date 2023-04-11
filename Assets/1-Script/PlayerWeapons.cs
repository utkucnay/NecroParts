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
        weapons.AddLast(new ThunderWeapon(2, 10, 3, thunderWeaponObj));
    }
}
 
public abstract class Weapon
{
    public float attackSpeed;
    public float damage;
    public GameObject prefab;
    public int count;

    float timer;

    public Weapon(float attackSpeed, float damage, int count, GameObject prefab)
    {
        timer = 0;

        this.count = count;
        this.attackSpeed = attackSpeed;
        this.damage = damage;
        this.prefab = prefab;
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
    public ThunderWeapon(float attackSpeed, float damage, int count, GameObject prefab) : base(attackSpeed, damage, count, prefab)
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
    public MagicWandWeapon(float attackSpeed, float damage, int count, GameObject prefab) : base(attackSpeed, damage, count, prefab)
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