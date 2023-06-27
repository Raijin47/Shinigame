using UnityEngine;
using TMPro;

namespace Assets.SimpleLocalization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedDynamic : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private string localizeKey;
        public void Localize(string key)
        {
            text.text = LocalizationManager.Localize(key);
            localizeKey = key;
        }
        private void OnEnable()
        {
            if(localizeKey != null)
                text.text = LocalizationManager.Localize(localizeKey);
        }
    }
}