using TMPro;
using UnityEngine;

namespace Fight
{
    public class FightLogger : MonoBehaviour, IElement
    {
        [SerializeField] private TMP_Text text;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void LogText(string data)
        {
            text.text = data;
        }
    }
}