using Projectile;
using Projectile.Pool;
using UnityEngine;

namespace Factories
{
	public class ProjectileFactory : AbstractFactory
	{
		#region SerializeFields

		[SerializeField] private ProjectileSystem m_projectileSystemPrefab;

		#endregion

		#region PublicMethods

		public ProjectileSystem CreateProjectileSystem(
			ProjectilePool projectilePool,
			Transform root
		)
		{
			ProjectileSystem abstractProjectileSystem = Instantiate(m_projectileSystemPrefab, root);

			abstractProjectileSystem
				.Inject(projectilePool)
				.Initialize();

			return abstractProjectileSystem;
		}

		#endregion
	}
}
