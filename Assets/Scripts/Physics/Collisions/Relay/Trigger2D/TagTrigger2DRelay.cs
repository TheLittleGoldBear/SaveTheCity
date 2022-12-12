using Physics.Collisions.Relay.Tag;
using UnityEngine;

namespace Physics.Collisions.Relay.Trigger2D
{
	public class TagTrigger2DRelay : Trigger2DRelay
	{
		#region SerializeFields

		[SerializeField] private ETag[] m_acceptedTags;

		#endregion

		#region ProtectedMethods

		protected override bool CheckConditions(Collider2D col)
		{
			return col.CompareTag(m_acceptedTags);
		}

		#endregion
	}
}
