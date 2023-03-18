using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PrincessAdventure
{
	public class GlobalUtils : MonoBehaviour
	{
		public static void DestroyChildren(GameObject go)
		{
			List<GameObject> children = new List<GameObject>();
			foreach (Transform tran in go.transform)
			{      
				children.Add(tran.gameObject); 
			}
			children.ForEach(child => GameObject.Destroy(child));  
		}
	}
}
