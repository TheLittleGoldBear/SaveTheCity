using Spawners;
using UnityEngine;

namespace Turret
{
	public class TurretSystem : MonoBehaviour
	{
		#region Properties

		public int ProjectileCount { get; private set; }

		#endregion

		#region SerializeFields

		[SerializeField] private Transform m_spawnPosition;

		#endregion

		#region PrivateFields

		private ProjectileSpawner m_projectileSpawner;

		#endregion

		#region PublicMethods

		public void Inject(ProjectileSpawner projectileSpawner, int projectileCount)
		{
			m_projectileSpawner = projectileSpawner;
			ProjectileCount = projectileCount;
		}

		public void SpawnProjectile(Vector3 goalPosition)
		{
			Vector3 position = m_spawnPosition.position;
			Vector3 forwardDirection = (goalPosition - position).normalized;

			m_projectileSpawner.SpawnProjectileSystem(
				position,
				forwardDirection,
				goalPosition
			);

			ProjectileCount--;
		}

		#endregion
	}
}
