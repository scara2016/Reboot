using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerMeters playerMeters;
    UIManager uIManager;
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    public float SavedScore=0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerMeters = FindObjectOfType<PlayerMeters>();
        uIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMeters.BlueMeter <= 0 && playerMeters.GreenMeter <= 0)
        {
            Application.Quit();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (uIManager.Score <= 0)
        {
            Application.Quit();
           // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (uIManager.WakeUpTime >= 14)
        {
            SavedScore = uIManager.Score;
        }
    }
}
