using Projectile;
using UnityEngine;

namespace Spawners
{
	public class ProjectileSpawner : AbstractSpawner
	{
		#region SerializeFields

		[SerializeField] private ProjectilePool m_projectilePool;

		#endregion

		#region PublicMethods

		public ProjectileSystem SpawnProjectileSystem(Vector3 position, Quaternion rotation, Vector3 goalPosition)
		{
			var projectileSystem = (ProjectileSystem)m_projectilePool.Get();

			projectileSystem.gameObject.SetActive(false);
			projectileSystem.transform.SetPositionAndRotation(position, rotation);
			projectileSystem.Setup(goalPosition);
			projectileSystem.gameObject.SetActive(true);

			return projectileSystem;
		}

		#endregion
	}
}
