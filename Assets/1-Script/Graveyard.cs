using UnityEngine;

public class Graveyard : Unit
{
    [SerializeField] private float thresholdDistance = 30;

    private void Update()
    {
        if(Vector2.Distance(Player.s_Instance.transform.position, transform.position) > thresholdDistance)
        {
            transform.position = SpawnManager.s_Instance.PosOutCamera();
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