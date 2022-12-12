using UnityEngine;

namespace Physics
{
	public abstract class AbstractRigidbody2DMovementSystem : AbstractMovementSystem
	{
		#region Constants

		private const float SQUARE_DISTANCE_THRESHOLD = 0.01f;

		#endregion

		#region SerializeFields

		[SerializeField] protected Rigidbody2D m_rigidbody;

		#endregion

		#region ProtectedMethods

		protected override void CheckIfGoalReached()
		{
			if (m_goalReached)
			{
				return;
			}

			Vector3 position = m_rigidbody.transform.position;
			float xDistance = position.x - m_goalPosition.x;
			float zDistance = position.z - m_goalPosition.z;

			if (xDistance * xDistance + zDistance * zDistance <= SQUARE_DISTANCE_THRESHOLD)
			{
				OnGoalReached();
			}
		}

		#endregion
	}
}
