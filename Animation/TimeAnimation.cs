using System;
/*
    This script was created by Drennen Dooms, 2022.
*/

using System.Collections;
using System.Collections.Generic;
using Dooms.Math;
using UnityEngine;


namespace Dooms.Animation
{
	[System.Serializable]
	public class AnimationCollection
	{
		public Transform parent;
		public List<PixelDustAnimation> animations;
	}
	[System.Serializable]
	public class PixelDustAnimation
	{
		public string name = "object";
		public ValueSet valueSet;
		public PixelDustAnimation() { }
		public PixelDustAnimation(string name, ValueSet valueSet)
		{
			this.name = name;
			this.valueSet = valueSet;
		}
		public PixelDustAnimation Clone()
		{
			return new PixelDustAnimation(name, valueSet.Clone());
		}
	}

	[System.Serializable]
	public class AnimatedObject
	{
		public string name = "object";
		public Transform transform;
		// [HideInInspector]
		public AnimationSet animationSet;
	}

	[System.Serializable]
	public class AnimationSet
	{
		public string name = "object";
		public List<ValueSet> scales = new List<ValueSet>();
		public List<ValueSet> moves = new List<ValueSet>();
		public List<ValueSet> rotations = new List<ValueSet>();



	}
	[System.Serializable]
	public class ValueSet
	{
		public bool enabled = true;
		[Range(0f, 1f)]
		public float from = 0, to = 1;
		public Vector3 start, end;
		public Math.Direction transitionDirection;
		public Math.Interpolation interpolation = Math.Interpolation.circ;

		public ValueSet()
		{
			enabled = true;
			from = 0;
			to = 1;
			start = Vector3.zero;
			end = Vector3.zero;
			interpolation = Math.Interpolation.circ;
		}

		public ValueSet(bool enabled, float from, float to, Vector3 start, Vector3 end, Math.Direction transitionDirection, Math.Interpolation interpolation)
		{
			this.enabled = enabled;
			this.from = from;
			this.to = to;
			this.start = start;
			this.end = end;
			this.transitionDirection = transitionDirection;
			this.interpolation = interpolation;
		}

		public ValueSet Clone()
		{
			// Debug.Log($"(from {from.ToString("0.00")} to {to.ToString("0.00")})");
			return new ValueSet(enabled, Mathf.Abs(from), Mathf.Abs(to), start, end, transitionDirection, interpolation); ;
		}
	}

	public class TimeAnimation : MonoBehaviour
	{


		public delegate void Callback();
		public Callback OnAnimateCallback;
		public Callback OnCompleteCallback;


		public float time = 0;
		private float speed = 2f;
		private float direction = 0;
		private bool completed;
		public bool pingpong;

		[HideInInspector]
		public AnimationSetManager animationSetManager;

		public List<AnimatedObject> animatedObjects = new List<AnimatedObject>();
		public AnimationData animationData;


		private void Awake()
		{
			animationSetManager = new AnimationSetManager(this);

			if (animationData != null && animationData.animationSets != null)
			{
				if (animatedObjects.Count != animationData.animationSets.Count) { Debug.LogError("Animated objects and animationdata must have the same length."); return; }

				for (int i = 0; i < animatedObjects.Count; i++)
				{
					if (animationData.animationSets[i].name == animatedObjects[i].name) animatedObjects[i].animationSet = animationData.animationSets[i];
					else { Debug.LogError($"Animated object name {animatedObjects[i].name} does not match animation set {animationData.animationSets[i].name}"); }
				}
			}

			OnAwake();
			if (OnAnimateCallback == null) OnAnimateCallback = OnAnimate;
			if (OnCompleteCallback == null) OnCompleteCallback = OnComplete;
		}

		private void Update()
		{
			OnUpdate();
		}

		private void Start()
		{
			OnStart();
		}

		public virtual void OnUpdate()
		{
			Animate();
		}

