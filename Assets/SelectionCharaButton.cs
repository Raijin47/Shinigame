using UnityEngine;
using UnityEngine.UI;

public class SelectionCharaButton : MonoBehaviour
{
    [SerializeField] private CharacterData data;
    [SerializeField] private GameObject lockImage;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        if (data.Level == 0)
        {
            lockImage.SetActive(true);
            _button.interactable = false;
        }
        else
        {
            lockImage.SetActive(false);
            _button.interactable = true;
        }
    }
}