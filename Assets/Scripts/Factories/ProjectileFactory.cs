using Projectile;
using UnityEngine;

namespace Factories
{
	public class ProjectileFactory : AbstractFactory
	{
		#region SerializeFields

		[SerializeField] private ProjectileSystem m_projectileSystemPrefab;

		#endregion

		#region PublicMethods

		public ProjectileSystem CreateProjectileSystem(ProjectilePool projectilePool, Transform root)
		{
			ProjectileSystem projectileSystem = Instantiate(m_projectileSystemPrefab, root);

			projectileSystem.Inject(projectilePool);
			projectileSystem.OnInitialize();

			return projectileSystem;
		}

		#endregion
	}
}
