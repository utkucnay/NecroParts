using DG.Tweening;
using UnityEngine;

public class TextAnim : MonoBehaviour {
    
    private void OnEnable()
    {
        TextAnimate();
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;

    }

    public void TextAnimate()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(2.25f, .4f));
        sequence.Append(transform.DOScale(.4f, .4f));
        sequence.AppendCallback(() => {
            Destroy(gameObject, .1f);
        });
    }
}