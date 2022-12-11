using UnityEngine;

namespace Physics.Collisions.Relay
{
	public class TagTriggerRelay : TriggerRelay
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
