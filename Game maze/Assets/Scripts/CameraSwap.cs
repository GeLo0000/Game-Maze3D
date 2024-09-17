using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject thirdPersonCamera;
    [SerializeField] private Camera firstPersonUICamera;
    [SerializeField] private Camera thirdPersonUICamera;

    [SerializeField] private Canvas[] canvases;
    [SerializeField] private PauseControl pauseControl;
    [SerializeField] private GameObject settingsMenu;


    // ����������� ��� ��������� ������
    public void ActivateFirstPersonView()
    {
        // �������� ������ ������� ����
        firstPersonCamera.SetActive(true);

        // �������� ������ �������� ����
        thirdPersonCamera.SetActive(false);

        SetCanvases(firstPersonUICamera);
    }

    public void ActivateThirdPersonView()
    {
        // ������� ������ �������� ����
        thirdPersonCamera.SetActive(true);

        // �������� ������ ������� ����
        firstPersonCamera.SetActive(false);

        SetCanvases(thirdPersonUICamera);
    }

    private void SetCanvases(Camera UICamera)
    {
        pauseControl.ResumeGame(settingsMenu);
        foreach (Canvas canvas in canvases)
        {
            canvas.worldCamera = UICamera;
        }
    }
}

