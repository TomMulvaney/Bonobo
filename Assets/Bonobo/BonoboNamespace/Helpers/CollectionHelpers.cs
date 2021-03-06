﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Bonobo
{
	public class CollectionHelpers 
	{
	    public static T GetRandom<T>(IList<T> list)
	    {
	        return list [UnityEngine.Random.Range(0, list.Count)];
	    }

	    public static void DestroyObjects<T>(IList<T> list, string message = null)
	    {
	        bool hasDestroyed = false;

	        for (int i = list.Count - 1; i > -1; --i)
	        {
	            Component comp = list[i] as Component;

	            GameObject go = comp != null ? comp.gameObject : list[i] as GameObject;

	            if(go != null)
	            {
	                if(!string.IsNullOrEmpty(message))
	                {
	                    go.SendMessage(message);
	                }
	                else
	                {
	                    MonoBehaviour.Destroy(go);
	                }

	                hasDestroyed = true;
	            }
	        }

	        if (hasDestroyed)
	        {
	            list.Clear();
	        }
	    }

		public static void Shuffle<T>(IList<T> list)
		{
			System.Random rng = new System.Random();
			int n = list.Count;
			while(n > 1)
			{
				--n;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}

	    public static string ConcatList<T>(IList<T> list)
	    {
	        string concat = "";

	        foreach (T t in list) 
	        {
	            concat += String.Format("{0}_", t);
	        }
	        
	        concat = concat.TrimEnd (new char[] { '_' });
	        
	        return concat;
	    }

	    public static bool IsSubset<T>(IList<T> subset, IList<T> superset)
	    {
	        return !subset.Except(superset).Any();
	    }

	    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
	    // Comparisons for sorting Collections

		public static int LeftToRight(MonoBehaviour a, MonoBehaviour b)
		{
	        return LeftToRight(a.transform, b.transform);
		}

		public static int LeftToRight(Transform a, Transform b)
		{
	        return a.position.x.CompareTo(b.position.x);
		}

	    public static int LocalLeftToRight(MonoBehaviour a, MonoBehaviour b)
	    {
	        return LocalLeftToRight(a.transform, b.transform);
	    }

	    public static int LocalLeftToRight(Transform a, Transform b)
	    {
	        return a.localPosition.x.CompareTo(b.localPosition.x);
	    }

	    public static int LocalRightToLeft(MonoBehaviour a, MonoBehaviour b)
	    {
	        return LocalRightToLeft(a.transform, b.transform);
	    }
	    
	    public static int LocalRightToLeft(Transform a, Transform b)
	    {
	        return b.localPosition.x.CompareTo(a.localPosition.x);
	    }

	    public static int TopToBottom(MonoBehaviour a, MonoBehaviour b)
	    {
	        return TopToBottom(a.transform, b.transform);
	    }
	    
	    public static int TopToBottom(Transform a, Transform b)
	    {
	        return b.position.y.CompareTo(a.position.y);
	    }

	    public static int LocalTopToBottom(GameObject a, GameObject b)
	    {
	        return LocalTopToBottom(a.transform, b.transform);
	    }

	    public static int LocalTopToBottom(MonoBehaviour a, MonoBehaviour b)
	    {
	        return LocalTopToBottom(a.transform, b.transform);
	    }

	    public static int LocalTopToBottom(Transform a, Transform b)
	    {
	        return b.localPosition.y.CompareTo(a.localPosition.y);
	    }

	    public static int LocalBottomToTop(GameObject a, GameObject b)
	    {
	        return LocalBottomToTop(a.transform, b.transform);
	    }

	    public static int LocalBottomToTop(MonoBehaviour a, MonoBehaviour b)
	    {
	        return LocalBottomToTop(a.transform, b.transform);
	    }

	    public static int LocalBottomToTop(Transform a, Transform b)
	    {
	        return a.localPosition.y.CompareTo(b.localPosition.y);
	    }

	    public static int LocalLeftToRight_TopToBottom(MonoBehaviour a, MonoBehaviour b)
	    {
	        return LocalLeftToRight_TopToBottom(a.transform, b.transform);
	    }
	    
	    public static int LocalLeftToRight_TopToBottom(Transform a, Transform b)
	    {
	        if (a == null && b == null)
	        {
	            return 0;
	        } 
	        else if (a == null)
	        {
	            return 1;
	        } 
	        else if (b == null)
	        {
	            return -1;
	        } 
	        else
	        {
	            int compare = b.localPosition.y.CompareTo(a.localPosition.y);
	            return compare != 0 ? compare : a.localPosition.x.CompareTo(b.localPosition.x);
	        }
	    }

	    public static int LocalLeftToRight_BottomToTop(MonoBehaviour a, MonoBehaviour b)
	    {
	        return LocalLeftToRight_BottomToTop(a.transform, b.transform);
	    }

	    public static int LocalLeftToRight_BottomToTop(Transform a, Transform b)
	    {
	        if (a == null && b == null)
	        {
	            return 0;
	        } 
	        else if (a == null)
	        {
	            return 1;
	        } 
	        else if (b == null)
	        {
	            return -1;
	        } 
	        else
	        {
	            int compare = a.localPosition.y.CompareTo(b.localPosition.y);
	            return compare != 0 ? compare : a.localPosition.x.CompareTo(b.localPosition.x);
	        }
	    }

	    public static int LocalTopToBottom_LeftToRight(MonoBehaviour a, MonoBehaviour b)
	    {
	        return LocalTopToBottom_LeftToRight(a.transform, b.transform);
	    }

	    public static int LocalTopToBottom_LeftToRight(Transform a, Transform b)
	    {
	        if (a == null && b == null)
	        {
	            return 0;
	        } 
	        else if (a == null)
	        {
	            return 1;
	        } 
	        else if (b == null)
	        {
	            return -1;
	        } 
	        else
	        {
	            int compare = a.localPosition.x.CompareTo(b.localPosition.x);
	            return compare != 0 ? compare : b.localPosition.y.CompareTo(a.localPosition.y);
	        }
	    }

	    public static int CompareName(UnityEngine.Object a, UnityEngine.Object b)
	    {
	        return String.Compare(a.name, b.name);
	    }
	}
}