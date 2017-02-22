using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MathExpression
{


  public string control;
  public string rpExpression;

  private Stack<string> output;
  private Stack<string> operators;
  private Dictionary<string, int> precedence;
  private Dictionary<string, int> associative;

  public MathExpression()
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
    precedence.Add("s", 5);
    precedence.Add("l", 6);
    precedence.Add("!", 7);

    associative = new Dictionary<string, int>();
    associative.Add("+", 1);
    associative.Add("-", 1);
    associative.Add("x", 1);
    associative.Add("÷", 1);
    associative.Add("^", 2);
    associative.Add("s", 1);
    associative.Add("l", 1);
    associative.Add("!", 1);
  }

  public double evaluate()
  {
    shuntingYard();
    makeDebugString();
    Debug.Log(control + " -> " + rpExpression);

    // Do recursive expression solving
    // Check for () blocks. recall evaluate with contents of block

    return 0.0;
  }

  private void shuntingYard()
  {
    output.Clear();
    operators.Clear();
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
          if (precedence[operators.Peek()] >= precedence[token] && associative[token] == 1/*left associative*/)
          {
            while(precedence[operators.Peek()] >= precedence[token])
            {
              output.Push(operators.Pop());
              if (operators.Count == 0)
              {
                break;
              }
            }
            operators.Push(token);
          }
          else if (precedence[operators.Peek()] > precedence[token] && associative[token] == 2/*right associative*/)
          {
            while (precedence[operators.Peek()] > precedence[token])
            {
              output.Push(operators.Pop());
              if (operators.Count == 0)
              {
                break;
              }
            }
            operators.Push(token);
          }
          else
          {
            operators.Push(token);
          }
        }
        else
        {
          operators.Push(token);
        }
      }
    }
    while (operators.Count > 0)
    {
      output.Push(operators.Pop());
    }
  }

  private void makeDebugString()
  {
    Stack<string> rev = new Stack<string>(output);
    rpExpression = "";
    while(rev.Count > 0)
    {
      rpExpression += rev.Pop() + " ";
    }
  }

  private string getToken(int idx)
  {
    if (isNumberChar(control[idx]))
    {
      string sub = "" + control[idx];
      for (int i = idx + 1; i < control.Length; i++)
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

