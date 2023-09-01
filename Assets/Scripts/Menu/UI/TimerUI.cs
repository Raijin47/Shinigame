using UnityEngine;
using TMPro;
using Assets.SimpleLocalization;

public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private LocalizedDynamic _localized;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _localized = GetComponent<LocalizedDynamic>();
    }

    public void UpdateTime(float time)
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);

        _text.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void SetName(string name) => _localized.Localize(name);
}