		public virtual void OnAwake()
		{

		}
		public virtual void OnStart()
		{
			if (animationData == null) return;


		}
		public virtual void InitializeAnimationSets()
		{
			try
			{
				for (int i = 0; i < animationData.animationSets.Count; i++)
				{
					animationSetManager.InitializeAnimationSet(animatedObjects[i].animationSet, animatedObjects[i].transform);
				}
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public virtual void ProcessAnimationSets()
		{
			try
			{
				for (int i = 0; i < animationData.animationSets.Count; i++)
				{
					animationSetManager.ProcessAnimationSet(animatedObjects[i].animationSet, animatedObjects[i].transform);
				}
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public virtual void PostAwake()
		{
			if (pingpong)
				StartCoroutine(PingPong());
		}

		public void SetSpeed(double speed) { SetSpeed((float)speed); }
		public void SetSpeed(float speed)
		{
			this.speed = speed;
		}
		public float GetSpeed()
		{
			return speed;
		}
		public float GetTime()
		{
			return time;
		}
		public bool Completed { get { return completed; } }
		public void SetTime(float t)
		{
			time = t;
		}

		public void SetDuration(float t)
		{
			SetSpeed(1 / t);
		}

		public float Slice(float currentTime, float start, float end)
		{
			float duration = end - start;
			// Debug.Log($"Slice current time: {currentTime} || Duration {duration} || start: {start} end: {end}");
			if (currentTime <= 0) return 0f;
			if (duration <= 0f) return 0f;
			if (start <= 0f) start = 0f;

			float timeSliceCurrentTime = currentTime - start;
			if (timeSliceCurrentTime <= 0f) return 0f;
			if (timeSliceCurrentTime >= duration) return 1f;

			// Debug.Log($"Slice: {timeSliceCurrentTime / duration}");

			return timeSliceCurrentTime / duration;
		}

		private float Remap(float val, float from1, float to1, float from2, float to2)
		{
			return (val - from1) / (to1 - from1) * (to2 - from2) + from2;
		}

		public void SetDirection(int dir)
		{
			direction = (float)dir;
			completed = false;
		}
		public int GetDirection()
		{
			return (int)direction;
		}

		public void Animate()
		{
			time = Mathf.Clamp(time += direction * speed * Time.deltaTime, 0, 1);

			OnAnimateCallback();

			if (direction != 0)
				if ((time == 1 && direction == 1 || time == 0 && direction == -1)) OnCompleteCallback();
		}

		public virtual void OnAnimate()
		{
			if (animationData == null) return;


		}

		public virtual void OnComplete()
		{
			completed = true;
		}


		//         public Vector3 SlideIn(Vector3 startPos, Vector3 endPos)
		//         {
		//             return Vector3.Lerp(startPos, endPos, Easing.EaseInBezier(time));
		//         }
		// 
		//         public Vector2 SlideIn(Vector2 startPos, Vector2 endPos)
		//         {
		//             return Vector2.Lerp(startPos, endPos, Easing.EaseInBezier(time));
		//         }





		public IEnumerator PingPong()
		{
			yield return new WaitUntil(() => Completed);
			yield return new WaitForSeconds(1);
			SetDirection(-GetDirection());
			StartCoroutine(PingPong());
		}

		public static float ProcessInterpolation(Interpolation interpolation, Direction direction, float alpha)
		{
			return Easing.Ease(alpha, interpolation, direction);
		}

		public class AnimationSetManager
		{

			private TimeAnimation animation;

			public AnimationSetManager(TimeAnimation timeAnimation)
			{
				this.animation = timeAnimation;
			}

			public void InitializeAnimationSet(AnimationSet animSet, Transform t)
			{
				// moves
				for (int i = 0; i < animSet.moves.Count; i++)
				{
					if (animSet.moves[i].enabled == false) return;

					// if (!(animation.time >= animSet.moves[i].from && animation.time <= animSet.moves[i].to)) return;

					Vector3 v = ProcessValueSet(animSet.moves[i]);
					// if (NaN(v)) continue;
					t.localPosition = animSet.moves[i].start;
				}

				// rotations
				for (int i = 0; i < animSet.rotations.Count; i++)
				{
					if (animSet.rotations[i].enabled == false) return;

					// if (!(animation.time >= animSet.rotations[i].from && animation.time <= animSet.rotations[i].to)) return;

					Vector3 v = ProcessValueSet(animSet.rotations[i]);
					// if (NaN(v)) continue;
					// t.localRotation = Quaternion.Euler(ProcessValueSet(animSet.rotations[i]));
					t.localRotation = Quaternion.Euler(animSet.rotations[i].start);
				}
				// scales
				for (int i = 0; i < animSet.scales.Count; i++)
				{
					if (animSet.scales[i].enabled == false) return;

					// if (!(animation.time >= animSet.scales[i].from && animation.time <= animSet.scales[i].to)) return;

					Vector3 v = ProcessValueSet(animSet.scales[i]);
					// if (NaN(v)) continue;
					// t.localScale = ProcessValueSet(animSet.scales[i]);
					t.localScale = animSet.scales[i].start;
				}
			}

			private bool NaN(Vector3 v)
			{
				if (float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z)) return true;
				return false;
			}

			public void ProcessAnimationSet(AnimationSet animSet, Transform t)
			{
				// moves
				for (int i = 0; i < animSet.moves.Count; i++)
				{
					if (animation.time >= animSet.moves[i].from && animation.time <= animSet.moves[i].to)
					{
						if (animSet.moves[i].enabled == false) return;

						Vector3 v = ProcessValueSet(animSet.moves[i]);
						// if (NaN(v)) continue;
						t.localPosition = ProcessValueSet(animSet.moves[i]);
					}
				}

				// rotations
				for (int i = 0; i < animSet.rotations.Count; i++)
					if (animation.time >= animSet.rotations[i].from && animation.time <= animSet.rotations[i].to)
					{
						if (animSet.rotations[i].enabled == false) return;
						Vector3 v = ProcessValueSet(animSet.rotations[i]);
						// if (NaN(v)) continue;
						t.localEulerAngles = ProcessValueSet(animSet.rotations[i]);
					}

				// scales
				for (int i = 0; i < animSet.scales.Count; i++)
					if (animation.time >= animSet.scales[i].from && animation.time <= animSet.scales[i].to)
					{
						if (animSet.scales[i].enabled == false) return;
						Vector3 v = ProcessValueSet(animSet.scales[i]);
						// if (NaN(v)) continue;
						t.localScale = ProcessValueSet(animSet.scales[i]);
					}
			}

			public Vector3 ProcessValueSet(ValueSet valueSet)
			{
				Vector3 vec = Vector3.zero;

				float alpha = animation.Slice(animation.time, valueSet.from, valueSet.to);


				// easing
				alpha = TimeAnimation.ProcessInterpolation(valueSet.interpolation, valueSet.transitionDirection, alpha);
				// alpha = Mathf.Clamp01(animation.EaseOutCirc(alpha));

				alpha = animation.Slice(alpha, valueSet.from, valueSet.to);


				vec = Vector3.Lerp(valueSet.start, valueSet.end, alpha);


				return vec;
			}
		}


	}
}
/*
    This script was created by Drennen Dooms, 2022.
*/


namespace Dooms.Animation
{

	[CreateAssetMenu(fileName = "Data", menuName = "Animations/AnimatedObjectList", order = 1)]
	public class AnimationData : ScriptableObject
	{

		public void SetAllToDefault()
		{
			for (int i = 0; i < animationSets.Count; i++)
			{
				for (int j = 0; j < animationSets[i].scales.Count; j++)
				{
					animationSets[i].scales[j].interpolation = globalInterpolationDefault;
					animationSets[i].scales[j].transitionDirection = globalDirectionDefault;
				}
				for (int j = 0; j < animationSets[i].moves.Count; j++)
				{
					animationSets[i].moves[j].interpolation = globalInterpolationDefault;
					animationSets[i].moves[j].transitionDirection = globalDirectionDefault;
				}
				for (int j = 0; j < animationSets[i].rotations.Count; j++)
				{
					animationSets[i].rotations[j].interpolation = globalInterpolationDefault;
					animationSets[i].rotations[j].transitionDirection = globalDirectionDefault;
				}
			}
		}

		public List<AnimationSet> animationSets = new List<AnimationSet>();

		public Math.Interpolation globalInterpolationDefault;
		public Math.Direction globalDirectionDefault;
	}
}
