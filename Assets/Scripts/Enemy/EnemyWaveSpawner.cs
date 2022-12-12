using System.Collections.Generic;
using Spawners;
using UnityEngine;

namespace Enemy
{
	public class EnemyWaveSpawner : WaveSpawner
	{
		#region SerializeFields

		[SerializeField] private EnemyProjectileSpawner m_enemyProjectileSpawner;

		#endregion

		#region PrivateFields

		private EnemyGoalPositionSystem m_enemyGoalPositionSystem;
		private List<EnemyProjectileSystem> m_spawnedProjectileSystems;

		#endregion

		#region PublicMethods

		public EnemyWaveSpawner Inject(
			EnemyGoalPositionSystem enemyGoalPositionSystem,
			List<EnemyProjectileSystem> spawnedProjectileSystems
		)
		{
			m_enemyGoalPositionSystem = enemyGoalPositionSystem;
			m_spawnedProjectileSystems = spawnedProjectileSystems;

			return this;
		}

		#endregion

		#region ProtectedMethods

		protected override void OnSpawnObject(Vector3 position)
		{
			if (m_enemyGoalPositionSystem.NoValidLocationAvailable())
			{
				return;
			}

			Vector3 goalPosition = m_enemyGoalPositionSystem.GetGoalLocation();
			Vector3 forwardDirection = (goalPosition - position).normalized;

			EnemyProjectileSystem spawnedAbstractProjectileSystem = m_enemyProjectileSpawner.SpawnProjectileSystem(position, forwardDirection, goalPosition);

			m_spawnedProjectileSystems.Add(spawnedAbstractProjectileSystem);
		}

		#endregion
	}
}
