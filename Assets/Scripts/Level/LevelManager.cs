using Building;
using Enemy;
using Projectile.Events;
using Turret;
using UI;
using UnityEngine;

namespace Level
{
	public class LevelManager : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private PoolManager m_poolManager;
		[SerializeField] private TurretManager m_turretManager;
		[SerializeField] private EnemyManager m_enemyManager;
		[SerializeField] private BuildingManager m_buildingManager;
		[SerializeField] private UISystem m_uiSystem;

		#endregion

		#region UnityMethods

		private void Awake()
		{
			var enemyGoalPositionSystem = new EnemyGoalPositionSystem(m_buildingManager, m_turretManager);
			var projectileEventBus = new ProjectileEventBus();
			var enemyProjectileEventBus = new ProjectileEventBus();

			m_pointSystem = new PointSystem(
				m_buildingManager,
				m_turretManager,
				m_uiSystem
			);

			m_poolManager
				.Inject(projectileEventBus, enemyProjectileEventBus)
				.Initialize();

			m_enemyManager
				.Inject(
					enemyGoalPositionSystem,
					enemyProjectileEventBus,
					m_pointSystem,
					this
				)
				.Initialize();

			m_turretManager.Initialize();
			m_buildingManager.Initialize();
		}

		private PointSystem m_pointSystem;

		public void FinishedLevel()
		{
			m_pointSystem.SummarizePoints();
		}

		#endregion
	}
}
