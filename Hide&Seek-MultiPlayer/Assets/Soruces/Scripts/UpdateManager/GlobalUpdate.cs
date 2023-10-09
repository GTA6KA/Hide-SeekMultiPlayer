using UnityEngine;

public class GlobalUpdate : MonoBehaviour
{
    private void Update()
    {
        for (int i = 0; i < MonoCache._allUpdates.Count; i++) MonoCache._allUpdates[i].Tick();
    }
}
