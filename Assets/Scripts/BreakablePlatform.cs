using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : Platform
{
    protected override void AddForceUp(Collision2D collision)
    {
        base.AddForceUp(collision);
        gameObject.SetActive(false);

    }
}
