using Turret;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
	public class InputSystem : MonoBehaviour
	{
		#region Properties

		public bool IsEnabled { get; set; }

		#endregion

		#region SerializeFields

		[SerializeField] private Camera m_camera;
		[SerializeField] private TurretManager m_turretManager;

		#endregion

		#region UnityMethods

		private void Update()
		{
			if (!IsEnabled)
			{
				return;
			}

			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				Vector3 position = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
				position.z = 0.0f;

				m_turretManager.ShootProjectile(position);
			}
		}

		#endregion
	}
}
