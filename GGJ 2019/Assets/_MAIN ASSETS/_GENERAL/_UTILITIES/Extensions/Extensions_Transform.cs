using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public static class Extensions_Transform 
{
    public static void DisableAllColliders (this Transform transform)
    {
        foreach (Transform t in transform)
        {
            Collider col = t.GetComponent <Collider> ();

            if (col != null)
                col.enabled = false;
        }
    }
}
