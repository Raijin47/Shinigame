using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPrefab;
    [SerializeField] [Range(0f, 1f)] float chance;

    bool isQuitting = false;

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    public void CheckDrop()
    {
        if(isQuitting) { return; }
        if(Random.value < chance)
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];
            if(toDrop == null)
            {
                Debug.LogWarning("���� ��������� � ������ ���������");
                return;
            }
            SpawnManager.instance.SpawnObject(transform.position, toDrop);
        }
    }
}