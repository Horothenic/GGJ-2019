using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Cameras
{
	[RequireComponent (typeof (Camera))]
	public class OrtographicMobileCamera : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header ("RESOLUTION SETTINGS")]
		//iPad has special size. //
		[SerializeField] private float iPadCameraSize = 8.89f;

		// This number is the size of the camera when using the desired resolution. //
		[SerializeField] private float baseSize = 9.5f;

		// These two are which resolution you want to use as base. //
		[SerializeField] private float baseWidth = 640f;
		[SerializeField] private float baseHeight = 960f;

		[Header ("ADJUSTMENT SETTINGS")]
		[SerializeField] private Transform[] topObjects;
		[SerializeField] private Transform[] centerObjects;
		[SerializeField] private Transform[] bottomObjects;

		private float aspectRatio;
		private float iPadAspectRatio = 1536f / 2048f;
		private Camera ortographicCamera;

		#endregion

		#region INITIALIZATION

		void Awake ()
		{
			ortographicCamera = GetComponent <Camera> ();
		}

		void Start ()
		{
			AdjustOrtographicCameraForMobile ();
		}

		#endregion

		#region BEHAVIOURS

		public void AdjustOrtographicCameraForMobile ()
		{
			if (ortographicCamera == null)
			{
				ortographicCamera = GetComponent <Camera> ();
			}

			aspectRatio = (float) Screen.width / (float) Screen.height;

			if (SystemInfo.deviceModel.Contains("iPad") || aspectRatio == iPadAspectRatio)
			{
				ortographicCamera.orthographicSize = iPadCameraSize;

				//Debug.Log ("Ortographic size adjusted to: " + iPadCameraSize);
			}
			else
			{
				float ortographicWidth = baseSize / baseHeight * baseWidth;

				ortographicCamera.orthographicSize = ortographicWidth / ortographicCamera.pixelWidth * ortographicCamera.pixelHeight;

				//Debug.Log ("Ortographic size adjusted to: " + ortographicCamera.orthographicSize);
			}

			AdjustMainPivotPosition ();
		}

		private void AdjustMainPivotPosition ()
		{
			if (topObjects != null)
				for (int i = 0; i < topObjects.Length; i++)
				{
					Vector3 adjustedPosition = topObjects [i].transform.position;
					adjustedPosition.y = ortographicCamera.ScreenToWorldPoint (new Vector2 (0, Screen.height)).y;

					topObjects [i].transform.position = adjustedPosition;
				}

			if (centerObjects != null)
				for (int i = 0; i < centerObjects.Length; i++)
				{
					Vector3 adjustedPosition = centerObjects [i].transform.position;
					adjustedPosition.y = ortographicCamera.ScreenToWorldPoint (new Vector2 (0, Screen.height / 2)).y;

					centerObjects [i].transform.position = adjustedPosition;
				}

			if (bottomObjects != null)
				for (int i = 0; i < bottomObjects.Length; i++)
				{
					Vector3 adjustedPosition = bottomObjects [i].transform.position;
					adjustedPosition.y = ortographicCamera.ScreenToWorldPoint (Vector2.zero).y;

					bottomObjects [i].transform.position = adjustedPosition;
				}
		}

		#endregion
	}
}