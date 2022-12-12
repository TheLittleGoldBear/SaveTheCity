using Building;
using Enemy;
using Enemy.Events;
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

		#region PrivateFields

		private PointsSystem m_pointsSystem;

		#endregion

		#region UnityMethods

		private void Awake()
		{
			var enemyGoalPositionSystem = new EnemyGoalPositionSystem(m_buildingManager, m_turretManager);
			var enemyProjectileEventBus = new EnemyProjectileEventBus();

			m_pointsSystem = new PointsSystem(
				m_buildingManager,
				m_turretManager,
				m_uiSystem
			);

			m_poolManager
				.Inject(enemyProjectileEventBus)
				.Initialize();

			m_enemyManager
				.Inject(
					enemyGoalPositionSystem,
					enemyProjectileEventBus,
					m_pointsSystem,
					this
				)
				.Initialize();

			m_turretManager.Initialize();
			m_buildingManager.Initialize();
		}

		private void OnDestroy()
		{
			m_buildingManager.OnTearDown();
			m_turretManager.OnTearDown();
			m_enemyManager.OnTearDown();
			m_poolManager.OnTearDown();
		}

		#endregion

		#region PublicMethods

		public void FinishedLevel()
		{
			m_pointsSystem.SummarizePoints();
			m_uiSystem.DisplayGameOverView();
		}

		#endregion
	}
}
