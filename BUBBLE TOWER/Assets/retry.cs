using UnityEngine;
using UnityEngine.UI;

public class RetryButtonActivator : MonoBehaviour
{
    public Button retryButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && retryButton != null)
        {
            retryButton.onClick.Invoke();
        }
    }
}
