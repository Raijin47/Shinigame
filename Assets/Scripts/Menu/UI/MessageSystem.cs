using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
    [SerializeField] private GameObject damageMessage;

    List<TextMeshPro> messagePool;
    [SerializeField] private int objCount;
    private int count;

    private void Start()
    {
        messagePool = new List<TextMeshPro>();
        for(int i = 0; i < objCount; i++)
        {
            Populate();
        }
    }
    public void Populate()
    {
        GameObject go = Instantiate(damageMessage, transform);
        messagePool.Add(go.GetComponent<TextMeshPro>());
        go.SetActive(false);
    }
    public void PostMessage(string text, Vector2 worldPosition)
    {
        messagePool[count].gameObject.SetActive(true);
        messagePool[count].transform.position = worldPosition + offset;
        messagePool[count].text = text;

        count += 1;
        if (count >= objCount)
        {
            count = 0;
        }
    }
}