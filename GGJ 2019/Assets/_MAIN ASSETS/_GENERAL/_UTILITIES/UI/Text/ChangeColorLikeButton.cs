using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
	public class ChangeColorLikeButton : MonoBehaviour,
		IPointerDownHandler, IPointerUpHandler
	{
		#region ATTRIBUTES

		[Header ("COMPONENT")]
		[SerializeField] private Image imageComponent;
		[SerializeField] private Text textComponent;

		[Header ("COLORS")]
		[SerializeField] private Color normalColor;
		[SerializeField] private Color pressedColor;

		#endregion

		#region INITIALIZATION

		void Reset ()
		{
			imageComponent = GetComponentInChildren <Image> ();
			textComponent = GetComponentInChildren <Text> ();
		}

		#endregion

		#region BEHAVIOURS

		public void OnPointerDown (PointerEventData eventData)
		{
			imageComponent.color = pressedColor;
			textComponent.color = normalColor;
		}

		public void OnPointerUp (PointerEventData eventData)
		{
			imageComponent.color = normalColor;
			textComponent.color = pressedColor;
		}

		#endregion
	}
}