using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MainManager2 : MonoBehaviour
{
    [SerializeField] private  TMP_InputField nameInput;
    [SerializeField] private TextMeshProUGUI ScoreText;
    public new string name { get; private set; }
    public string displayedName;
    public static MainManager2 instance;

    public int bestScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
        Debug.Log(displayedName);
        Debug.Log(bestScore);
        ScoreText.text = "Best score: " + bestScore + " name: " + displayedName;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();

#else
        Application.Quit(); //original code to quit unity player
    
#endif
    }

    public void ChangeName()
    {
        name = nameInput.text;
        if (displayedName == null)
        {
            displayedName = name;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string displayedName;
        public int bestScore;

    }
    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.displayedName = displayedName;
        data.bestScore = bestScore;
        Debug.Log(data.displayedName);
        Debug.Log(data.bestScore);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log(data.displayedName);
            Debug.Log(data.bestScore);

            displayedName = data.displayedName;
            bestScore = data.bestScore;
        }
    }

}
