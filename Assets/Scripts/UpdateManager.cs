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
        if (isplaying)
        {
            var count = updateables.Count;
            for (int i = 0; i < count; i++)
            {
                if (updateables[i] != null && updateables[i].isActiveAndEnabled)
                {
                    updateables[i].CustomUpdate();
                }
                else
                {
                    updateables.RemoveAt(i);

                    count = updateables.Count;
                }

            }
        }
    }
}
