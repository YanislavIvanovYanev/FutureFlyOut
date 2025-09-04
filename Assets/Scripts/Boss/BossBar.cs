using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private TextMeshProUGUI number;

    public static int scoreMultOnDamage;

    private float hp = maxHp;
    private const int initialScoreMult = 2, maxHp = 300; //in seconds
    private const float secondPhaseStart = maxHp - maxHp / 6f, thirdPhaseStart = maxHp / 3f, passiveDmg = .1f;

    private string InMinutes(int seconds) => $"{seconds / 60}:{seconds % 60:D2}";
    private void UpdateNumber() => number.text = $"{InMinutes((int)hp)} / {InMinutes(maxHp)}";
    private void EverySecondUpdate()
    {
        World.Score.Add(1);
        UpdateNumber();
    }
    
    public void StartBoss()
    {
        scoreMultOnDamage = initialScoreMult;
        InvokeRepeating(nameof(EverySecondUpdate), 0f, 1f);
        InvokeRepeating(nameof(PassiveDamage), passiveDmg, passiveDmg);
    }

    private void PassiveDamage() => VirtualDamage(passiveDmg);

    private void VirtualDamage(float amount)
    {
        hp -= amount;
        bar.value = hp / maxHp;
    }

    public void DamageBoss(int amount)
    {
        VirtualDamage(amount);
        World.Score.Add(amount * scoreMultOnDamage);
        UpdateNumber();
        if(hp <= 0f) SceneManager.LoadScene("Victory");
        else if(World.Boss.phase == 2 && hp <= thirdPhaseStart) World.Boss.SwitchPhase(true);
        else if(World.Boss.phase == 1 && hp <= secondPhaseStart) World.Boss.SwitchPhase(false);
    }    
}
