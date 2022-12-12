using System.Collections.Generic;
using Projectile;
using Spawners;
using UnityEngine;

namespace Enemy
{
	public class EnemyWaveSpawner : WaveSpawner
	{
		#region SerializeFields

		[SerializeField] private ProjectileSpawner m_projectileSpawner;

		#endregion

		#region PrivateFields

		private EnemyGoalPositionSystem m_enemyGoalPositionSystem;
		private List<ProjectileSystem> m_spawnedProjectileSystems;

		#endregion

		#region PublicMethods

		public EnemyWaveSpawner Inject(
			EnemyGoalPositionSystem enemyGoalPositionSystem,
			List<ProjectileSystem> spawnedProjectileSystems
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
			Vector3 goalPosition = m_enemyGoalPositionSystem.GetGoalLocation();
			Vector3 forwardDirection = (goalPosition - position).normalized;

			ProjectileSystem spawnedProjectileSystem = m_projectileSpawner.SpawnProjectileSystem(position, forwardDirection, goalPosition);

			m_spawnedProjectileSystems.Add(spawnedProjectileSystem);
		}

		#endregion
	}
}
