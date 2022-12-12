using System.Collections.Generic;
using System.Linq;
using Physics.Collisions.DetectionService;
using Physics.Collisions.Relay.Collision2D;
using Physics.Collisions.Relay.Trigger2D;
using Projectile.Events;
using UnityEngine;

namespace Projectile
{
	public class ProjectileExplosionSystem
	{
		#region PrivateFields

		private ProjectileDetectionService m_projectileDetectionService;
		private Collision2DRelay m_collision2DRelay;
		private bool m_registeredToEvents;

		#endregion

		#region Constructors

		public ProjectileExplosionSystem(Collision2DRelay collision2DRelay, Trigger2DRelay detectionTrigger2DRelay)
		{
			m_collision2DRelay = collision2DRelay;

			m_projectileDetectionService = new ProjectileDetectionService(detectionTrigger2DRelay);

			RegisterToEvents();
		}

		#endregion

		#region PublicMethods

		public void OnTearDown()
		{
			m_projectileDetectionService.OnTearDown();

			UnregisterFromEvents();
		}

		public void ActiveExplosion()
		{
			List<ProjectileSystem> detectedProjectileSystemsList =
				m_projectileDetectionService
					.DetectedObjects
					.ToList();

			for (int i = 0; i < detectedProjectileSystemsList.Count; i++)
			{
				detectedProjectileSystemsList[i].Shooted();
				detectedProjectileSystemsList[i].HitExplosion();
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

			m_collision2DRelay.CollisionEnter2D += OnCollision2DEntered;

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_collision2DRelay.CollisionEnter2D -= OnCollision2DEntered;

			m_registeredToEvents = false;
		}

		private void OnCollision2DEntered(Collision2D collision2D)
		{
			ActiveExplosion();
		}

		#endregion
	}
}
