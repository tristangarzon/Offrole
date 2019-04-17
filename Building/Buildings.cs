using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : BaseBuilding
{
    private void Update()
    {
        if(Health.Curr > 0)
        {
            UpdateStats();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
