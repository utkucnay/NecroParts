using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : Bar<LevelBar>
{
    private void Update()
    {
        UpdateSlider(SoulManager.s_Instance.soul, SoulManager.s_Instance.ReqSoul);
    }
}
