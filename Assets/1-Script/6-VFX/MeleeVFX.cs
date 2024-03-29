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
    [SerializeField] bool rotate = true; 
    [SerializeField] bool followPlayer = false;

    Animator anim;

    public void SetGFXData(in MeleeGFXData meleeGFXData)
    {
        this.meleeGFXData = meleeGFXData;
        anim.SetFloat("_TimeScale", 1 / meleeGFXData.time);    
        Destroy(gameObject, meleeGFXData.time);
        timer = 0;
    }
    
    private void Awake() {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if(rotate){
            var angle = Vector2.SignedAngle(meleeGFXData.baseDir, meleeGFXData.targetDir);
            GFX.transform.localEulerAngles += new Vector3(0, 0, angle);
        }    
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

        if(followPlayer) 
            transform.position = Player.s_Instance.transform.position;
    }
}