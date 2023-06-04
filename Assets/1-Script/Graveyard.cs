using UnityEngine;

public class Graveyard : Unit
{
    [SerializeField] private float thresholdDistance = 30;
    float damageTimeBegin = -1, damageTimeEnd = -1;

    void Start()
    {
        material.SetTexture("_WhiteTexture", Texture2D.whiteTexture);
    }

    private void Update()
    {
        if(Vector2.Distance(Player.s_Instance.transform.position, transform.position) > thresholdDistance)
        {
            transform.position = SpawnManager.s_Instance.PosOutCamera();
        }
        OnDamage();
    }

    protected override void Damage(float damage)
    {
        base.Damage(damage);

        if(damageTimeEnd > Time.time)
            damageTimeBegin = Time.time;
        
        damageTimeEnd = Time.time + 1.25f;
    }

    void OnDamage(){
        if (damageTimeEnd > Time.time)
        {
            material.SetFloat("_LerpDamage", Mathf.Clamp(Utils.Scale(1.25f, 1.15f, 0, .7f, damageTimeBegin - Time.time), 0, .7f));
        }
        else
        {
            material.SetFloat("_LerpDamage", Mathf.Clamp(Utils.Scale(0f, .1f, 0.7f, 0, Time.time - damageTimeEnd), 0, .7f));
        }
    }

    protected override void Death()
    {
        Instantiate(AIManager.s_Instance.PotPrefab, transform.position, Quaternion.identity);
        transform.position = SpawnManager.s_Instance.PosOutCamera();
        health = maxHealth;
        death = false; 
   }
}