using UnityEngine;
using UnityEngine.PlayerLoop;

[System.Serializable]
public struct UpdatedData
{
    public bool updated;
    
    public UpdatedData(bool updated)
    {
        this.updated = updated;
    }
}
