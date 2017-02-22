using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Controller : MonoBehaviour {
  public Text solution;
  public Text answer;
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

  private MathExpression expression;
  
  void Start () {
    expression = new MathExpression();
    expression.control = "";

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
  }

  void clear()
  {
    expression.clear();
    updateSolutionText();
  }

  void updateSolutionText()
  {
    solution.text = expression.control;
  }

  void buttonFourPressed()
  {
    if (expression.add("4"))
    {
      updateSolutionText();
      // Check if solution is correct.
    }
    //Debug.Log("4");
  }

  void buttonEqualsPressed()
  {
    double ans = expression.evaluate();
    if(ans % 1 < 0.000001)
    {
      answer.text = "" + (int)ans;
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