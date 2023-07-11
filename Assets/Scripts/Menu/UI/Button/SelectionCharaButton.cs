using UnityEngine;
using UnityEngine.UI;
public class SelectionCharaButton : MonoBehaviour
{
    [SerializeField] private CharacterData data;
    private Button _button;

    private void Awake() => _button = GetComponent<Button>();
    private void OnEnable()
    {
        if (data.Level == 0)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }
}