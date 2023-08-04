using UnityEngine;
using UnityEngine.UI;
public class SelectionCharaButton : MonoBehaviour
{
    [SerializeField] private UpdateDescription panel;
    [SerializeField] private CharacterData data;
    [SerializeField] private int id;
    private Button _button;
    Material material;
    int innerPropertyID;
    float lineValue;
    private void Awake() => _button = GetComponent<Button>();
    private void Start()
    {
        material = GetComponent<Image>().material;
        innerPropertyID = Shader.PropertyToID("_InnerOutlineFade");
    }
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

    public void SelectedButton()
    {
        lineValue = 1;
        material.SetFloat(innerPropertyID, lineValue);
    }
    public void DeselectButton()
    {
        lineValue = 0;
        material.SetFloat(innerPropertyID, lineValue);
    }
}