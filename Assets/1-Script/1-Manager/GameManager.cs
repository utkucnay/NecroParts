using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : Singleton<GameManager>
{
    public static readonly Vector3 UpVector = new Vector3(0, 1, 1).normalized;
    public static readonly Vector3 RightVector = new Vector3(1, 0, 0).normalized;
    public static Vector3 Translate3D(Vector2 dir) => new Vector3(dir.x, dir.y, dir.y);

    [SerializeField] bool forceStart;

    public override void Awake()
    {
        base.Awake();
        CommandStream.Init();   
        EventManager.Init();

        EventManager.AddEvent("Start Game");
        EventManager.AddEvent("Pause Game");
        EventManager.AddEvent("Exit Game");

        EventManager.AddEvent("Start Run");
        EventManager.AddEvent("End Run");

        EventManager.AddEventAction("Exit Game", ExitGame);
    }

    private void Start()
    {
        if (forceStart)
            EventManager.InvokeEvent("Start Run");
        else
            UIManager.s_Instance.SetUIScene("MainMenu");
    }

    private void LateUpdate()
    {
        CommandStream.ExecuteCommands();
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
