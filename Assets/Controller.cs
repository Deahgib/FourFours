using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

  public double evaluate()
  {

    // Do recursive expression solving
    // Check for () blocks. recall evaluate with contents of block

    return 0.0;
  }

  private bool parse(string exprItem)
  {
    
    return true;
  }

  public bool add(string exprItem)
  {
    if (!this.parse(exprItem))
    {
      return false;
    }
    output += exprItem;
    return true;
  }
}
