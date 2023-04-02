using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : Singleton<SoulManager>
{
    [SerializeField] int reqSoul;
    public int level;
    public int soul;
    public float reqSoulMultipler;

    public GameObject soulObj;

    public GameObject skeleton;

    public override void Awake()
    {
        level = 0;
        base.Awake();
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
        return soul >= reqSoul + level * reqSoulMultipler;
    }

    void LevelUp()
    {
        //EventManager.InvokeEvent("Pause Game");
        //UIManager.s_Instance.SetUIScene("LevelUpHUD");
        SpawnManager.s_Instance.SpawnInCamera(skeleton);
        soul = 0;
    }

    public void SoulSpawn(Vector3 pos)
    {
        var soulOb = Instantiate(soulObj);
        soulOb.transform.position = pos;
    }
}
