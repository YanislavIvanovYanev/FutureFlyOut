using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesController : MonoBehaviour
{
    [SerializeField] private List<HeartController> hearts;

    public void LoseLife()
    {
        for(int i = hearts.Count - 1; i >= 0; i--)
            if(hearts[i].isFull)
            {
                hearts[i].SetHeart(false);
                break;
            }

        if(!hearts.TrueForAll(h => !h.isFull))
            return;
        //game over
        SceneManager.LoadScene("Battle1");
    }

    public void RegainAllLives(){ for(int i = 0; i < hearts.Count; i++) if(!hearts[i].isFull) hearts[i].SetHeart(true); }
}
