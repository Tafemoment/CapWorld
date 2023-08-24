using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Variables and Data
    private enum MenuState
    {
        MainPanel,
        OptionsPanel,
        PlayPanel,
        AreYouSurePanel
    }
    [SerializeField] MenuState _menuState = MenuState.MainPanel;

    public float mouseSense;
    public bool invertMouse;
    [SerializeField] private float gameVolume;
    [SerializeField] private float musicVolume, sfxVolume, lightIntensity;
    [SerializeField] private bool fullscreenToggle;
    private bool showResolution, showQuality;
    private Resolution[] resolutions = new Resolution[10];
    private Vector2 resolutionScrollPosition, qualityScrollPosition;
    private int[] qualityLevelIndex = new int[4];
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private RenderTexture cameraRender;
    [SerializeField] private Light theSun;
    #endregion
    #region Unity Event Behaviours
    private void OnGUI()
    {
        switch (_menuState)
        {
            case MenuState.MainPanel:
                MainPanel();
                break;
            case MenuState.OptionsPanel:
                OptionsPanel();
                break;
            case MenuState.PlayPanel:
                PlayPanel();
                break;
            case MenuState.AreYouSurePanel:
                AreYouSurePanel();
                break;
            default:
                _menuState = MenuState.MainPanel;
                break;
        }
    }
    #endregion
    #region GUI Panels
    void MainPanel()
    {
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "Main Menu Panel");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 0.25f, 8, 2)), "Game Title");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(5, 2.5f, 6, 6.25f)), "Button Box");
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 3.25f, 4, 0.75f)), "Play"))
        {
            _menuState = MenuState.PlayPanel;
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 5.25f, 4, 0.75f)), "Options"))
        {
            _menuState = MenuState.OptionsPanel;
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 7.25f, 4, 0.75f)), "Exit"))
        {
            _menuState = MenuState.AreYouSurePanel;
        }
    }
    void OptionsPanel()
    {
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "Options Panel");
        #region Audio
        //audio settings
        //sliders
        //game volume
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1.25f, 1.4f, 2.5f, 0.4f)), "Game Volume");
        gameVolume = GUI.HorizontalSlider(new Rect(UIHandler.ScreenPlacement(1.25f, 1.8f, 2.5f, 0.25f)), gameVolume,-20, 20);
        
        //music volume
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1.25f, 1.4f, 2.5f, 0.4f)), "Music Volume");
        //SFX volume
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1.25f, 1.4f, 2.5f, 0.4f)), "SFX Volume");
        #endregion
        #region Brightness
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1, 3.3f, 3, 2.3f)), "Render");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1, 3.3f, 3, 1.75f)), cameraRender);
        //Brightness - 3D lighting
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1, 3.3f, 3, 2.3f)), "Brightness");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1.25f, 4.2f, 2.5f, 0.4f)), "Light Intensity");
        lightIntensity = GUI.HorizontalSlider(new Rect(UIHandler.ScreenPlacement(1.25f, 4.6f, 2.5f, 0.25f)), lightIntensity, 0, 1);
        if (lightIntensity != lightIntensity.intensity)
        {
            theSun.intensity = lightIntensity;
        }
        #endregion
        #region Graphics
        //Graphics quality - shadows an stuff
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1, 5.6f, 3, 2.3f)), "Graphics Quality");
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(1.25f,5.9f,2.5f,0.5f)), QualitySettings.names[QualitySettings.GetQualityLevel()]))
        {
            showQuality = !showQuality;
        }
        if (showQuality)
        {
            //start of scroll view
            qualityScrollPosition = GUI.BeginScrollView(
               //first value is the location of the scroll box
               new Rect(UIHandler.ScreenPlacement(1.25f, 6.4f, 2.5f, 1.6f)),
               //second is the currently scrolled position on the X & Y axis of our view
               qualityScrollPosition,
               //third is the size and position of the scrollable area on the screen.
               new Rect(UIHandler.ScreenPlacement(0, 0, 2, 0.5f * qualityLevelIndex.Length)),
               //is the horizontal slider visible
               false,
               //is the verticle slider visible
               true);
            //content within the scrollview
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (GUI.Button(new Rect(UIHandler.ScreenPlacement(0.25f, 0.25f, 2, 0.5f)), QualitySettings.names[i]))
                {
                    //set resolution when clicked
                    QualitySettings.SetQualityLevel(i);
                    showQuality = false;

                }
            }
            GUI.EndScrollView();
        }
        #endregion
        #region Resolution
        //Resolution
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 1, 3, 2.3f)), "Resolution");
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(4.35f, 1.4f, 2.5f, 0.5f)), "Resolution"))
        {
            showResolution = !showResolution;
        }
        if (showResolution)
        {
            GUI.Box(new Rect(UIHandler.ScreenPlacement(4.25f, 1.9f, 2.5f, 1.8f)), "box");
            //scrollview format start
            resolutionScrollPosition = GUI.BeginScrollView(
                // first value is the location of the scroll box
                new Rect(UIHandler.ScreenPlacement(4.25f, 1.9f, 2.5f, 1.6f)),
                //second is the currently scrolled position on the X & Y axis of our view
                resolutionScrollPosition,
                //third is the size and position of the scrollable area on the screen.
                new Rect(UIHandler.ScreenPlacement(0, 0, 2, 0.5f * resolutions.Length)),
                //is the horizontal slider visible
                false,
                //is the verticle slider visible
                true);

         
            GUI.EndScrollView();
        }
        #endregion
        #region Fullscreen
        //Fullscreen/Windowed Toggle
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1, 5.6f, 3, 2.3f)), "Fullscreen");
        fullscreenToggle = GUI.Toggle(new Rect(UIHandler.ScreenPlacement(4.25f, 4.3f, 2.5f, 1f)), fullscreenToggle, "Window Toggle");
        if (fullscreenToggle != Screen.fullScreen)
        {
            Screen.fullScreen = fullscreenToggle;
        }
        #endregion
        #region Mouselooks
        //mouslook sense
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1, 5.6f, 3, 2.3f)), "Mouselook Settings");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4.25f, 6.3f, 2.5f, 0.4f)), "Senstivity");
        mouseSense = GUI.HorizontalSlider(new Rect(UIHandler.ScreenPlacement(4.25f, 6.25f, 2.5f, 0.4f)), MouseLook.sensitivity,5,15);
        #endregion

        //keybnids
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1, 5.6f, 3, 2.3f)), "Keybinds");



        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 7.25f, 4, 0.75f)), "Back"))
        {
            _menuState = MenuState.MainPanel;
        }
    }
    void PlayPanel()
    {
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "Play Panel");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 0.5f, 8, 8)), "Button Box");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(5, 1.25f, 6, 1.5f)), "PlayPanel");

        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 3.5f, 4, 0.75f)), "Continue"))
        {
            Debug.Log("In future we will load our lasted played saved game");
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 4.75f, 4, 0.75f)), "New Game"))
        {
            Debug.Log("Change Scene to Customisation scene and start a new game");
            ChangeSceneByIndexValue(1);
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 6, 4, 0.75f)), "Load"))
        {
            Debug.Log("In future we will load from a list of saved games");
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 7.25f, 4, 0.75f)), "Back"))
        {
            _menuState = MenuState.MainPanel;
        }
    }

    void AreYouSurePanel()
    {
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "Are You Sure Panel");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "AreYouSurePanel");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 2.25f, 8, 4.5f)), "AreYouSurePanel");
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(5f, 4.25f, 2.75f, 0.75f)), "Yes"))
        {
            Debug.Log("Exit");
            ExitToDesktop();
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(8.25f, 4.25f, 2.75f, 0.75f)), "No"))
        {
            _menuState = MenuState.MainPanel;
        }
    }
    #endregion
    #region Scene Management
    void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void ChangeSceneByIndexValue(int sceneIndexValue)
    {
        SceneManager.LoadScene(sceneIndexValue);
    }
    void ExitToDesktop()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion
}
