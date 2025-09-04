using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesController : MonoBehaviour
{
    [SerializeField] private List<HeartController> hearts;

    private const int scoreLossOnHit = -10; //seconds of manually damaging the boss

    public void LoseLife()
    {
        if(World.Player.invincibility > 0)
            return;
        World.Player.BecomeInvincible();
        World.Score.Add(BossBar.scoreMultOnDamage * scoreLossOnHit);
        for(int i = hearts.Count - 1; i >= 0; i--)
            if(hearts[i].isFull)
            {
                hearts[i].SetHeart(false);
                break;
            }

        if(!hearts.TrueForAll(h => !h.isFull))
            return;
        //game over
        SceneManager.LoadScene("GameOver");
    }

    public void RegainAllLives(){ for(int i = 0; i < hearts.Count; i++) if(!hearts[i].isFull) hearts[i].SetHeart(true); }
}
