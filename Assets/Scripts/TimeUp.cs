using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void Time()
    {
        await Task.Delay(1000);
        SceneManager.LoadScene("ClearScene");
    }
}
