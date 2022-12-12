using Projectile;
using Projectile.Events;
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
			Transform root,
			ProjectileEventBus projectileEventBus
		)
		{
			ProjectileSystem projectileSystem = Instantiate(m_projectileSystemPrefab, root);

			projectileSystem
				.Inject(projectileEventBus, projectilePool)
				.Initialize();

			return projectileSystem;
		}

		#endregion
	}
}
