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


    // ¬икликаЇтьс€ при натисканн≥ кнопки
    public void ActivateFirstPersonView()
    {
        // јктивуЇмо камери першого лиц€
        firstPersonCamera.SetActive(true);

        // ¬имикаЇмо камери третього лиц€
        thirdPersonCamera.SetActive(false);

        SetCanvases(firstPersonUICamera);
    }

    public void ActivateThirdPersonView()
    {
        // ¬микаЇмо камери третього лиц€
        thirdPersonCamera.SetActive(true);

        // ¬имикаЇмо камери першого лиц€
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

