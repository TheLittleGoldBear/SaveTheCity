using Factories;
using UnityEngine;

namespace Projectile.Pool
{
	public class ProjectilePool : AbstractProjectilePool<ProjectileSystem>
	{
		#region SerializeFields

		[SerializeField] private ProjectileFactory m_projectileFactory;

		#endregion

		#region ProtectedMethods

		protected override ProjectileSystem CreateObjectPool()
		{
			return m_projectileFactory.CreateProjectileSystem(
				this,
				m_poolRoot
			);
		}

		#endregion
	}
}
