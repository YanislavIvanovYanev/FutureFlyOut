using UnityEngine;
using UnityEngine.UI;

public class PotionController : MonoBehaviour
{
    [SerializeField] private Sprite fullPotion, emptyPotion;
    [SerializeField] private Image image;

    private int charges = 1;
    private const int scoreLossOnPotion = -30; //seconds of manually damaging the boss

    private void Start() => World.Input.heal.OnDown += Heal;

    public void Heal(bool value)
    {
        if(!value || World.Input.heal.Was || charges <= 0)
            return;
        World.Lives.RegainAllLives();
        image.sprite = emptyPotion;
        charges--;
        World.Score.Add(BossBar.scoreMultOnDamage * scoreLossOnPotion);
    }
}
