/*
    This script was created by Drennen Dooms, 2022.
*/

using System.Collections.Generic;
using UnityEngine;


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
