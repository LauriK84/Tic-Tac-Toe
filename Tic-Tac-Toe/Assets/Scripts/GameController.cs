using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject[] TurnIcons; // näyttää kenen vuoro on
    public Sprite[] playerIcons; //  0 = X ja 1 = O merkit
    public Button[] ticTacToe; // pelattavat ruudut
    public Text winnerText;  // voittajateksti
    public GameObject[] lines;  // sisältää pelin voittolinjat
    public Text xScoreText;
    public Text oScoreText;

    public int _whoTurn; // 0 = X ja 1 = O, nolla tarkoittaa X ja ykkönen tarkoittaa O
    public int _countTurn; // laskee vuorot
    public int[] _marked; // merkityt ruudut
    public int _xScore;
    public int _oScore;



    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        _whoTurn = 0;
        _countTurn = 0;
        TurnIcons[0].SetActive(true);
        TurnIcons[1].SetActive(false);
        for(int i=0; i < ticTacToe.Length; i++)
        {
            //nappiin voi vaikuttaa
            ticTacToe[i].interactable = true;
            //asetetaan kuva tyhjäksi
            ticTacToe[i].GetComponent<Image>().sprite = null;
        }

        for (int i = 0; i < _marked.Length; i++)
        {
            _marked[i] = -100;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameButton(int whitchNumber)
    {
        //asetetaan kuva
        ticTacToe[whitchNumber].image.sprite = playerIcons[_whoTurn];
        //Varmistetaan ettei nappiin voida vaikuttaa
        ticTacToe[whitchNumber].interactable = false;

        _marked[whitchNumber] = _whoTurn+1;
        _countTurn++;

        if(_countTurn > 4)
        {
            WinCheck();
        }


        if(_whoTurn == 0)
        {
            _whoTurn = 1;
            TurnIcons[0].SetActive(false);
            TurnIcons[1].SetActive(true);
        }
        else
        {
            _whoTurn = 0;
            TurnIcons[0].SetActive(true);
            TurnIcons[1].SetActive(false);
        }

    }

    void WinCheck()
    {
        int _row1 = _marked[0] + _marked[1] + _marked[2];
        int _row2 = _marked[3] + _marked[4] + _marked[5];
        int _row3 = _marked[6] + _marked[7] + _marked[8];
        int _row4 = _marked[0] + _marked[3] + _marked[6];
        int _row5 = _marked[1] + _marked[4] + _marked[7];
        int _row6 = _marked[2] + _marked[5] + _marked[8];
        int _row7 = _marked[0] + _marked[4] + _marked[8];
        int _row8 = _marked[2] + _marked[4] + _marked[6];
        var _solution = new int[] { _row1, _row2, _row3, _row4, _row5, _row6, _row7, _row8 };

        for(int i=0; i<_solution.Length; i++)
        {
            //jos i on jokaisella rivillä 1 voittaa x 3*1=3, jos jokaisella rivillä on 2 voittaa o 3*2=6 
            if(_solution[i] == 3* (_whoTurn + 1))
            {
                LineDisplay(i);
                return;
            }
        }
    }

    void LineDisplay(int index)
    {
        winnerText.gameObject.SetActive(true);

        if(_whoTurn == 0)
        {
            _xScore++;
            xScoreText.text = _xScore.ToString();
            winnerText.text = "Winner is X player!";
        }
        else if (_whoTurn == 1)
        {
            _oScore++;
            oScoreText.text = _oScore.ToString();
            winnerText.text = "Winner is O player!";
        }

        lines[index].SetActive(true);
        for(int i = 0; i < ticTacToe.Length; i++)
        {
            ticTacToe[i].interactable = false;
        }
    }
    public void Rematch()
    {
        GameSetup();

        for(int i =0;i< lines.Length; i++)
        {
            lines[i].SetActive(false);
        }
    }
    public void Restart()
    {
        Rematch();
        _xScore = 0;
        _oScore = 0;
        xScoreText.text = "0";
        oScoreText.text = "0";
    }
}
