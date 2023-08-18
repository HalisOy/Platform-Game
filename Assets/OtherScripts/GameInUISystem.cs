using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInUISystem : MonoBehaviour
{
    [SerializeField] private Animator CutSceneAnim;

    private void Start()
    {
        CutSceneAnim = transform.GetChild(transform.childCount - 1).GetComponent<Animator>();
    }
    public void TimeScale()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        StartCoroutine(RestartDelay());
    }

    public void Finish()
    {
        StartCoroutine(FinishUpLevel());
    }
    public void ChangeScene(string LoadScene)
    {
        StartCoroutine(ChangeSceneDelay(LoadScene));
    }

    private IEnumerator RestartDelay()
    {
        CutSceneAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator FinishUpLevel()
    {
        CutSceneAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        string LevelName = SceneManager.GetActiveScene().name;
        int Level = Convert.ToInt32(LevelName.Substring(5, 1));
        Level++;
        string NextLevel = "Level" + Level;
        SceneManager.LoadScene(NextLevel);
    }

    private IEnumerator ChangeSceneDelay(string LoadScene)
    {
        if (LoadScene != "")
        {
            CutSceneAnim.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(LoadScene);
        }
        else
            Debug.Log("Çok Yakýnda Hizmetinizde.");
    }
}
