using System.Collections.Generic;
using UnityEngine;

namespace Physics.Collisions.Relay
{
	public class AbstractRelay : MonoBehaviour
	{
		#region ProtectedFields

		protected Dictionary<Rigidbody2D, HashSet<Collider2D>> m_addedRigidbody = new();
		protected HashSet<Collider2D> m_addedColliders = new();

		#endregion
	}
}
