using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : Singleton<SoulManager>
{
    [SerializeField] int reqSoul;
    public int level;
    public int soul;
    private int soulOverflow;
    public float reqSoulMultipler;

    public int ReqSoul { get { return (int)(reqSoul + level * reqSoulMultipler); } }         

    public GameObject soulObj;

    public override void Awake()
    {
        level = 0;
        base.Awake();

        EventManager.AddEventAction("Reset Game", ResetSoulAndLevel);
        EventManager.AddEventAction("Close Levelup Game", AfterLevelup);
    }

    void ResetSoulAndLevel()
    {
        soul = 0;
        level = 0;
    }

    public void AddSoul()
    {
        if(GameManager.s_Instance.GameState != GameState.LevelUp)
            soul++;
        else
            soulOverflow++;
        if (CheckSoulForLevelUp())
        {
            LevelUp();
        }
    }

    private void AfterLevelup()
    {
        soul = soulOverflow;
        soulOverflow = 0;
        level++;
    }

    bool CheckSoulForLevelUp()
    {
        return soul >= ReqSoul;
    }

    void LevelUp()
    {
        EventManager.InvokeEvent("Levelup Game");
    }

    public void SoulSpawn(Vector3 pos)
    {
        var soulOb = Instantiate(soulObj);
        soulOb.transform.position = pos;
    }
}
