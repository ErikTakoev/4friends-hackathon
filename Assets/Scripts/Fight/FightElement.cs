using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fight
{
    [System.Serializable]
    public class FightElement : IElement
    {
        [SerializeField] private Image image;
        [SerializeField] private HpBarViewController hpBarView;
        [SerializeField] private TMP_Text name;
        
        public void Show()
        {
            image.gameObject.SetActive(true);   
            hpBarView.gameObject.SetActive(true);
        }

        public void Hide()
        {
            image.gameObject.SetActive(false);
            hpBarView.gameObject.SetActive(false);
        }

        public void UpdateView(Attacker attacker)
        {
            name.text = attacker.Name;
            
            hpBarView.UpdateView(attacker);
        }
    }
}