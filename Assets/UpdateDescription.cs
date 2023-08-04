using UnityEngine;

public class UpdateDescription : MonoBehaviour
{
    [SerializeField] private PanelChara panelChara;
    [SerializeField] private CharacterData data;
    private int id;
    private Animator anim;
    string splashIn = "Splash In";
    string splashOut = "Splash Out";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void GetData(CharacterData getData, int getId)
    {
        if(id == getId) { return; }

        anim.Play(splashIn);
        id = getId;
        data = getData;
    }
    public void SetData()
    {
        panelChara.UpdateUI(data, id);
    }
    public void AnimEnd()
    {
        anim.Play(splashOut);
    }
}