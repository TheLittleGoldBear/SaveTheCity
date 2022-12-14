using Projectile;
using Projectile.Pool;
using UnityEngine;

namespace Spawners
{
	public class ProjectileSpawner : AbstractProjectileSpawner
	{
		#region SerializeFields

		[SerializeField] private ProjectilePool m_enemyProjectilePool;

		#endregion

		#region PublicMethods

		public ProjectileSystem SpawnProjectileSystem(
			Vector3 position,
			Vector3 upDirection,
			Vector3 goalPosition
		)
		{
			var projectileSystem = (ProjectileSystem)m_enemyProjectilePool.Get();

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
