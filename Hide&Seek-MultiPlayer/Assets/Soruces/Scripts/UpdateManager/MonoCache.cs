using System.Collections.Generic;
using UnityEngine;

public class MonoCache : MonoBehaviour
{
    public static List<MonoCache> _allUpdates = new List<MonoCache>(10001);

    private void OnEnable() => _allUpdates.Add(this);
    private void OnDisable() => _allUpdates.Remove(this);
    private void OnDestroy() => _allUpdates.Remove(this);

    public void Tick() => OnTick();
    public virtual void OnTick() { }

}
