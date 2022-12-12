using UnityEngine;

namespace Physics.Movement
{
	public class Kinematic2DMovementSystem : AbstractRigidbody2DMovementSystem
	{
		#region ProtectedMethods

		protected override void MoveToGoalPosition(float deltaTime)
		{
			Transform rigidbodyTransform = m_rigidbody.transform;
			Vector3 movePosition3D = rigidbodyTransform.position + m_speed * deltaTime * rigidbodyTransform.up;
			var movePosition2D = new Vector2(movePosition3D.x, movePosition3D.y);
			
			m_rigidbody.MovePosition(movePosition2D);
		}

		#endregion

		#region PrivateMethods

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

		#endregion
	}
}
