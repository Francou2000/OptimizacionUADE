using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    [SerializeField] public List<IUpdateable> updateables = new List<IUpdateable>();
    public bool isplaying;

    void Start()
    {
        isplaying = true;
        //updateables = FindObjectsByType<IUpdateable>(FindObjectsSortMode.None).ToList();
    }

    public void AddUpdateable(IUpdateable updateable)
    {
        updateables.Add(updateable);
    }

    public void RemoveUpdateable(IUpdateable updateable)
    {
        if (updateable != null) updateables.Remove(updateable);
    }

    void Update()
    {
        if (!isplaying) return;

        var count = updateables.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            var updateable = updateables[i];
            if (updateable != null && updateable.isActiveAndEnabled)
            {
                updateable.CustomUpdate();
            }
        }
    }
}
