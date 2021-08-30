using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TowerBlocks
{
	// Window that prompts the user to confirm or reject an action
	public class ModalWindow : MonoBehaviour
	{
		public delegate void WindowClosed(ModalWindow sender, ModalWindowResult result);
		public event WindowClosed OnWindowClosed;

		[SerializeField]
		private Button okButton = default;

		[SerializeField]
		private Button cancelButton = default;

		private void Awake()
		{
			Assert.IsNotNull(okButton);
			Assert.IsNotNull(cancelButton);
		}

		private void Start()
		{
			okButton.onClick.AddListener(OnOkButtonPressed);
			cancelButton.onClick.AddListener(OnCancelButtonPressed);
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}

		private void OnOkButtonPressed()
		{
			OnButtonPressed(ModalWindowResult.OK);
		}

		private void OnCancelButtonPressed()
		{
			OnButtonPressed(ModalWindowResult.CANCEL);
		}

		private void OnButtonPressed(ModalWindowResult result)
		{
			Hide();

			OnWindowClosed?.Invoke(this, result);
		}
	}
}

