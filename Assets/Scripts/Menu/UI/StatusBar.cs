using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    //[SerializeField] Transform bar;
    [SerializeField] Image hpBar;

    public void SetState(int current, int max)
    {
        //float state = (float)current;
        //state /= max;
        //if (state < 0f) { state = 0f; }
        hpBar.fillAmount = Mathf.InverseLerp(0, max, current);


        //bar.transform.localScale = new Vector3(state, 1f, 1f);
    }
}