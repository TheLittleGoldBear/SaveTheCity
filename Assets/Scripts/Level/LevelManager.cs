using Building;
using Enemy;
using Enemy.Events;
using Level.Events;
using Turret;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

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
		[SerializeField] private InputSystem m_inputSystem;

		#endregion

		#region PrivateFields

		private PointsSystem m_pointsSystem;
		private LevelEventBus m_levelEventBus;
		private bool m_registeredToEvents;
		
		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_levelEventBus.Subscribe<EndLevelEvent>(OnEndLevelEvent);

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_levelEventBus.Unsubscribe<EndLevelEvent>(OnEndLevelEvent);

			m_registeredToEvents = false;
		}

		private void OnEndLevelEvent(EndLevelEvent endLevelEvent)
		{
			//Inputy
			
			FinishedLevel();
		}

		#endregion

		#region UnityMethods

		private void Awake()
		{
			m_levelEventBus = new LevelEventBus();
			
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
					m_levelEventBus,
					enemyGoalPositionSystem,
					enemyProjectileEventBus,
					m_pointsSystem
				)
				.Initialize();

			m_turretManager.Initialize();
			m_buildingManager.Initialize();

			m_inputSystem.IsEnabled = true;
			
			RegisterToEvents();
		}

		private void OnDestroy()
		{
			m_buildingManager.OnTearDown();
			m_turretManager.OnTearDown();
			m_enemyManager.OnTearDown();
			m_poolManager.OnTearDown();
			
			UnregisterFromEvents();
		}

		#endregion

		#region PublicMethods

		public void FinishedLevel()
		{
			m_inputSystem.IsEnabled = false;
			
			m_pointsSystem.SummarizePoints();
			m_uiSystem.DisplayGameOverView();
		}

		#endregion
	}
}
