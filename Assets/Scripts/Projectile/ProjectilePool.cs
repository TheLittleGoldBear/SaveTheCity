using Factories;
using Pooling;
using UnityEngine;

namespace Projectile
{
	public class ProjectilePool : AbstractPool<ProjectileSystem>
	{
		#region SerializeFields

		[SerializeField] private ProjectileFactory m_projectileFactory;

		#endregion

		#region ProtectedMethods

		protected override ProjectileSystem CreateObjectPool()
		{
			return m_projectileFactory.CreateProjectileSystem(this, m_poolRoot);
		}

		#endregion
	}
}
