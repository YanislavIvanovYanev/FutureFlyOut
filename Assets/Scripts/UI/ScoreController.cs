using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private int score;

    public void Add(int add)
    {
        score += add;
        text.text = $"Score: {score}";
    }
}
