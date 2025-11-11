using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    // Singleton pattern
    private static GameEventManager eventInstance;
    public static GameEventManager instance
    {
        get
        {
            if (eventInstance == null)
            {
                eventInstance = GameObject.FindAnyObjectByType<GameEventManager>();
            }
            return eventInstance;
        }
        private set
        {
            eventInstance = value;
        }
    }

    public InteractEvents interactEvents;
    public PhotoEvents photoEvents;
    public NotebookEvents notebookEvents;
    public TaskEvents taskEvents;

    // Create a new instances? of these scripts (they hold events)
    private GameEventManager()
    {
        interactEvents = new InteractEvents();
        photoEvents = new PhotoEvents();
        notebookEvents = new NotebookEvents();
        taskEvents = new TaskEvents();
    }
}
