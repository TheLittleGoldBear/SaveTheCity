using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class UISystem : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private Button m_restartButton;
		[SerializeField] private Text m_gameOverText;
		[SerializeField] private Text m_pointsText;

		#endregion

		#region PublicMethods

		public void UpdatePoints(int points)
		{
			m_pointsText.text = points.ToString();
		}

		public void DisplayGameOverView()
		{
			m_gameOverText.gameObject.SetActive(true);
			m_restartButton.gameObject.SetActive(true);
		}

		public void OnRestartButtonPressed()
		{
			SceneManager.LoadScene(0);
		}

		#endregion
	}
}
