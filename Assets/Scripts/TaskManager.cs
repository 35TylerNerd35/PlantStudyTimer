using System.Collections.Generic;
using UnityEngine;
using static GameEvent;
using TMPro;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [SerializeField] GameObject taskPrefab;
    [SerializeField] Transform taskContainer;

    List<string> tasks = new();
    List<bool> isCompletedTask = new();

    public void AddTask(TMP_InputField inputField)
    {
        // Add variables
        tasks.Add(inputField.text);
        isCompletedTask.Add(false);

        // Display task
        GameObject newTaskObject = Instantiate(taskPrefab);
        newTaskObject.GetComponentInChildren<Text>().text = inputField.text;

        // Position task
        newTaskObject.transform.SetParent(taskContainer);
        newTaskObject.transform.localPosition = new Vector3(10, 400-((tasks.Count+1) * 55), 0);

        // Add event listener
        int index = tasks.Count - 1;
        newTaskObject.GetComponentInChildren<Toggle>().onValueChanged.AddListener((val) => OnToggleChanged(val, index));
        UpdatePomodoro();

        // Clear input field
        inputField.text = "";
    }

    public void OnToggleChanged(bool val, int index)
    {
        isCompletedTask[index] = val;
        UpdatePomodoro();
    }

    void UpdatePomodoro()
    {
        List<string> completed = new();
        List<string> incompleted = new();

        // Setup lists
        for (int i = 0; i < tasks.Count; i++)
        {
            // Keep track of completed tasks
            if (isCompletedTask[i])
            {
                completed.Add(tasks[i]);
            }
            else
            {
                incompleted.Add(tasks[i]);
            }
        }

        PomodoroManager.currentPomodoro.completedTasks = completed;
        PomodoroManager.currentPomodoro.incompleteTasks = incompleted;
    }
}
