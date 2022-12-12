using Projectile;
using UnityEngine;

namespace Spawners
{
	public abstract class AbstractProjectileSpawner : AbstractSpawner
	{
		#region ProtectedMethods

		protected static void Setup(
			AbstractProjectileSystem projectileSystem,
			Vector3 position,
			Vector3 upDirection,
			Vector3 goalPosition
		)
		{
			projectileSystem.gameObject.SetActive(false);

			Transform projectileSystemTransform = projectileSystem.transform;
			projectileSystemTransform.position = position;
			projectileSystemTransform.up = upDirection;
			projectileSystem.Setup(goalPosition);

			projectileSystem.gameObject.SetActive(true);
		}

		#endregion
	}
}
