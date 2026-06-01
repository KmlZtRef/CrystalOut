using UnityEngine;

namespace _02._Script.Effects
{
	[CreateAssetMenu(fileName = "Particle Effect List", menuName = "Effects/Particle Effect List", order = 0)]
	public class ParticleEffectListSo : ScriptableObject
	{
		[field: SerializeField] public PlayableEffect SmallCollisionEffect { get; private set; }
	}
}