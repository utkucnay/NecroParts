using UnityEngine;

public class Pot : CollectableMono
{
    public override void Collect()
    {
        Player.s_Instance.AddHealth(20);
    }
}