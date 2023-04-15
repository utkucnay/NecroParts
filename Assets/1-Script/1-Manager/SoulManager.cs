using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : Singleton<SoulManager>
{
    [SerializeField] int reqSoul;
    public int level;
    public int soul;
    public float reqSoulMultipler;

    public int ReqSoul { get { return (int)(reqSoul + level * reqSoulMultipler); } }         

    public GameObject soulObj;

    public override void Awake()
    {
        level = 0;
        base.Awake();

        EventManager.AddEventAction("Reset Game", ResetSoulAndLevel);
    }

    void ResetSoulAndLevel()
    {
        soul = 0;
        level = 0;
    }

    public void AddSoul()
    {
        soul++;

        if (CheckSoulForLevelUp())
        {
            LevelUp();
        }
    }

    bool CheckSoulForLevelUp()
    {
        return soul >= ReqSoul;
    }

    void LevelUp()
    {
        EventManager.InvokeEvent("Levelup Game");
        level++;
        soul = 0;
    }

    public void SoulSpawn(Vector3 pos)
    {
        var soulOb = Instantiate(soulObj);
        soulOb.transform.position = pos;
    }
}
