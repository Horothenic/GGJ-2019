using UnityEngine;
using System.Collections;
using System;

public class TakeScreenshot : MonoBehaviour
{
	static Texture2D screenshot;

	public static bool IsScreenshotNull
	{
		get
		{
			return screenshot == null ? true : false;
		}
	}

	public static void Take (Camera cam = null, Action action = null)
	{
		TakeScreenshot takeScreenshot = new GameObject ("TakeScreenshot").AddComponent<TakeScreenshot> ();

		takeScreenshot.StartCoroutine (takeScreenshot.InternalTake(cam, action));
	}

	IEnumerator InternalTake (Camera cam = null, Action action = null)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();

		if (cam != null)
			TakeTextureFromCamera (cam);
		else
			TakeTextureFromScreen ();

		if (action != null)
			action ();
		
		Destroy (this.gameObject);
	}

	void TakeTextureFromCamera(Camera cam)
	{
		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = cam.targetTexture;
		cam.Render();

		int width = cam.pixelWidth;
		int heigth = cam.pixelHeight;

		screenshot = new Texture2D(width, heigth, TextureFormat.RGB24, true);
		screenshot.ReadPixels(new Rect(0, 0, width, heigth), 0, 0);
		screenshot.Apply();
		RenderTexture.active = currentRT;
		cam.Render();
	}

	void TakeTextureFromScreen()
	{
		int width = Screen.width;
		int heigth = Screen.height;
		screenshot = new Texture2D(width, heigth, TextureFormat.RGB24, true);
		screenshot.ReadPixels(new Rect(0, 0, width, heigth), 0, 0);
		screenshot.Apply();
	}

	public static Texture2D GetScreenshot()
	{
		return screenshot;
	}
}
