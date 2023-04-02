using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.Normalize(Player.s_Instance.transform.position - transform.position) * 3;

            if (Vector3.Distance(transform.position, Player.s_Instance.transform.position) <= .6f)
            {
                SoulManager.s_Instance.AddSoul();
                Destroy(gameObject);
            }
        }
    }
}
