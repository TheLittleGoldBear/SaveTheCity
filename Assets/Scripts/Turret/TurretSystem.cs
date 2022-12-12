using Spawners;
using UnityEngine;
using UnityEngine.UI;

namespace Turret
{
	public class TurretSystem : MonoBehaviour
	{
		#region Properties

		public int ProjectileCount { get; private set; }

		#endregion

		#region SerializeFields

		[SerializeField] private Transform m_spawnPosition;
		[SerializeField] private Text m_projectileCountText;

		#endregion

		#region PrivateFields

		private ProjectileSpawner m_projectileSpawner;

		#endregion

		#region PublicMethods

		public TurretSystem Inject(ProjectileSpawner projectileSpawner, int projectileCount)
		{
			m_projectileSpawner = projectileSpawner;
			ProjectileCount = projectileCount;

			return this;
		}

		public void Initialize()
		{
			UpdateProjectileCountText();
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
			UpdateProjectileCountText();
		}

		#endregion

		#region PrivateMethods

		private void UpdateProjectileCountText()
		{
			m_projectileCountText.text = ProjectileCount.ToString();
		}

		#endregion
	}
}
