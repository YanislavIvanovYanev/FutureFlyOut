using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private TextMeshProUGUI number;

    private float hp = maxHp;
    private const int maxHp = 180; //in seconds
    private const float secondPhaseStart = maxHp - maxHp / 6f, thirdPhaseStart = maxHp / 3f, passiveDmg = .1f;

    public void StartBoss()
    {
        InvokeRepeating(nameof(PassiveDamage), passiveDmg, passiveDmg);
        InvokeRepeating(nameof(UpdateNumber), 0f, 1f);
    }

    private void PassiveDamage() => Damage(passiveDmg);

    private void Damage(float amount)
    {
        hp -= amount;
        bar.value = hp / maxHp;
    }

    public void HitBoss(float amount)
    {
        Damage(amount);
        UpdateNumber();
        if(hp <= 0f) SceneManager.LoadScene("Victory");
        else if(World.Boss.phase == 2 && hp <= thirdPhaseStart) World.Boss.SwitchPhase(true);
        else if(World.Boss.phase == 1 && hp <= secondPhaseStart) World.Boss.SwitchPhase(false);
    }

    private string InMinutes(int seconds) => $"{seconds / 60}:{seconds % 60:D2}";
    private void UpdateNumber() => number.text = $"{InMinutes((int)hp)} / {InMinutes(maxHp)}";
}
