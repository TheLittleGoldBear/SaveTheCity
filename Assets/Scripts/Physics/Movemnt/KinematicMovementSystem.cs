using UnityEngine;

namespace Physics
{
	public class KinematicMovementSystem : AbstractRigidbodyMovementSystem
	{
		#region ProtectedMethods

		protected override void MoveToGoalPosition(float deltaTime)
		{
			Transform rigidbodyTransform = m_rigidbody.transform;
			m_rigidbody.MovePosition(rigidbodyTransform.position + m_speed * deltaTime * rigidbodyTransform.up);
		}

		#endregion

		#region UnityMethods

		private void FixedUpdate()
		{
			if (!IsEnabled || m_goalReached)
			{
				return;
			}

			MoveToGoalPosition(Time.deltaTime);
			CheckIfGoalReached();
		}

		#endregion
	}
}
