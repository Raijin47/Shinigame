using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCooldownUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _fillImage;
    private Coroutine _updateCooldownCoroutine;
    private WeaponBase _weapon;
    public void Activate(WeaponBase weapon, WeaponData data)
    {
        _icon.sprite = data.icon;
        _weapon = weapon;
        if(_updateCooldownCoroutine != null)
        {
            StopCoroutine(_updateCooldownCoroutine);
            _updateCooldownCoroutine = null;
        }
        _updateCooldownCoroutine = StartCoroutine(UpdateCooldown());
    }

    private IEnumerator UpdateCooldown()
    {
        while(true)
        {
            _fillImage.fillAmount = _weapon.timer / _weapon.curTimer;
            yield return null;
        }
    }
}