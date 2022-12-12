using System;
using System.Collections.Generic;
using UnityEngine;

namespace Physics.Collisions.Relay.Trigger2D
{
	public class Trigger2DRelay : AbstractRelay
	{
		#region Events

		public event Action<Collider2D> TriggerEnter2D;
		public event Action<Collider2D> TriggerExit2D;

		#endregion

		#region PrivateMethods

		private void OnTriggerEntered2D(Collider2D col)
		{ }

		private void OnTriggerExited2D(Collider2D col)
		{ }
		
		protected virtual bool CheckConditions(Collider2D col)
		{
			return true;
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col == null || !CheckConditions(col))
			{
				return;
			}

			Rigidbody2D attachedRigidbody = col.attachedRigidbody;
			bool wasAdded = false;

			if (attachedRigidbody != null)
			{
				if (m_addedRigidbody.TryGetValue(attachedRigidbody, out HashSet<Collider2D> colliders))
				{
					colliders.Add(col);
				}
				else
				{
					m_addedRigidbody[attachedRigidbody] = new HashSet<Collider2D> { col };
					wasAdded = true;
				}
			}
			else
			{
				wasAdded = m_addedColliders.Add(col);
			}

			if (wasAdded)
			{
				OnTriggerEntered2D(col);
				NotifyOnTriggerEnter(col);
			}
		}

		private void OnTriggerExit2D(Collider2D col)
		{
			if (col == null || !CheckConditions(col))
			{
				return;
			}

			Rigidbody2D attachedRigidbody = col.attachedRigidbody;
			bool wasRemoved = false;

			if (attachedRigidbody != null)
			{
				if (m_addedRigidbody.TryGetValue(attachedRigidbody, out HashSet<Collider2D> colliders))
				{
					if (colliders.Remove(col) && colliders.Count == 0)
					{
						m_addedRigidbody.Remove(attachedRigidbody);
						wasRemoved = true;
					}
				}
			}
			else
			{
				wasRemoved = m_addedColliders.Remove(col);
			}

			if (wasRemoved)
			{
				OnTriggerExited2D(col);
				NotifyOnTriggerExit(col);
			}
		}

		private void NotifyOnTriggerEnter(Collider2D col)
		{
			TriggerEnter2D?.Invoke(col);
		}

		private void NotifyOnTriggerExit(Collider2D col)
		{
			TriggerExit2D?.Invoke(col);
		}

		#endregion
	}
}
