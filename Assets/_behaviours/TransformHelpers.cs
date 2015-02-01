using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TransformHelpers 
{
    public static void DestroyChildren(Transform parent, string offMessage = "")
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i > -1; --i)
        {
            if(string.IsNullOrEmpty(offMessage))
            {
                Object.Destroy(parent.GetChild(i).gameObject);
            }
            else
            {
                parent.GetChild(i).SendMessage(offMessage);
            }
        }
    }

    public static void EnableCollider2DRecursive(Transform tra, bool enable)
    {
        if (tra.collider2D != null)
        {
            tra.collider2D.enabled = enable;
        }

        for (int i = 0; i < tra.childCount; ++i)
        {
            EnableCollider2DRecursive(tra.GetChild(i), enable);
        }
    }

    public static Transform CreateSubParent(Transform parent, Transform[] excludedList = null)
    {
        int childCount = parent.childCount;
        
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < childCount; ++i) 
        {
            if(excludedList == null || System.Array.IndexOf(excludedList, parent.GetChild(i)) == -1)
            {
                children.Add(parent.GetChild(i));
            }
        }
        
        Transform subParent = new GameObject ("SubParent").transform;

        subParent.parent = parent;
        subParent.localPosition = Vector3.zero;
        subParent.localScale = Vector3.one;
        subParent.localRotation = Quaternion.identity;
        
        foreach (Transform child in children) 
        {
            child.parent = subParent;
        }
        
        return subParent;
    }
}
