using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MeleeGFXData
{
    public Vector3 targetDir;
    public Vector2 baseDir;
    public float scaleAttackBegin;
    public float scaleAttackEnd;
    public float time;
}

public class MeleeVFX : VFX
{
    [SerializeField] GameObject GFX;
    MeleeGFXData meleeGFXData;
    float timer;

    public void SetGFXData(in MeleeGFXData meleeGFXData)
    {
        this.meleeGFXData = meleeGFXData;
        timer = 0;
    }

    private void Start()
    {
        var angle = Vector2.SignedAngle(meleeGFXData.baseDir, meleeGFXData.targetDir);
        GFX.transform.localEulerAngles += new Vector3(0, 0, angle);
    }

    private void Update()
    {
        if (meleeGFXData.time == 0)
        {
            GFX.transform.localPosition = meleeGFXData.targetDir;
        }
        else if (timer <= meleeGFXData.time)
        {
            GFX.transform.localPosition =
                Utils.Scale(0, meleeGFXData.time, meleeGFXData.scaleAttackBegin, meleeGFXData.scaleAttackEnd, timer)
                    * (meleeGFXData.targetDir);

            GFX.transform.localPosition += new Vector3(0, .5f, -.5f);
            timer += Time.deltaTime;
        }
    }
}