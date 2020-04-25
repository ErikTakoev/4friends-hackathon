using System.Collections;
using System.Collections.Generic;
using Fight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBarViewController : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text hpText;

    public void UpdateView(Attacker attacker)
    {
        hpBar.fillAmount = attacker.Hp / attacker.CurrentHp;
        hpText.text = $"{attacker.CurrentHp} / {attacker.Hp}";
    }
}
