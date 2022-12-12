using System.Collections.Generic;
using System.Linq;
using Enemy;
using Physics.Collisions.DetectionService;
using Physics.Collisions.Relay.Collision2D;
using Physics.Collisions.Relay.Trigger2D;
using UnityEngine;

namespace Projectile
{
	public class ProjectileExplosionSystem
	{
		#region PrivateFields

		private ProjectileSystem m_projectileSystem;
		private EnemyProjectileDetectionService m_enemyProjectileDetectionService;
		private Collision2DRelay m_collision2DRelay;
		private bool m_registeredToEvents;

		#endregion

		#region Constructors

		public ProjectileExplosionSystem(ProjectileSystem projectileSystem, Collision2DRelay collision2DRelay, Trigger2DRelay detectionTrigger2DRelay)
		{
			m_projectileSystem = projectileSystem;
			m_collision2DRelay = collision2DRelay;

			m_enemyProjectileDetectionService = new EnemyProjectileDetectionService(detectionTrigger2DRelay);

			RegisterToEvents();
		}

		#endregion

		#region PublicMethods

		public void OnTearDown()
		{
			m_enemyProjectileDetectionService.OnTearDown();

			UnregisterFromEvents();
		}

		public void ActiveExplosion()
		{
			List<EnemyProjectileSystem> detectedProjectileSystemsList =
				m_enemyProjectileDetectionService
					.DetectedObjects
					.ToList();

			for (int i = 0; i < detectedProjectileSystemsList.Count; i++)
			{
				detectedProjectileSystemsList[i].Shooted();
			}

			m_projectileSystem.HitExplosion();
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
