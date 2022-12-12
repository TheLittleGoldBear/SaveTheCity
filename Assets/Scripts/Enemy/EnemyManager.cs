using System.Collections.Generic;
using Enemy.Events;
using Level;
using UnityEngine;

namespace Enemy
{
	public class EnemyManager : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private EnemyWaveSpawner m_enemyWaveSpawner;

		#endregion

		#region PrivateFields

		private List<EnemyProjectileSystem> m_enemyProjectileSystems = new();
		private EnemyProjectileEventBus m_enemyProjectileEventBus;
		private PointsSystem m_pointsSystem;
		private LevelManager m_levelManager;

		private bool m_registeredToEvents;

		#endregion

		#region PublicMethods

		public EnemyManager Inject(
			EnemyGoalPositionSystem enemyGoalPositionSystem,
			EnemyProjectileEventBus enemyProjectileEventBus,
			PointsSystem pointsSystem,
			LevelManager levelManager
		)
		{
			m_enemyProjectileEventBus = enemyProjectileEventBus;
			m_pointsSystem = pointsSystem;
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
			m_enemyWaveSpawner.OnTearDown();

			UnregisterFromEvents();
		}

		#endregion

		#region PrivateMethods

		private void OnProjectileExplosion(EnemyProjectileExplosionEvent enemyProjectileExplosionEvent)
		{
			RemoveEnemyProjectileSystem(enemyProjectileExplosionEvent.EnemyProjectileSystem);
		}

		private void RemoveEnemyProjectileSystem(EnemyProjectileSystem enemyAbstractProjectileSystem)
		{
			m_enemyProjectileSystems.Remove(enemyAbstractProjectileSystem);

			if (m_enemyProjectileSystems.Count == 0)
			{
				m_levelManager.FinishedLevel();
			}
		}

		private void OnShootedProjectileEvent(ShootedEnemyProjectileEvent shootedEnemyProjectileEvent)
		{
			if (shootedEnemyProjectileEvent.EnemyProjectileSystem == null)
			{
				Debug.Log("XD");

				return;
			}

			m_pointsSystem.ShootedProjectile();

			RemoveEnemyProjectileSystem(shootedEnemyProjectileEvent.EnemyProjectileSystem);
		}

		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_enemyProjectileEventBus.Subscribe<EnemyProjectileExplosionEvent>(OnProjectileExplosion);
			m_enemyProjectileEventBus.Subscribe<ShootedEnemyProjectileEvent>(OnShootedProjectileEvent);

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_enemyProjectileEventBus.Unsubscribe<EnemyProjectileExplosionEvent>(OnProjectileExplosion);
			m_enemyProjectileEventBus.Unsubscribe<ShootedEnemyProjectileEvent>(OnShootedProjectileEvent);

			m_registeredToEvents = false;
		}

		#endregion
	}
}
