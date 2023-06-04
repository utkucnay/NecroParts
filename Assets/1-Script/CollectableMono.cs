using DG.Tweening;
using UnityEngine;

public abstract class CollectableMono : MonoBehaviour 
{
     bool start = false;
    bool followPlayer = false;


    private void OnTriggerEnter(Collider other)
    {
        if (start) return;
        if (other.CompareTag("Player"))
        {
            CollectSoul(other.transform.root.transform.position);
        }
    }

    public void CollectSoul(Vector3 pos)
    {
        start = true;

        MoveAwayFromPlayer(pos);
    }

    public void MoveAwayFromPlayer(Vector3 pos)
    {
        Vector2 dir = Vector3.Normalize(transform.position - pos);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + GameManager.Translate3D(dir) * 1.5f, .3f));
        sequence.AppendCallback(() =>
        {
            followPlayer = true;
        });
    }

    private void Update()
    {
        if (!followPlayer) return;

        if (IsCacthUpPlayer())
        {
            CacthUpPlayer();
        }
        else
        {
            MoveToPlayer();
        }
    }

    public bool IsCacthUpPlayer()
    {
        return Vector3.Distance(Player.s_Instance.transform.position, transform.position) <= .75f;
    }
    public void CacthUpPlayer()
    {
        Destroy(gameObject);
        Collect();
    }
    public void MoveToPlayer()
    {
        Vector3 dir = Vector3.Normalize(Player.s_Instance.transform.position - transform.position);
        transform.position += 10f * Time.deltaTime * dir;
    }

    public abstract void Collect();
}