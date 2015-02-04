using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class NGUIHelpers
{
    // TODO: Deprecate all Alpha methods with no hashtable parameter

    public static void Alpha(Component comp, float duration, float value)
    {
        Alpha(comp.transform, duration, value);
    }

    public static void Alpha(GameObject go, float duration, float value)
    {
        Alpha(go.transform, duration, value);
    }

    public static void Alpha(Transform tra, float duration, float value)
    {
        if (tra.GetComponent<UIWidget>() != null)
        {
            TweenAlpha.Begin(tra.gameObject, duration, value);
        }
        else
        {
            for (int i = 0; i < tra.childCount; ++i)
            {
                Alpha(tra.GetChild(i), duration, value);
            }
        }
    }

    public static void Alpha(Component comp, float duration, float value, Hashtable ht)
    {
        AlphaInternal(comp.transform, duration, value, ht);
    }
    
    public static void Alpha(GameObject go, float duration, float value, Hashtable ht)
    {
        AlphaInternal(go.transform, duration, value, ht);
    }

    static void AlphaInternal(Transform tra, float duration, float value, Hashtable ht)
    {
        UIWidget widget = tra.GetComponent<UIWidget>() as UIWidget;
        if (widget!= null)
        {
            TweenAlpha tweener = tra.GetComponent<TweenAlpha>() as TweenAlpha;

            if(tweener != null)
            {
                UnityEngine.Object.Destroy(tweener);
            }

            tweener = tra.gameObject.AddComponent<TweenAlpha>() as TweenAlpha;

            tweener.from = widget.alpha;
            tweener.to = value;
            tweener.duration = duration;

            if(ht != null)
            {
                if(ht.ContainsKey("method") && ht["method"] is UITweener.Method)
                {
                    tweener.method = (UITweener.Method)ht["method"];
                }

                if(ht.ContainsKey("style") && ht["style"] is UITweener.Style)
                {
                    tweener.style = (UITweener.Style)ht["style"];
                }

                if(ht.ContainsKey("onFinishedEventReceiver") && ht["onFinishedEventReceiver"] is MonoBehaviour 
                   && ht.ContainsKey("onFinishedMethodName") && ht["onFinishedMethodName"] is String)
                {
                    tweener.onFinished.Add(new EventDelegate((MonoBehaviour)ht["onFinishedEventReceiver"], (string)ht["onFinishedMethodName"]));
                }
            }

            tweener.Play(); 
        }
        else
        {
            for (int i = 0; i < tra.childCount; ++i)
            {
                AlphaInternal(tra.GetChild(i), duration, value, ht);
            }
        }
    }

    public static void EnableUICams(bool enable, UICamera[] exceptions = null)
    {
        UICamera[] uiCams = UnityEngine.Object.FindObjectsOfType(typeof(UICamera)) as UICamera[];
        foreach (UICamera uiCam in uiCams)
        {
            if(exceptions == null || Array.IndexOf(exceptions, uiCam) != -1)
            {
                uiCam.enabled = enable;
            }
        }
    }

    public static string GetLinkedSpriteName(string spriteName)
    {
        if (spriteName.Length > 0)
        {
            string newNameEnd = spriteName [spriteName.Length - 1] == 'a' ? "b" : "a";
            
            return spriteName.Substring(0, spriteName.Length - 1) + newNameEnd;
        } 
        else
        {
            return "";
        }
    }

    public static float GetScaledLabelWidth(UILabel label, string text)
    {
        string originalLabelText = label.text;
        label.text = text;

        float width = label.printedSize.x * label.transform.localScale.x;

        label.text = originalLabelText;

        return width;
    }

    public static float GetScaledLabelHeight(UILabel label, string text)
    {
        string originalLabelText = label.text;
        label.text = text;
        
        float height = label.printedSize.y * label.transform.localScale.y;
        
        label.text = originalLabelText;
        
        return height;
    }

    public static Vector3 GetLabelSize3(UILabel label)
    {
        Vector2 size = label.printedSize;
        size.x *= label.transform.localScale.x;
        size.y *= label.transform.localScale.y;
        
        return new Vector3(size.x, size.y, label.transform.localScale.z);
    }

    public static int GetNumLines(UIGrid grid)
    {
        int numChildren = grid.GetChildList().Count;
        int numRows = numChildren / grid.maxPerLine;

        if (numChildren % grid.maxPerLine > 0)
        {
            ++numRows;
        }

        return numRows;
    }
}
