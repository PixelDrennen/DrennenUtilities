using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace DrennenUtilities.UI
{
	[RequireComponent(typeof(UI.BoxColliderAutoScaleForUI))]
	public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		///<summary> Amount to increase size when hovering </summary>
		private float hoverModifier = 1.05f;
		/// <summary> This is a variable you can use to pass arguments to functions </summary>
		public object arg;

		public delegate void Callback(object arg);

		public Callback OnDownCallback, OnUpCallback, OnEnterCallback, OnExitCallback;

		[SerializeField]
		protected bool deactivated;
		protected Vector3 baseScale, targetScale;
		// protected BoxCollider bc;
		protected CanvasGroup cg;
		protected UnityEngine.UI.Image backgroundImg;
		protected Color restingColor, targetColor;
		public bool initOnAwake = true, tintBackground = false;
		public float scaleIntensity = 1f;
		[HideInInspector] public float tintOverride = 1f;
		protected bool hovered, selected;
		private RectTransform rt;

		protected void Awake()
		{
			if (initOnAwake) Init();
		}
		public virtual void Init()
		{
			baseScale = transform.localScale;
			targetScale = baseScale;


			// if (GetComponent<BoxCollider>() == null) bc = gameObject.AddComponent<BoxCollider>();
			// else bc = GetComponent<BoxCollider>();

			if (GetComponent<RectTransform>() != null)
			{
				rt = transform as RectTransform;
				// Vector2 size = new Vector2(rt.rect.width, rt.rect.height);
				// bc.size = size;
			}



			if (GetComponent<CanvasGroup>() == null) cg = gameObject.AddComponent<CanvasGroup>();
			else cg = GetComponent<CanvasGroup>();

			if (tintBackground)
			{
				if (transform.Find("Background"))
					backgroundImg = transform.Find("Background").GetComponent<UnityEngine.UI.Image>();
				else if (transform.GetComponentInChildren<UnityEngine.UI.Image>())
					backgroundImg = transform.GetComponentInChildren<UnityEngine.UI.Image>();
				// Debug.Log($"Background: {backgroundImg != null}");
				restingColor = backgroundImg.color;
				targetColor = restingColor;
			}
			// OnDown += OnDownCallback;
			// OnUp += OnUpCallback;
			// OnEnter += OnEnterCallback;
			// OnExit += OnExitCallback;
		}
		public void SetRestingColor(Color c)
		{
			restingColor = c;
			targetColor = restingColor;
		}
		private void Update()
		{
			transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 10f);

			// Vector3 size = new Vector3(rt.rect.width, rt.rect.height, 1);
			// bc.size = size;
			if (deactivated) cg.alpha = .3f; else cg.alpha = 1f;
			if (tintBackground && backgroundImg == null)
			{
				if (GetComponentInChildren<UnityEngine.UI.Image>() == null) { gameObject.name = "NO TINT."; Debug.Log("Could not find an image to use for tint. Skipping."); tintBackground = false; return; }
				backgroundImg = GetComponentInChildren<UnityEngine.UI.Image>();
				restingColor = backgroundImg.color;
				targetColor = restingColor;
			}
			if (tintBackground)
			{
				backgroundImg.color =
				Color.Lerp(backgroundImg.color,
				targetColor,
				Time.deltaTime * 10f);
			}
			//bc.size = (Vector3)(rt.rect.size) + Vector3.forward; 


		}
		public virtual void Deactivate() { deactivated = true; targetScale = baseScale; }
		public virtual void Activate() { deactivated = false; }
		public virtual void Reset()
		{
			deactivated = false;
			targetScale = baseScale;
			selected = false;
			hovered = false;

			_OnExit();
		}
		protected virtual void _OnDown()
		{
			targetScale = baseScale * .95f * scaleIntensity;
			if (tintBackground) targetColor = restingColor.MultiplyColor(.8f) * tintOverride;
		}
		protected virtual void _OnUp()
		{
			targetScale = baseScale * hoverModifier * scaleIntensity;
			if (tintBackground) targetColor = restingColor.MultiplyColor(1.5f) * tintOverride;
		}
		protected virtual void _OnEnter()
		{
			targetScale = baseScale * hoverModifier * scaleIntensity;
			if (tintBackground) targetColor = restingColor.MultiplyColor(1.5f) * tintOverride;
		}
		protected virtual void _OnExit()
		{
			targetScale = baseScale;
			if (tintBackground) targetColor = restingColor * tintOverride;
		}



		public void OnPointerDown(PointerEventData eventData)
		{
			if (deactivated) return;
			_OnDown();
			if (hovered)
			{
				selected = true;
			}
			if (OnDownCallback != null)
				OnDownCallback(arg);
			// Trigger(OnDown);
		}
		public void OnPointerUp(PointerEventData eventData)
		{
			if (deactivated) { return; }

			if (selected && !hovered)
			{
				_OnExit(); selected = false;
				return;
			}

			selected = false;

			// if (!hovered) { Trigger(OnUpCallback); return; }

			_OnUp();
			if (OnUpCallback != null && hovered)
				Trigger(OnUpCallback);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (deactivated) return;
			hovered = true;
			if (selected) return;
			_OnEnter();
			if (OnEnterCallback != null)
				OnEnterCallback(arg);
			// Trigger(OnEnter);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (deactivated) return;
			hovered = false;
			if (selected) return;
			_OnExit();
			if (OnExitCallback != null)
				OnExitCallback(arg);
			// Trigger(OnExit);
		}
		protected virtual void Trigger(Callback callback)
		{
			StartCoroutine(WaitThenTrigger(callback));
		}
		protected WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
		protected IEnumerator WaitThenTrigger(Callback callback)
		{
			for (int i = 0; i < 15; i++)
				yield return wfeof;
			callback(arg);
		}




	}
}