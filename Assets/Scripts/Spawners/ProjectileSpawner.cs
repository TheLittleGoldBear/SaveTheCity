using Projectile;
using Projectile.Events;
using UnityEngine;

namespace Spawners
{
	public class ProjectileSpawner : AbstractSpawner
	{
		#region SerializeFields

		[SerializeField] private ProjectilePool m_projectilePool;

		#endregion

		#region PublicMethods

		public ProjectileSystem SpawnProjectileSystem(
			Vector3 position,
			Vector3 upDirection,
			Vector3 goalPosition
		)
		{
			var projectileSystem = (ProjectileSystem)m_projectilePool.Get();

			projectileSystem.gameObject.SetActive(false);

			Transform projectileSystemTransform = projectileSystem.transform;
			projectileSystemTransform.position = position;
			projectileSystemTransform.up = upDirection;
			projectileSystem.Setup(goalPosition);

			projectileSystem.gameObject.SetActive(true);

			return projectileSystem;
		}

		#endregion
	}
}
