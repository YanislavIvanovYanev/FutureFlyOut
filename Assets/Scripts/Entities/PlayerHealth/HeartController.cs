using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart, emptyHeart;
    [SerializeField] private Image image;

    [HideInInspector] public bool isFull = true;

    public void UpdateHeart() => image.sprite = isFull ? fullHeart : emptyHeart;

    public void SetHeart(bool _isFull)
    {
        isFull = _isFull;
        UpdateHeart();
    }
}
