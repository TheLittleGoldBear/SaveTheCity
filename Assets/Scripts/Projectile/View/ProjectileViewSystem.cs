using UnityEngine;

namespace Projectile.View
{
	public class ProjectileViewSystem : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private TrailRenderer m_trailRenderer;

		#endregion

		#region PublicMethods

		public void ClearView()
		{
			m_trailRenderer.Clear();
		}

		#endregion
	}
}
