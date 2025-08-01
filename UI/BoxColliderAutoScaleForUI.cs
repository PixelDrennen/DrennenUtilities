using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrennenUtilities.UI
{
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(RectTransform))]
	public class BoxColliderAutoScaleForUI : MonoBehaviour
	{
		private BoxCollider bc;
		void Update()
		{
			if (bc == null) { bc = GetComponent<BoxCollider>(); return; }

			if (bc.size != lastSize)
			{
				bc.size = CalcSize();
				lastSize = bc.size;
			}
		}
		private Vector3 lastSize;
		private Vector3 CalcSize()
		{
			for (int i = 0; i < 2; i++) if ((transform as RectTransform).rect.size[i] < 0) return Vector3.zero;

			return ((Vector3)(transform as RectTransform).rect.size + Vector3.forward);
		}
	}
}