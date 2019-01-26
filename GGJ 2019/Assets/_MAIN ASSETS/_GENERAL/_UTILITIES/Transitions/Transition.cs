using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Utilities.Transitions
{
	public class Transition : MonoBehaviour 
	{
		/// ////////////////////////////// ///
		/// ///////  CANVAS GROUP  /////// ///
		/// ////////////////////////////// ///

		public const float defaultTime = 0.4f;
		private Action action;
		private float fadeTime;
		private float fadeValue;
		private bool ignoreTimescale;
		private CanvasGroup canvasGroup;
		private bool fadeIn = false;

		void DoAction()
		{
			InteractableState ();

			if(action != null)
				action ();
			Destroy (this);
		}

		void SetCanvasGroupFade(float fadeTime, float fadeValue, Action action)
		{
			//iTween.Stop (this.gameObject);
			this.action = action;
			canvasGroup = gameObject.GetComponent<CanvasGroup> ();
			Hashtable ht = iTween.Hash(
				"from",canvasGroup.alpha,
				"to",fadeValue,
				"time",fadeTime,
				"easetype",iTween.EaseType.linear,
				"onupdate","UpdateAlpha",
				"ignoretimescale",ignoreTimescale,
				"oncomplete","DoAction");
			iTween.ValueTo(gameObject,ht);
		}

		void UpdateAlpha(float alpha)
		{
			canvasGroup.alpha = alpha;
		}

		public static void FadeInCanvasGroup(CanvasGroup cg, float fadeTime = defaultTime,  bool interactableFromstart = true, Action action = null, bool ignoreTimescale = true)
		{
			cg.gameObject.SetActive (true);

			if (interactableFromstart) 
			{
				cg.interactable = true;
				cg.blocksRaycasts = true;
			}
			FadeCanvasGroup (true, cg, fadeTime, action, ignoreTimescale);
		}

		public static void FadeOutCanvasGroup(CanvasGroup cg, float fadeTime = defaultTime, bool uninteractableFromstart = true, Action action = null, bool ignoreTimescale = true)
		{
			if (uninteractableFromstart) 
			{
				cg.interactable = false;
				cg.blocksRaycasts = false;
			}
			FadeCanvasGroup (false, cg, fadeTime, action, ignoreTimescale);
		}

		static void FadeCanvasGroup(bool fadeIn, CanvasGroup cg, float fadeTime = defaultTime, Action action = null, bool ignoreTimescale = true)
		{
			float alpha = fadeIn ? 1 : 0;
			Transition t = cg.gameObject.AddComponent<Transition> ();

			t.fadeIn = fadeIn;
			t.ignoreTimescale = ignoreTimescale;
			
			t.SetCanvasGroupFade(fadeTime,alpha,action);
		}

		void InteractableState()
		{
			if(fadeIn)
			{
				canvasGroup.blocksRaycasts = true;
				canvasGroup.interactable = true;
			}
			else
			{
				canvasGroup.blocksRaycasts = false;
				canvasGroup.interactable = false;

				//canvasGroup.gameObject.SetActive (false);
			}
		}

		public static void MovePanelFromScreen (RectTransform panel, Direction direction, float transitionTime, Action onComplete = null)
		{
			float from, to;

			if (direction == Direction.Left || direction == Direction.Right) 
			{
				from = panel.anchoredPosition.x;
				to = direction == Direction.Right ? from + panel.rect.width : from - panel.rect.width;
			}
			else
			{
				from = panel.anchoredPosition.y;
				to = direction == Direction.Up ? from + panel.rect.height : from - panel.rect.height;
			}

			// Actions. //

			Action <object> onUpdateAction = (offset => 
			{
				// Adjust panel position
				if (direction == Direction.Left || direction == Direction.Right) 
					panel.anchoredPosition = new Vector2 ((float)offset, panel.anchoredPosition.y);
				else
					panel.anchoredPosition = new Vector2 (panel.anchoredPosition.x, (float)offset);
			});

			Action <object> onCompleteAction = (offset => 
			{
				if(onComplete != null)
					onComplete();
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", true);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (panel.gameObject, hash);
		}
		
		public static void MovePanelUpSpecificRectTransform(RectTransform panel, Action onComplete, float transitionTime, RectTransform next = null, RectTransform previous = null)
		{
			//ApplicationManager.Instance.CurrentEventSystem.enabled = false;

			float from, to;

			from = panel.anchoredPosition.y;
			to = next != null ? (previous != null ? from + (next.rect.height - previous.rect.height) : from + next.rect.height) : 0;

			Action<object> onUpdateAction = (offset => 
			{
				panel.anchoredPosition = new Vector2 (panel.anchoredPosition.x, (float)offset);
			});

			Action<object> onCompleteAction = (offset => 
			{
				if(onComplete != null)
					onComplete();
				//ApplicationManager.Instance.CurrentEventSystem.enabled = true;
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", true);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (panel.gameObject, hash);
		}

		public static void MoveRectTransformVertically (RectTransform rectTransfrom, float amount, float transitionTime, Action onEnd = null, bool ignoreTimescale = false, iTween.EaseType ease = iTween.EaseType.easeInOutQuad, float delay = 0)
		{
			float from, to;

			from = rectTransfrom.anchoredPosition.y;
			to = from + amount;

			Action <object> onUpdateAction = (offset => 
			{
				rectTransfrom.anchoredPosition = new Vector2 (rectTransfrom.anchoredPosition.x, (float) offset);
			});

			Action <object> onCompleteAction = (offset => 
			{
				if (onEnd != null)
					onEnd ();
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("delay", delay);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", ignoreTimescale);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (rectTransfrom.gameObject, hash);
		}

		public static void MoveRectTransformHorizontally (RectTransform rectTransfrom, float amount, float transitionTime, Action onEnd = null, bool ignoreTimescale = false, iTween.EaseType ease = iTween.EaseType.easeInOutQuad, float delay = 0)
		{
			float from, to;

			from = rectTransfrom.anchoredPosition.x;
			to = from + amount;

			Action <object> onUpdateAction = (offset => 
			{
				rectTransfrom.anchoredPosition = new Vector2 ((float) offset, rectTransfrom.anchoredPosition.y);
			});

			Action <object> onCompleteAction = (offset => 
			{
				if (onEnd != null)
					onEnd ();
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("delay", delay);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", ignoreTimescale);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (rectTransfrom.gameObject, hash);
		}

		public static void StretchRectVertically (RectTransform stretchRectTransform, float desiredAmount, float transitionTime, Action onComplete)
		{
			//ApplicationManager.Instance.CurrentEventSystem.enabled = false;

			float from = stretchRectTransform.sizeDelta.y;
			float to = desiredAmount;

			Action <object> onUpdateAction = (newValue => 
			{
				stretchRectTransform.sizeDelta = new Vector2 (stretchRectTransform.sizeDelta.x, (float) newValue);
			});
			
			Action <object> onCompleteAction = (none => 
			{
				//ApplicationManager.Instance.CurrentEventSystem.enabled = true;

				onComplete ();
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", true);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (stretchRectTransform.gameObject, hash);
		}

		public static void MoveBetweenPoints (Transform movingTransform, Vector3 originPosition, Vector3 endPosition, float transitionTime, float delay = 0, iTween.EaseType easeType = iTween.EaseType.easeInOutQuad, Action onEnd = null)
		{
			Vector3 from = originPosition;
			Vector3 to = endPosition;

			Action <object> onUpdateAction = (newValue => 
			{
				movingTransform.position = (Vector3) newValue;
			});

			Action <object> onEndAction = (none => 
			{
				if (onEnd != null)
					onEnd ();
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("delay", delay);
			hash.Add ("easetype", easeType);
			//hash.Add ("ignoretimescale", true);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onEndAction);

			iTween.ValueTo (movingTransform.gameObject, hash);
		}

		public static void MoveBetweenPointsLocal (Transform movingRectTransform, Vector3 originPosition, Vector3 endPosition, float transitionTime, float delay = 0, iTween.EaseType easeType = iTween.EaseType.easeInOutQuad, Action onEnd = null)
		{
			Vector3 from = originPosition;
			Vector3 to = endPosition;

			Action <object> onUpdateAction = (newValue => 
			{
				movingRectTransform.localPosition = (Vector3) newValue;
			});

			Action <object> onEndAction = (none => 
			{
				if (onEnd != null)
					onEnd ();
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("delay", delay);
			hash.Add ("easetype", easeType);
			//hash.Add ("ignoretimescale", true);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onEndAction);

			iTween.ValueTo (movingRectTransform.gameObject, hash);
		}
		
		public static void MoveToPosition (RectTransform movingRectTransform, Vector2 newPosition, float transitionTime, Action onEnd = null)
		{
			MoveBetweenPoints (movingRectTransform, movingRectTransform.anchoredPosition, newPosition, transitionTime, 0, iTween.EaseType.easeInOutQuad, onEnd);
		}

		public static void AppearCanvasGroup (CanvasGroup canvasGroup, bool interactable = true)
		{
			canvasGroup.alpha = 1;

			if (interactable)
			{
				canvasGroup.blocksRaycasts = true;
				canvasGroup.interactable = true;
			} 
			else
			{
				canvasGroup.blocksRaycasts = false;
				canvasGroup.interactable = false;
			}
		}

		public static void DisappearCanvasGroup (CanvasGroup canvasGroup, bool interactable = true)
		{
			canvasGroup.alpha = 0;

			if (interactable)
			{
				canvasGroup.blocksRaycasts = true;
				canvasGroup.interactable = true;
			} 
			else
			{
				canvasGroup.blocksRaycasts = false;
				canvasGroup.interactable = false;
			}
		}

		public static void MoveScrollVertically (ScrollRect scrollRect, float amount, float transitionTime)
		{
			float from = scrollRect.verticalNormalizedPosition;
			float to = scrollRect.verticalNormalizedPosition + amount;

			Action <object> onUpdateAction = (newValue => 
			{
				scrollRect.verticalNormalizedPosition = (float) newValue;
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", true);
			hash.Add ("onupdate", onUpdateAction);

			iTween.ValueTo (scrollRect.gameObject, hash);
		}

		public static void FillImage (Image imageToFill, float newFillAmount, float transitionTime, Action onEnd = null,  iTween.EaseType easeType = iTween.EaseType.easeInOutQuad)
		{
			float from = imageToFill.fillAmount;
			float to = newFillAmount;

			Action <object> onUpdateAction = (newValue => 
			{
				imageToFill.fillAmount = (float) newValue;
			});

			Action <object> onEndAction = (none => 
			{
				if (onEnd != null)
					onEnd ();
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("easetype", easeType);
			hash.Add ("ignoretimescale", true);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onEndAction);

			iTween.ValueTo (imageToFill.gameObject, hash);
		}

		public static void DwarfObject (Transform transformToDwarf, Vector3 wantedSize, float transitionTime, iTween.EaseType easeType, Action onEnd)
		{
			Vector3 from = transformToDwarf.localScale;
			Vector3 to = wantedSize;

			Action <object> onUpdateAction = (newValue => 
			{
				transformToDwarf.localScale = (Vector3) newValue;
			});

			Action <object> onEndAction = (none => 
			{
				if (onEnd != null)
					onEnd ();
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("easetype", easeType);
			hash.Add ("ignoretimescale", true);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onEndAction);

			iTween.ValueTo (transformToDwarf.gameObject, hash);
		}

		public static void MoveTransformVertically (Transform thisTransform, float amount, float transitionTime, bool ignoreTimescale = false, iTween.EaseType ease = iTween.EaseType.easeInOutQuad, float delay = 0, Action onEnd = null)
		{
			float from, to;

			from = thisTransform.position.y;
			to = from + amount;

			Action <object> onUpdateAction = (offset => 
				{
					thisTransform.position = new Vector3 (thisTransform.position.x, (float) offset, thisTransform.position.z);
				});

			Action <object> onCompleteAction = (offset => 
				{
					if (onEnd != null)
						onEnd ();
				});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("delay", delay);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", ignoreTimescale);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (thisTransform.gameObject, hash);
		}

		public static void MoveTransformHorizontally (Transform thisTransform, float amount, float transitionTime, bool ignoreTimescale = false, iTween.EaseType ease = iTween.EaseType.easeInOutQuad, float delay = 0, Action onEnd = null)
		{
			float from, to;

			from = thisTransform.position.x;
			to = from + amount;

			Action <object> onUpdateAction = (offset => 
				{
					thisTransform.position = new Vector2 ((float) offset, thisTransform.position.y);
				});

			Action <object> onCompleteAction = (offset => 
				{
					if (onEnd != null)
						onEnd ();
				});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("delay", delay);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", ignoreTimescale);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (thisTransform.gameObject, hash);
		}

		public static void FadeSpriteRenderer (SpriteRenderer spriteRenderer, float targetAlpha, float transitionTime, bool ignoreTimescale = false, iTween.EaseType ease = iTween.EaseType.easeInOutQuad, float delay = 0, Action onEnd = null)
		{
			float from, to;

			from = spriteRenderer.color.a;
			to = targetAlpha;

			Action <object> onUpdateAction = (alpha => 
				{
					spriteRenderer.color = new Color (spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (float) alpha);
				});

			Action <object> onCompleteAction = (offset => 
				{
					if (onEnd != null)
						onEnd ();
				});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("delay", delay);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", ignoreTimescale);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (spriteRenderer.gameObject, hash);
		}

		public static void FadeTextMesh (TextMesh textMesh, float targetAlpha, float transitionTime, bool ignoreTimescale = false, iTween.EaseType ease = iTween.EaseType.easeInOutQuad, float delay = 0, Action onEnd = null)
		{
			float from, to;

			from = textMesh.color.a;
			to = targetAlpha;

			Action <object> onUpdateAction = (alpha => 
				{
					textMesh.color = new Color (textMesh.color.r, textMesh.color.g, textMesh.color.b, (float) alpha);
				});

			Action <object> onCompleteAction = (offset => 
				{
					if (onEnd != null)
						onEnd ();
				});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("delay", delay);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", ignoreTimescale);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (textMesh.gameObject, hash);
		}

		public static void Scale (Transform transform, Vector3 target, float transitionTime, bool ignoreTimescale = false, iTween.EaseType ease = iTween.EaseType.easeInOutQuad, float delay = 0, Action onEnd = null)
		{
			Vector3 from, to;

			from = transform.localScale;
			to = target;

			Action <object> onUpdateAction = (scale => 
			{
				transform.localScale = (Vector3) scale;
			});

			Action <object> onCompleteAction = (offset => 
			{
				if (onEnd != null)
					onEnd ();
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("delay", delay);
			hash.Add ("easetype", iTween.EaseType.easeInOutQuad);
			hash.Add ("ignoretimescale", ignoreTimescale);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			//print ("Scaling");

			iTween.ValueTo (transform.gameObject, hash);
		}
	}
}