using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    Animator animator;

    void OnEnable()
    {
        Destroy(gameObject, GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(0).Length);
    }
}
