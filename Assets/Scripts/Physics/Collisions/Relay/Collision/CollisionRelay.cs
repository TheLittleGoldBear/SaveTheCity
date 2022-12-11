using System;
using System.Collections.Generic;
using UnityEngine;

namespace Physics.Collisions.Relay
{
	public class CollisionRelay : AbstractRelay
	{
		#region Events

		public event Action<Collision2D> CollisionEnter2D;
		public event Action<Collision2D> CollisionStay2D;
		public event Action<Collision2D> CollisionExit2D;

		#endregion

		#region SerializeFields

		[SerializeField] private Rigidbody2D m_rigidbody2D;

		#endregion

		#region ProtectedMethods

		protected virtual void OnCollisionEntered2D(Collision2D collision2D)
		{ }

		protected virtual void OnCollisionStaying2D(Collision2D collision2D)
		{ }

		protected virtual void OnCollisionExited2D(Collision2D collision2D)
		{ }

		protected virtual bool CheckConditions(Collision2D collision2D)
		{
			return true;
		}

		#endregion

		#region PrivateMethods

		private void NotifyOnCollisionEnter2D(Collision2D collision2D)
		{
			CollisionEnter2D?.Invoke(collision2D);
		}

		private void NotifyOnCollisionStay2D(Collision2D collision2D)
		{
			CollisionStay2D?.Invoke(collision2D);
		}

		private void NotifyOnCollisionExit2D(Collision2D collision2D)
		{
			CollisionExit2D?.Invoke(collision2D);
		}

		private void OnCollisionEnter2D(Collision2D collision2D)
		{
			if (!CheckConditions(collision2D))
			{
				return;
			}
			
			Rigidbody2D collisionRigidbody = collision2D.rigidbody;
			Collider2D collision2DCollider = collision2D.collider;
			bool wasAdded = false;

			if (m_addedRigidbody.TryGetValue(collisionRigidbody, out HashSet<Collider2D> collider2Ds))
			{
				collider2Ds.Add(collision2DCollider);
			}
			else
			{
				m_addedRigidbody.Add(collisionRigidbody, new HashSet<Collider2D> { collision2DCollider });
				wasAdded = true;
			}

			if (wasAdded)
			{
				OnCollisionEntered2D(collision2D);
				NotifyOnCollisionEnter2D(collision2D);
			}
		}

		private void OnCollisionStay2D(Collision2D collision2D)
		{
			if (!CheckConditions(collision2D))
			{
				return;
			}
			
			OnCollisionStaying2D(collision2D);
			NotifyOnCollisionStay2D(collision2D);
		}

		private void OnCollisionExit2D(Collision2D collision2D)
		{
			if (!CheckConditions(collision2D))
			{
				return;
			}
			
			Rigidbody2D collisionRigidbody = collision2D.rigidbody;
			Collider2D collision2DCollider = collision2D.collider;
			bool wasRemoved = false;

			if (m_addedRigidbody.TryGetValue(collisionRigidbody, out HashSet<Collider2D> collider2Ds))
			{
				int collidersInContact = m_rigidbody2D.GetContacts(new[] { collision2DCollider });

				if (collidersInContact == 0)
				{
					if (collider2Ds.Remove(collision2DCollider) && collider2Ds.Count == 0)
					{
						wasRemoved = true;
					}
				}
			}

			if (wasRemoved)
			{
				OnCollisionExited2D(collision2D);
				NotifyOnCollisionExit2D(collision2D);
			}
		}

		#endregion
	}
}
