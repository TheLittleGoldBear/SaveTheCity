using System.Collections.Generic;
using Physics.Collisions.Relay;
using UnityEngine;

namespace Physics.Collisions.DetectionService
{
	public abstract class AbstractDetectionService<T> where T : MonoBehaviour
	{
		#region Properties

		public HashSet<T> DetectedObjects { get; }

		#endregion

		#region PrivateFields

		private readonly TriggerRelay m_triggerRelay;
		private bool m_registeredToEvents;

		#endregion

		#region Constructors

		protected AbstractDetectionService(TriggerRelay triggerRelay)
		{
			DetectedObjects = new HashSet<T>();
			m_triggerRelay = triggerRelay;

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

			m_triggerRelay.TriggerEnter2D += OnTriggerEntered;
			m_triggerRelay.TriggerExit2D += OnTriggerExited;

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_triggerRelay.TriggerEnter2D -= OnTriggerEntered;
			m_triggerRelay.TriggerExit2D -= OnTriggerExited;

			m_registeredToEvents = false;
		}

		private void OnTriggerEntered(Collider2D collider2D)
		{
			if (collider2D.attachedRigidbody.TryGetComponent(out T projectileSystem))
			{
				DetectedObjects.Add(projectileSystem);
			}
		}

		private void OnTriggerExited(Collider2D collider2D)
		{
			if (collider2D.attachedRigidbody.TryGetComponent(out T projectileSystem))
			{
				DetectedObjects.Remove(projectileSystem);
			}
		}

		#endregion
	}
}
