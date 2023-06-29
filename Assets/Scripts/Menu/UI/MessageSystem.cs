using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CartoonFX;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;
    [SerializeField] private CFXR_ParticleText[] message;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [SerializeField] private GameObject damageMessage;
    List<TextMeshPro> messagePool;
    //private int objectCount = 10;
    private int count;

    //private void Start()
    //{
    //    messagePool = new List<TextMeshPro>();
    //    for(int i = 0; i < objectCount; i++)
    //    {
    //        //Populate();
    //    }
    //}
    public void Populate()
    {
        GameObject go = Instantiate(damageMessage, transform);
        messagePool.Add(go.GetComponent<TextMeshPro>());
        go.SetActive(false);
    }
    public void PostMessage(string text, Vector2 worldPosition)
    {
        message[count].transform.position = worldPosition;
        message[count].UpdateText(text);
        message[count].Play();

        count += 1;
        if (count >= message.Length)
        {
            count = 0;
        }
    }

    //public void PostMessage(string text, Vector2 worldPosition)
    //{
    //    messagePool[count].gameObject.SetActive(true);
    //    messagePool[count].transform.position = worldPosition;
    //    messagePool[count].text = text;
    //    count += 1;
    //    if(count >= objectCount)
    //    {
    //        count = 0;
    //    }
    //}
}