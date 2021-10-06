using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;// để sử dụng Serializable
// tạo 1 class để quản lí câu hỏi của mình


[Serializable] // thuộc trính dùng để hiển thị class của mình trên inspector
class dataquestion
{
    public string _question;
    public string _answerA;
    public string _answerB;
    public string _answerC;
    public string _answerD;
    public string correctanswer;
}
enum GameState// quản lí trạng thái
{
    home,
    gameplay,
    end,
}
public class Gamemannager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private TextMeshProUGUI question;
    [SerializeField]private TextMeshProUGUI answerA, answerB, answerC, answerD;// để gán câu hỏi
    [SerializeField] private Image imgA, imgB, imgC, imgD;// để gán hình 

    [SerializeField] private dataquestion[] _dataquestion;// mảng câu hỏi
    [SerializeField] private GameObject home, begin, lose;// trạng thái trong game
    [SerializeField] private Sprite imgyellow, imggreen, imgblack;// các hình ảnh 
    private int count, er=3;
    GameState gameState;
    // quản lí âm thanh.
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip m_MusicMainTheme, m_SfxWrongAnswer, m_SfxCorrectAnswer;
    void Start()
    {
        count = 0;
        dataques(0);
        setgamestate(GameState.home);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void checkanswer(string __answer)
    {
        imgA.sprite = imgblack;
        imgB.sprite = imgblack;
        imgC.sprite = imgblack;
        imgD.sprite = imgblack;
        bool status = false;
        if(_dataquestion[count].correctanswer == __answer)
        {
            Debug.Log("trả lời đúng");
            status = true;
            audioSource.PlayOneShot(m_SfxCorrectAnswer);// âm thanh hiệu ứng nên ta sử dụng playoneshot
        }
        else
        {
            Debug.Log("trả lời sai");
            status = false;
            er--;
            audioSource.PlayOneShot(m_SfxWrongAnswer);
            if (er == 0)
            {
                
                setgamestate(GameState.end);
                audioSource.Stop();// cho đùng nhạc khi thua.
            }
        }
        switch (__answer)
        {
            case "a":
               // imgA.color = status ? Color.green : Color.red;
                imgA.sprite = status ? imggreen : imgyellow;
                break;
            case "b":
                //  imgB.color = status ? Color.green : Color.red;
                imgB.sprite = status ? imggreen : imgyellow;
                break;
            case "c":
                imgC.sprite = status ? imggreen : imgyellow;
                // imgC.color = status ? Color.green : Color.red;
                break;
            case "d":
                imgD.sprite = status ? imggreen : imgyellow;
                // imgD.color = status ? Color.green : Color.red;
                break;
        }
        if (status)
        {
            if (count >= _dataquestion.Length)
            {
                Debug.Log("you win");
                return;
            }
          
            Invoke("next", 3f);
        }
    }
    private void dataques(int index)
    {
        if (count < 0 || count >= _dataquestion.Length)
            return;
       /* imgA.color = Color.white;
        imgB.color = Color.white;
        imgC.color = Color.white;
        imgD.color = Color.white;*/
       //reset lại dap án như ban đầu
        imgA.sprite = imgblack;
        imgB.sprite = imgblack;
        imgC.sprite = imgblack;
        imgD.sprite = imgblack;
        question.text ="Câu hỏi: "+ _dataquestion[index]._question;
        answerA.text = "A: "+_dataquestion[index]._answerA;
        answerB.text = "B: "+_dataquestion[index]._answerB;
        answerC.text = "C: "+_dataquestion[index]._answerC;
        answerD.text ="D: "+ _dataquestion[index]._answerD;
    }
    void next()
    {
        count++;
        dataques(count);
    }
    void setgamestate(GameState state)// hàm lấy trạng thái của gamestate 
    {
        gameState = state;// lấy trạng tháy hiện tại.
        home.SetActive(gameState == GameState.home);
        begin.SetActive(gameState == GameState.gameplay);
        lose.SetActive(gameState == GameState.end);
    }
    public void btnbegin()
    {
        er = 3;
        setgamestate(GameState.gameplay);
        dataques(0);
        audioSource.clip = m_MusicMainTheme; // gán nhạc mình cần vào
        audioSource.Play();// cho chạy nhạc
    }
    public void btnlose()
    {
        setgamestate(GameState.home);
    }
    
}

