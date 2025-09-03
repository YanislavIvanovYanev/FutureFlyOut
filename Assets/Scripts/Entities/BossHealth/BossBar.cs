using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private TextMeshProUGUI number;

    private float hp = maxHp;
    private const int maxHp = 300, secondPhaseStart = 250, thirdPhaseStart = 100, passiveDmg = 1; //in seconds

    public void StartBoss() => InvokeRepeating(nameof(PassiveDamage), passiveDmg, passiveDmg);
    
    public void PassiveDamage() => Damage(passiveDmg);

    public void Damage(float amount)
    {
        hp -= amount;
        UpdateBar();
        if(hp <= 0f) SceneManager.LoadScene("Battle");
        else if(hp <= thirdPhaseStart) ThirdPhase();
        else if(hp <= secondPhaseStart) SecondPhase();
    }

    private void UpdateBar()
    {
        bar.value = hp / maxHp;
        number.text = $"{(int)(hp/60)}:{(int)(hp%60):D2} / 5:00";
    }

    private void SecondPhase()
    {
        World.Boss.SwitchPhase(false);
    }

    private void ThirdPhase()
    {
        World.Boss.SwitchPhase(true);
    }
}
