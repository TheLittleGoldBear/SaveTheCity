using System.Collections.Generic;
using Physics.Collisions.Relay.Trigger2D;
using UnityEngine;

namespace Physics.Collisions.DetectionService
{
	public abstract class AbstractDetectionService<T> where T : MonoBehaviour
	{
		#region Properties

		public HashSet<T> DetectedObjects { get; }

		#endregion

		#region PrivateFields

		private readonly Trigger2DRelay m_trigger2DRelay;
		private bool m_registeredToEvents;

		#endregion

		#region Constructors

		protected AbstractDetectionService(Trigger2DRelay trigger2DRelay)
		{
			DetectedObjects = new HashSet<T>();
			m_trigger2DRelay = trigger2DRelay;

			RegisterToEvents();
		}

		#endregion

		#region PublicMethods

		public void OnTearDown()
		{
			UnregisterFromEvents();
		}

		#endregion

		#region PrivateMethods

		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_trigger2DRelay.TriggerEnter2D += OnTrigger2DEntered;
			m_trigger2DRelay.TriggerExit2D += OnTrigger2DExited;

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_trigger2DRelay.TriggerEnter2D -= OnTrigger2DEntered;
			m_trigger2DRelay.TriggerExit2D -= OnTrigger2DExited;

			m_registeredToEvents = false;
		}

		private void OnTrigger2DEntered(Collider2D collider2D)
		{
			if (collider2D.attachedRigidbody.TryGetComponent(out T projectileSystem))
			{
				DetectedObjects.Add(projectileSystem);
			}
		}

		private void OnTrigger2DExited(Collider2D collider2D)
		{
			if (collider2D.attachedRigidbody.TryGetComponent(out T projectileSystem))
			{
				DetectedObjects.Remove(projectileSystem);
			}
		}

		#endregion
	}
}
