using Physics.Collisions.Relay.Tag;
using UnityEngine;

namespace Physics.Collisions.Relay.Collision2D
{
	public class TagCollision2DRelay : Collision2DRelay
	{
		#region SerializeFields

		[SerializeField] private ETag[] m_acceptedTags;

		#endregion

		#region ProtectedMethods

		protected override bool CheckConditions(UnityEngine.Collision2D collision2D)
		{
			return collision2D.CompareTag(m_acceptedTags);
		}

		#endregion
	}
}
