using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
    }

    void OnEnable()
    {
        Destroy(gameObject, GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(0).Length);
    }
}
