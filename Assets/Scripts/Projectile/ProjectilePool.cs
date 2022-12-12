using Factories;
using Pooling;
using Projectile.Events;
using UnityEngine;

namespace Projectile
{
	public class ProjectilePool : AbstractPool<ProjectileSystem>
	{
		#region SerializeFields

		[SerializeField] private ProjectileFactory m_projectileFactory;

		#endregion

		#region PrivateFields

		private ProjectileEventBus m_projectileEventBus;

		#endregion

		#region PublicMethods

		public ProjectilePool Inject(ProjectileEventBus projectileEventBus)
		{
			m_projectileEventBus = projectileEventBus;

			return this;
		}

		#endregion

		#region ProtectedMethods

		protected override ProjectileSystem CreateObjectPool()
		{
			return m_projectileFactory.CreateProjectileSystem(
				this,
				m_poolRoot,
				m_projectileEventBus
			);
		}

		#endregion
	}
}
