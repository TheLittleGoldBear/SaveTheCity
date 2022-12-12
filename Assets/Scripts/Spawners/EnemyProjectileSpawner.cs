using Enemy;
using Enemy.Pool;
using UnityEngine;

namespace Spawners
{
	public class EnemyProjectileSpawner : AbstractProjectileSpawner
	{
		#region SerializeFields

		[SerializeField] private EnemyProjectilePool m_enemyProjectilePool;

		#endregion

		#region PublicMethods

		public EnemyProjectileSystem SpawnProjectileSystem(
			Vector3 position,
			Vector3 upDirection,
			Vector3 goalPosition
		)
		{
			var projectileSystem = (EnemyProjectileSystem)m_enemyProjectilePool.Get();

			Setup(
				projectileSystem,
				position,
				upDirection,
				goalPosition
			);

			return projectileSystem;
		}

		#endregion
	}
}
