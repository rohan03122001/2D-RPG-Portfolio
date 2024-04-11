using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject essentialsPrefab;

    private void Awake()
    {
        var exisitingObject = FindObjectsOfType<EssentialObjects>();
        if(exisitingObject.Length == 0)
        {
            Instantiate(essentialsPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}
