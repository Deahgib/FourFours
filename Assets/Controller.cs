using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Controller : MonoBehaviour {
  public GameObject successModal;

  public Text solution;
  public Text answer;
  public Text counter;
  public Text target;

  public Text compSolution;
  public Text compTarget;

  public Button button4;
  public Button buttonEquals;
  public Button buttonPlus;
  public Button buttonMinus;
  public Button buttonTimes;
  public Button buttonDivide;
  public Button buttonFactorial;
  public Button buttonPow;
  public Button buttonLog;
  public Button buttonSqrt;
  public Button buttonLPar;
  public Button buttonRPar;
    
  public Button buttonClear;

  public Button compNextLevel;

  private MathExpression expression;

  private GM_Target gameMode;

  private int fours;


  void Start () {
    expression = new MathExpression();
    expression.control = "";
    fours = 4;

    gameMode = new GM_Target();
    
    successModal.SetActive(false);

    updateSolutionText();


    // Event listeners

    Button b = button4.GetComponent<Button>();
    b.onClick.AddListener(buttonFourPressed);

    b = buttonEquals.GetComponent<Button>();
    b.onClick.AddListener(buttonEqualsPressed);

    b = buttonPlus.GetComponent<Button>();
    b.onClick.AddListener(buttonPlusPressed);

    b = buttonMinus.GetComponent<Button>();
    b.onClick.AddListener(buttonMinusPressed);

    b = buttonTimes.GetComponent<Button>();
    b.onClick.AddListener(buttonTimesPressed);

    b = buttonDivide.GetComponent<Button>();
    b.onClick.AddListener(buttonDividePressed);

    b = buttonFactorial.GetComponent<Button>();
    b.onClick.AddListener(buttonFactorialPressed);

    b = buttonPow.GetComponent<Button>();
    b.onClick.AddListener(buttonPowPressed);

    b = buttonLog.GetComponent<Button>();
    b.onClick.AddListener(buttonLogPressed);

    b = buttonSqrt.GetComponent<Button>();
    b.onClick.AddListener(buttonSqrtPressed);

    b = buttonLPar.GetComponent<Button>();
    b.onClick.AddListener(buttonLParPressed);

    b = buttonRPar.GetComponent<Button>();
    b.onClick.AddListener(buttonRParPressed);

    //---
    b = buttonClear.GetComponent<Button>();
    b.onClick.AddListener(clear);

    //---
    b = compNextLevel.GetComponent<Button>();
    b.onClick.AddListener(buttonNextLevelPressed);
  }

  void clear()
  {
    expression.clear();
    fours = 4;
    answer.text = "";
    updateSolutionText();
  }

  void updateSolutionText()
  {
    solution.text = expression.control;
    counter.text = "" + fours;
    target.text = "" + gameMode.getTarget();
  }

  void buttonNextLevelPressed()
  {
    successModal.SetActive(false);
    gameMode.nextLevel();
    clear();

  }

  void buttonFourPressed()
  {
    if (fours > 0)
    {
      if (expression.add("4"))
      {

        fours--;
        updateSolutionText();
        // Check if solution is correct.
      }
    }
    //Debug.Log("4");
  }

  void buttonEqualsPressed()
  {
    double ans = expression.evaluate();
    if(ans % 1 < 0.000001)
    {
      int ians = (int)ans;
      if(fours == 0) { 
        if (gameMode.checkTarget(ians))
        {
          successModal.SetActive(true);
          compSolution.text = expression.control;
          compTarget.text = "" + gameMode.getTarget();
        }
      }

      answer.text = "= " + ians;
    }
    else
    {
      answer.text = "decimal nb";
    }
    
  }

  void buttonPlusPressed()
  {
    if (expression.add("+"))
    { 
      updateSolutionText();
    }
    //Debug.Log("+");
  }

  void buttonMinusPressed()
  {
    if (expression.add("-"))
    {
      updateSolutionText();
    }
    //Debug.Log("-");
  }

  void buttonTimesPressed()
  {
    if (expression.add("x"))
    {
      updateSolutionText();
    }
    //Debug.Log("x");
  }

  void buttonDividePressed()
  {
    if (expression.add("÷"))
    {
      updateSolutionText();
    }
    //Debug.Log("÷");
  }

  void buttonFactorialPressed()
  {
    if (expression.add("!"))
    {
      updateSolutionText();
    }
    //Debug.Log("!");
  }

  void buttonPowPressed()
  {
    if (expression.add("^"))
    {
      updateSolutionText();
    }
    //Debug.Log("^");
  }

  void buttonLogPressed()
  {
    if (expression.add("l"))
    {
      updateSolutionText();
    }
    //Debug.Log("log");
  }

  void buttonSqrtPressed()
  {
    if (expression.add("s"))
    {
      updateSolutionText();
    }
    //Debug.Log("sqrt");
  }

  void buttonLParPressed()
  {
    if (expression.add("("))
    {
      updateSolutionText();
    }
    //Debug.Log("sqrt");
  }

  void buttonRParPressed()
  {
    if (expression.add(")"))
    {
      updateSolutionText();
    }
    //Debug.Log("sqrt");
  }
}