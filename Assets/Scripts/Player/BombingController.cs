using UnityEngine;
using UnityEngine.UI;

public class BombingController : MonoBehaviour
{
    [SerializeField] private Sprite fullBomb, emptyBomb;
    [SerializeField] private Image image;
    [SerializeField] private GameObject bombPrefab;

    private int charges = 1;
    private const int scoreLossOnBomb = -30; //seconds of manually damaging the boss, bomb boss hit = + effect on score (10dmg + impact)

    private void Start() => World.Input.bomb.OnDown += Bomb;

    public void Bomb(bool value)
    {
        if(!value || World.Input.bomb.Was || charges <= 0)
            return;
        image.sprite = emptyBomb;
        charges--;
        Instantiate(bombPrefab, World.Player.laserRenderer.transform.position, Quaternion.identity).GetComponent<BombController>().Set();
        World.Score.Add(BossBar.scoreMultOnDamage * scoreLossOnBomb);
    }
}
