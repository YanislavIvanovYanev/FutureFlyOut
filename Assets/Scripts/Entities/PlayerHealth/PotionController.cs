using UnityEngine;
using UnityEngine.UI;

public class PotionController : MonoBehaviour
{
    [SerializeField] private Sprite fullPotion, emptyPotion;
    [SerializeField] private Image image;

    private int charges = 1;

    private void Start() => World.Input.heal.OnDown += Heal;

    public void Heal(bool value)
    {
        if(!value || World.Input.heal.Was || charges <= 0)
            return;
        World.Lives.RegainAllLives();
        image.sprite = emptyPotion;
        charges--;
    }


}
