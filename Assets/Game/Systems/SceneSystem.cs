using EZS;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : EZS.System
{
    private SceneComponent[] sceneComponents;

    public enum SceneEvents
    {
        LevelOne,
        LevelTwo,
        LevelThree,
        LevelFour,
        LevelFive,
        MainMenu
    }

    public override void InitSystem()
    {
        RegisterEventEnum<SceneEvents>();
        RegisterComponents<SceneComponent>(s => s.PushComponent(ref sceneComponents), s => s.PopComponent(ref sceneComponents));
    }

    [EnumAction(typeof(SceneEvents))]
    public void InvokeSceneEvent(int e)
    {
        InvokeEventsOnComponents(AsInt(e), sceneComponents);
    }

    protected override void UpdateSystem()
    {
        foreach(SceneComponent sceneComponent in sceneComponents)
        {
            CheckForSceneEvent(sceneComponent, SceneEvents.LevelOne);
            CheckForSceneEvent(sceneComponent, SceneEvents.LevelTwo);
            CheckForSceneEvent(sceneComponent, SceneEvents.LevelThree);
            CheckForSceneEvent(sceneComponent, SceneEvents.LevelFour);
            CheckForSceneEvent(sceneComponent, SceneEvents.LevelFive);
            CheckForSceneEvent(sceneComponent, SceneEvents.MainMenu);
        }
    }

    private void CheckForSceneEvent(SceneComponent sceneComponent, SceneEvents sceneEvent)
    {
        if(sceneComponent.HasEvent(AsInt(sceneEvent)) && sceneComponent.Scene.SceneName.Split('/').Last() == sceneEvent.ToString())
        {
            StartCoroutine(LoadScene(sceneComponent));
        }
    }

    private IEnumerator LoadScene(SceneComponent sceneComponent)
    {
        yield return new WaitForSeconds(sceneComponent.LoadDelay);
        SceneManager.LoadScene(sceneComponent.Scene);
    }
}
