using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CartoonFX;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;
    [SerializeField] private CFXR_ParticleText[] message;
    [SerializeField]private Vector2 offset;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [SerializeField] private GameObject damageMessage;
    List<TextMeshPro> messagePool;
    private int count;

    public void Populate()
    {
        GameObject go = Instantiate(damageMessage, transform);
        messagePool.Add(go.GetComponent<TextMeshPro>());
        go.SetActive(false);
    }
    public void PostMessage(string text, Vector2 worldPosition)
    {
        message[count].transform.position = worldPosition + offset;
        message[count].UpdateText(text);
        message[count].Play();

        count += 1;
        if (count >= message.Length)
        {
            count = 0;
        }
    }
}