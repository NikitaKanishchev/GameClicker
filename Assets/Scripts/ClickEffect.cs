using DG.Tweening;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    public float punchScale = 0.00003f;
    public float punchDuration = 0.005f;
    public Ease punchEase = Ease.OutElastic; 

    public void Poke()
    {
        Vector3 originalScale = transform.localScale;
        
        transform.DOScale(originalScale * (1 + punchScale), punchDuration)
            .SetEase(punchEase)
            .SetLoops(2, LoopType.Yoyo); 
    }
}

