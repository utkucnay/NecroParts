using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState : ushort
{
    MainMenu,
    OnRun,
    Pause,
    LevelUp
}

[DefaultExecutionOrder(-100)]
public class GameManager : Singleton<GameManager>
{
    public static readonly Vector3 UpVector = new Vector3(0, 1, 1).normalized;
    public static readonly Vector3 RightVector = new Vector3(1, 0, 0).normalized;
    public static Vector3 Translate3D(Vector2 dir) => new Vector3(dir.x, dir.y, dir.y);

    [SerializeField] bool forceStart;

    GameState gameState = GameState.MainMenu;

    public GameState GameState { get { return gameState;} }

    public override void Awake()
    {
        base.Awake();
        CommandStream.Init();   
        EventManager.Init();

        EventManager.AddEvent("Start Game");
        EventManager.AddEvent("Pause Game");
        EventManager.AddEvent("Resume Game");
        EventManager.AddEvent("Reset Game");
        EventManager.AddEvent("Exit Game");

        EventManager.AddEvent("Start Run");
        EventManager.AddEvent("End Run");
        EventManager.AddEvent("Levelup Game");
        EventManager.AddEvent("Close Levelup Game");

        EventManager.AddEventAction("Exit Game", ExitGame);
        EventManager.AddEventAction("Reset Game", () => Time.timeScale = 1);
        EventManager.AddEventAction("Reset Game", () => AIManager.s_Instance.DestroyAllObject());
        EventManager.AddEventAction("Reset Game", () => EventManager.InvokeEvent("End Run"));
        EventManager.AddEventAction("Pause Game", () => Time.timeScale = 0);
        EventManager.AddEventAction("Pause Game", () => UIManager.s_Instance.SetUIScene("PauseMenu"));

        EventManager.AddEventAction("Resume Game", () => Time.timeScale = 1);
        EventManager.AddEventAction("Resume Game", () => gameState = GameState.OnRun);

        EventManager.AddEventAction("Levelup Game", () => Time.timeScale = 0);
        EventManager.AddEventAction("Levelup Game", () => UIManager.s_Instance.SetUIScene("LevelUpHUD"));
        EventManager.AddEventAction("Levelup Game", () => gameState = GameState.LevelUp);

        EventManager.AddEventAction("Close Levelup Game", () => EventManager.InvokeEvent("Resume Game"));

        EventManager.AddEventAction("Start Game", () => gameState = GameState.MainMenu);
        EventManager.AddEventAction("Resume Game", () => gameState = GameState.OnRun);
        EventManager.AddEventAction("Pause Game", () => gameState = GameState.Pause);
        EventManager.AddEventAction("Reset Game", () => gameState = GameState.MainMenu);

        EventManager.AddEventAction("Start Run", () => gameState = GameState.OnRun);
        EventManager.AddEventAction("Start Run", () => Time.timeScale = 1);
        EventManager.AddEventAction("End Run", () => gameState = GameState.MainMenu);
        EventManager.AddEventAction("End Run", () => Time.timeScale = 0);
        EventManager.AddEventAction("End Run", () => {
            var smcs = Resources.FindObjectsOfTypeAll<SkeletonSMConfig>();
            foreach (var smc in smcs)
            {
                smc.skeletonMultipler = SkeletonMultipler.Init();
            }
        });

        SkeletonUIData.instance.Setup();
    }

    private void Start()
    {
        EventManager.InvokeEvent("Start Game");

#if UNITY_EDITOR
        if (forceStart)
        {
            EventManager.InvokeEvent("Start Run");
            UIManager.s_Instance.SetUIScene("GameHUD");
        }
        else
            UIManager.s_Instance.SetUIScene("MainMenu");
#else   
            UIManager.s_Instance.SetUIScene("MainMenu");
#endif

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && gameState == GameState.OnRun)
        {
            EventManager.InvokeEvent("Pause Game");
        }
    }

    private void LateUpdate()
    {
        CommandStream.ExecuteCommands();
    }

    void OnDestroy()
    {
        SkeletonUIData.instance.ResetSekeleton();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
