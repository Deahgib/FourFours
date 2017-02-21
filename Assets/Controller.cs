using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Controller : MonoBehaviour {
  public Text solution;
  public Button button4;
  public Button buttonPlus;
  public Button buttonMinus;
  public Button buttonTimes;
  public Button buttonDivide;

  private Expression expression;
  
  void Start () {
    expression = new Expression();
    expression.control = "";

    Button b = button4.GetComponent<Button>();
    b.onClick.AddListener(buttonFourPressed);

    b = buttonPlus.GetComponent<Button>();
    b.onClick.AddListener(buttonPlusPressed);

    b = buttonMinus.GetComponent<Button>();
    b.onClick.AddListener(buttonMinusPressed);

    b = buttonTimes.GetComponent<Button>();
    b.onClick.AddListener(buttonTimesPressed);

    b = buttonDivide.GetComponent<Button>();
    b.onClick.AddListener(buttonDividePressed);

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
    
    Debug.Log("4");
    expression.evaluate();
  }

  void buttonPlusPressed()
  {
    if (expression.add("+"))
    { 
      updateSolutionText();
    }
    Debug.Log("+");
  }

  void buttonMinusPressed()
  {
    if (expression.add("-"))
    {
      updateSolutionText();
    }
  }

  void buttonTimesPressed()
  {
    if (expression.add("x"))
    {
      updateSolutionText();
    }
    Debug.Log("x");
  }

  void buttonDividePressed()
  {
    if (expression.add("÷"))
    {
      updateSolutionText();
    }
    Debug.Log("÷");
  }
}

class Expression
{


  public string control;
  public string rpExpression;

  private Stack<string> output;
  private Stack<string> operators;
  private Dictionary<string, int> precedence;

  public Expression()
  {

    rpExpression = "";
    control = "";

    output = new Stack<string>();
    operators = new Stack<string>();
    precedence = new Dictionary<string, int>();
    precedence.Add("+", 2);
    precedence.Add("-", 2);
    precedence.Add("x", 3);
    precedence.Add("÷", 3);
    precedence.Add("^", 4);
    precedence.Add("s", 4);
    precedence.Add("!", 4);
    precedence.Add("l", 4);
  }

  public double evaluate()
  {
    shuntingYard();
    Debug.Log(control + " -> " + rpExpression);



    // Do recursive expression solving
    // Check for () blocks. recall evaluate with contents of block

    return 0.0;
  }

  private void shuntingYard()
  {
    output.Clear();
    operators.Clear();
    rpExpression = "";
    string token = "";
    for (int i = 0; i < control.Length; i += token.Length)
    {
      token = getToken(i);
      if (isNumberString(token))
      {
        output.Push(token);
      }
      else
      {
        if (operators.Count > 0)
        {
          for (int j = 0; j < operators.Count; j++)
          {
            if (operators.Count > 0 && precedence[operators.Peek()] >= precedence[token])
            {
              output.Push(operators.Pop());
            }
            else
            {
              operators.Push(token);
              break;
            }
          }
        }
        else
        {
          operators.Push(token);
        }
      }
    }
    for (int i = 0; i < operators.Count; i++)
    {
      output.Push(operators.Pop());
    }
  }

  private string getToken(int idx)
  {
    if (isNumberChar(control[idx]))
    {
      string sub = "" + control[idx];
      for (int i = idx+1; i < control.Length; i++)
      {
        if (!isNumberChar(control[i]))
        {
          return sub;
        }
        sub += control[i];
      }
    }
    return "" + control[idx];
    
  }

  private bool isNumberString(string s)
  {
    for (int i = 0; i < s.Length; i++)
    {
      if (!isNumberChar(s[i]))
      {
        return false;
      }
    }
    return true;
  }

  private bool isNumberChar(char c)
  {
    if (Char.IsDigit(c))
    {
      return true;
    }
    else if (c == '.')
    {
      return true;
    }
    return false;
  }

  private bool insert(string exprItem)
  {
    control += exprItem;
    //if (output.Length == 0)
    //{
    //  if(Char.IsNumber(exprItem[0]))
    //  {
    //    output += exprItem;
    //  }else
    //  {
    //    return false;
    //  }
    //}
    //else
    //{
    //  if (output[output.Length-1] == ')')
    //  {
    //    if (Char.IsNumber(exprItem[0]))
    //    {
    //      output += "x" + exprItem;
    //    }
    //  }
    //  // Continue to add filters. Allowed character combinations
    //}



    return true;
  }

  public bool add(string exprItem)
  {
    if (!this.insert(exprItem))
    {
      return false;
    }
    return true;
  }
}
