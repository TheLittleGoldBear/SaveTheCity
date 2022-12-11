using System;
using Projectile;
using Spawners;
using UnityEngine;

namespace Turret
{
	public class TurretSystem : MonoBehaviour
	{
		private ProjectileSpawner m_projectileSpawner;
		[SerializeField] private Transform m_spawnPosition;
		
		#region Properties

		public int ProjectileCount { get; private set; }

		#endregion

		#region PublicMethods

		public void Inject(ProjectileSpawner projectileSpawner,int projectileCount)
		{
			m_projectileSpawner = projectileSpawner;
			ProjectileCount = projectileCount;
		}

		public void SpawnProjectile(Vector3 goalPosition)
		{
			Vector3 forwardDirection = (goalPosition - m_spawnPosition.position).normalized;
			float angle = Mathf.Acos(forwardDirection.x)*Mathf.Rad2Deg;
			
			Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, angle-90.0f);
			m_projectileSpawner.SpawnProjectileSystem(m_spawnPosition.position, rotation, goalPosition);

			ProjectileCount--;
		}

		#endregion
	}
}
