using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
    expression.output = "";

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
    solution.text = expression.output;
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


  public string output;
  public string rpExpression;

  public double evaluate()
  {
    parse();
    rpExpression = "";
    convertToRP(output);
    Debug.Log(output + " -> " + rpExpression);
    solve();



    // Do recursive expression solving
    // Check for () blocks. recall evaluate with contents of block

    return 0.0;
  }

  private bool parse()
  {

    return true;
  }

  private void convertToRP(string expr)
  {

    bool blockFound = false;
    int stIdx = 0; ;
    int refCounter = 0;
    for (int i = 0; i < expr.Length; i++)
    {
      if (!blockFound)
      {
        if (expr[i] == '(')
        {
          blockFound = true;
          stIdx = i;
        }
        if (!Char.IsNumber(expr[i])) // Is operator
        {
          if (expr[i] == 's') // One variable for the operator one on the right.
          {
            if (expr[i + 1] == '(')
            {
              blockFound = true;
              stIdx = i + 1;
            }
            else
            {
              rpExpression += expr[i + 1] + " ";
            }
            rpExpression += expr[i] + " ";
          }
          else if (expr[i] == '!') // One variable for the operator one on the left
          {
            if (Char.IsNumber(expr[i - 1]) && rpExpression.Length == 0)
            {
              rpExpression += expr[i-1] + " ";
            }
            rpExpression += expr[i] + " ";
          }
          else // Two varaible for operator one on either side
          {
            if (rpExpression.Length == 0)
            {
              rpExpression += expr[i - 1] + " ";
            }
            if (expr[i + 1] == '(')
            {
              blockFound = true;
              stIdx = i + 1;
            }
            else
            {
              rpExpression += expr[i + 1] + " ";
            }
            rpExpression += expr[i] + " ";
          }
        }
      }
      else if (blockFound)
      {
        if(expr[i] == '(')
        {
          refCounter++;
        }
        else if(expr[i] == ')')
        {
          if (refCounter == 0)
          {
            convertToRP(expr.Substring((stIdx + 1), i - stIdx));
            stIdx = i;
          }
          else
          {
            refCounter--;
          }
        }
      }
    }
  }

  private double solve()
  {
    return 0.0;
  }

  private bool insert(string exprItem)
  {
    output += exprItem;
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
