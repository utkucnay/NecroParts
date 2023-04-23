using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    Animator animator;

    void OnEnable()
    {
        float length = GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(0).LongLength;
        Destroy(gameObject, length);
    }
}
