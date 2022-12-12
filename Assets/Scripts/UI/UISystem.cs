using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UISystem : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private Text m_pointsText;

		#endregion

		#region PublicMethods

		public void UpdatePoints(int points)
		{
			m_pointsText.text = points.ToString();
		}

		#endregion
	}
}
