using System.Collections.Generic;
using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    public static PlayerKeys Instance;

    private HashSet<string> keys = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddKey(string keyID)
    {
        keys.Add(keyID);
    }

    public bool HasKey(string keyID)
    {
        return keys.Contains(keyID);
    }
}

