using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] protected bool autoCloseVFX = true;
    void OnEnable()
    {
        if(autoCloseVFX) {
        float length = GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(0).LongLength;
        Destroy(gameObject, length);
        }
    }
}
