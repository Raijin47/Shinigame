using UnityEngine;
using UnityEngine.UI;
public class SelectionCharaButton : MonoBehaviour
{
    [SerializeField] private UpdateDescription panel;
    [SerializeField] private CharacterData data;
    [SerializeField] private int id;
    private Button _button;

    private void Awake() => _button = GetComponent<Button>();

    public void Check()
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
    public void SetChara() => panel.GetData(data, id);
}