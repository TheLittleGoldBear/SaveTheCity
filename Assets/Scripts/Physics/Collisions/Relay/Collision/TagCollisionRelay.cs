using UnityEngine;

namespace Physics.Collisions.Relay
{
	public class TagCollisionRelay : CollisionRelay
	{
		#region SerializeFields

		[SerializeField] private ETag[] m_acceptedTags;

		#endregion

		#region ProtectedMethods

		protected override bool CheckConditions(Collision2D collision2D)
		{
			return collision2D.CompareTag(m_acceptedTags);
		}

		#endregion
	}
}
