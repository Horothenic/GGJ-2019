using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utilities.UI
{
	[CustomEditor (typeof (ImprovedInput))]
	public class ImprovedInput_Editor : Editor 
	{
		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector ();
		}
	}
}