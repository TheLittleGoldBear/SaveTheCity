using System.Collections.Generic;
using Level;
using Projectile;
using Projectile.Events;
using UnityEngine;

namespace Enemy
{
	public class EnemyManager : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private EnemyWaveSpawner m_enemyWaveSpawner;

		#endregion

		#region PrivateFields

		private List<ProjectileSystem> m_enemyProjectileSystems = new();
		private ProjectileEventBus m_projectileEventBus;
		private PointSystem m_pointSystem;
		//Zmienić na event
		private LevelManager m_levelManager;

		private bool m_registeredToEvents;

		#endregion

		#region PublicMethods

		public EnemyManager Inject(
			EnemyGoalPositionSystem enemyGoalPositionSystem,
			ProjectileEventBus projectileEventBus,
			PointSystem pointSystem,
			LevelManager levelManager
		)
		{
			m_projectileEventBus = projectileEventBus;
			m_pointSystem = pointSystem;
			m_levelManager = levelManager;

			m_enemyWaveSpawner.Inject(enemyGoalPositionSystem, m_enemyProjectileSystems);

			return this;
		}

		public void Initialize()
		{
			m_enemyWaveSpawner.Initialize();

			RegisterToEvents();
		}

		public void OnTearDown()
		{
			UnregisterFromEvents();
		}

		#endregion

		#region PrivateMethods
		
		private void OnProjectileExplosion(ProjectileExplosionEvent projectileExplosionEvent)
		{
			RemoveEnemyProjectileSystem(projectileExplosionEvent.ProjectileSystem);
		}

		private void RemoveEnemyProjectileSystem(ProjectileSystem enemyProjectileSystem)
		{
			m_enemyProjectileSystems.Remove(enemyProjectileSystem);
			
			if (m_enemyProjectileSystems.Count == 0)
			{
				m_levelManager.FinishedLevel();
			}
		}

		private void OnShootedProjectileEvent(ShootedProjectileEvent shootedProjectileEvent)
		
		{
			if (shootedProjectileEvent.ProjectileSystem == null)
			{
				Debug.Log("XD");

				return;
			}

			m_pointSystem.ShootedProjectile();

			RemoveEnemyProjectileSystem(shootedProjectileEvent.ProjectileSystem);
		}

		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_projectileEventBus.Subscribe<ProjectileExplosionEvent>(OnProjectileExplosion);
			m_projectileEventBus.Subscribe<ShootedProjectileEvent>(OnShootedProjectileEvent);

			m_registeredToEvents = true;
		}

	

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_projectileEventBus.Unsubscribe<ProjectileExplosionEvent>(OnProjectileExplosion);
			m_projectileEventBus.Unsubscribe<ShootedProjectileEvent>(OnShootedProjectileEvent);

			m_registeredToEvents = false;
		}

		#endregion
	}
}
