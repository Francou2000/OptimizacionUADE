using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    [SerializeField] public List<IUpdateable> updateables = new List<IUpdateable>();
    [SerializeField] int a;
    public bool isplaying;

    void Start()
    {
        isplaying = true;
    }


    void Update()
    {
        if (!isplaying) return;

        for (int i = updateables.Count - 1; i >= 0; i--)
        {
            var updateable = updateables[i];
            if (updateable != null && updateable.isActiveAndEnabled)
            {
                updateable.CustomUpdate();
            }
            else
            {
                updateables.RemoveAt(i);
            }
        }
    }
}
