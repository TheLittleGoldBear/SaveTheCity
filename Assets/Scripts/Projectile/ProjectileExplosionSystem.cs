using System.Collections.Generic;
using System.Linq;
using Physics.Collisions.DetectionService;
using Physics.Collisions.Relay;
using Projectile.Events;
using UnityEngine;

namespace Projectile
{
	public class ProjectileExplosionSystem
	{
		#region PrivateFields

		private ProjectileDetectionService m_projectileDetectionService;
		private ProjectileEventBus m_projectileEventBus;
		private CollisionRelay m_collisionRelay;
		private bool m_registeredToEvents;

		#endregion

		#region Constructors

		public ProjectileExplosionSystem(
			ProjectileEventBus projectileEventBus,
			CollisionRelay collisionRelay,
			TriggerRelay detectionTriggerRelay
		)
		{
			m_projectileEventBus = projectileEventBus;
			m_collisionRelay = collisionRelay;

			m_projectileDetectionService = new ProjectileDetectionService(detectionTriggerRelay);

			RegisterToEvents();
		}

		#endregion

		#region PublicMethods

		public void OnTearDown()
		{
			m_projectileDetectionService.OnTearDown();

			UnregisterFromEvents();
		}

		public void OnExploded()
		{
			HashSet<ProjectileSystem> detectedProjectileSystems = m_projectileDetectionService.DetectedObjects;

			if (detectedProjectileSystems.Count == 0)
			{
				return;
			}

			List<ProjectileSystem> detectedProjectileSystemsList = detectedProjectileSystems.ToList();

			for (int i = 0; i < detectedProjectileSystemsList.Count; i++)
			{
				detectedProjectileSystemsList[i].Explode();
			}
		}

		#endregion

		#region PrivateMethods

		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_collisionRelay.CollisionEnter2D += OnCollisionEntered;

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_collisionRelay.CollisionEnter2D -= OnCollisionEntered;

			m_registeredToEvents = false;
		}

		private void OnCollisionEntered(Collision2D collision2D)
		{
			m_projectileEventBus.Publish(new ProjectileExplosionEvent());
			OnExploded();
		}

		#endregion
	}
}
