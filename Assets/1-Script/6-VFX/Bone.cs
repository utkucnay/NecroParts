using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    [SerializeField] float waitTime;
    float timer;
    void Update()
    {
        if (timer > waitTime)
        {
            var vfx = Instantiate(AIManager.s_Instance.puffVFX);
            vfx.transform.position = transform.position;
            vfx.SetActive(true);

            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }

    private void OnDestroy()
    {
        if(Random.value < .7f)
            SoulManager.s_Instance.SoulSpawn(transform.position);
    }
}
