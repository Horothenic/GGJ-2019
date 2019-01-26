using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Utilities.Cameras
{
	[CustomEditor (typeof (OrtographicMobileCamera))]
	public class Editor_OrtographicMobileCamera : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			OrtographicMobileCamera myScript = (OrtographicMobileCamera) target;

			if (GUILayout.Button("Adjust Ortographic Camera"))
			{
				myScript.AdjustOrtographicCameraForMobile ();
			}
		}
	}
}