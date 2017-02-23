using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MathExpression
{


  public string control;
  public string rpExpression;

  private Stack output;
  private Stack operators;
  private Dictionary<string, int> precedence;
  private Dictionary<string, int> associative;

  public MathExpression()
  {

    rpExpression = "";
    control = "";

    output = new Stack();
    operators = new Stack();
    precedence = new Dictionary<string, int>();
    precedence.Add("+", 2);
    precedence.Add("-", 2);
    precedence.Add("x", 3);
    precedence.Add("÷", 3);
    precedence.Add("^", 4);
    precedence.Add("s", 5);
    precedence.Add("l", 6);
    precedence.Add("!", 7);

    precedence.Add("(", -1);
    precedence.Add(")", -1);

    associative = new Dictionary<string, int>();
    associative.Add("+", 1);
    associative.Add("-", 1);
    associative.Add("x", 1);
    associative.Add("÷", 1);
    associative.Add("^", 2);
    associative.Add("s", 2);
    associative.Add("l", 2);
    associative.Add("!", 2);

    associative.Add("(", -1);
    associative.Add(")", -1);
  }

  public void clear()
  {
    output.Clear();
    operators.Clear();
    control = "";
    rpExpression = "";
  }

  public double evaluate()
  {
    shuntingYard();
    makeDebugString();
    Debug.Log(control + " -> " + rpExpression);
    return solve();
  }

  /* Shunting Yard algorithm explanation:
   * https://en.wikipedia.org/wiki/Shunting-yard_algorithm
   * This creates a reverse polish from a infix (regular) string maths expression
   */
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
        output.Push( Double.Parse( token ) );
      }
      else
      {
        if (operators.Count > 0)
        {
          if (precedence[(string)operators.Peek()] >= precedence[token] && associative[token] == 1/*left associative*/)
          {
            while(precedence[(string)operators.Peek()] >= precedence[token])
            {
              output.Push(operators.Pop());
              if (operators.Count == 0)
              {
                break;
              }
            }
            operators.Push(token);
          }
          else if (precedence[(string)operators.Peek()] > precedence[token] && associative[token] == 2/*right associative*/)
          {
            while (precedence[(string)operators.Peek()] > precedence[token])
            {
              output.Push(operators.Pop());
              if (operators.Count == 0)
              {
                break;
              }
            }
            operators.Push(token);
          }
          else if(token == ")")
          {
            while (operators.Contains("("))
            {
              if ((string)operators.Peek() != "(")
              {
                output.Push(operators.Pop());
              }
              else
              {
                operators.Pop();
                break;
              }
            }
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

  private double solve()
  {
    Stack rpExpr = new Stack(output);
    Stack<double> solve = new Stack<double>();
    double r1, r2;
    while(rpExpr.Count > 0)
    {
      if (rpExpr.Peek() is Double)
      {
        solve.Push( (double) rpExpr.Pop() );
      }
      else
      {
        string op = (string) rpExpr.Pop();
        switch (op)
        {
          case "+":
            r2 = solve.Pop();
            r1 = solve.Pop();
            solve.Push(r1 + r2);
            break;
          case "-":
            r2 = solve.Pop();
            r1 = solve.Pop();
            solve.Push(r1 - r2);
            break;
          case "x":
            r2 = solve.Pop();
            r1 = solve.Pop();
            solve.Push(r1 * r2);
            break;
          case "÷":
            r2 = solve.Pop();
            r1 = solve.Pop();
            solve.Push(r1 / r2);
            break;
          case "^":
            r2 = solve.Pop();
            r1 = solve.Pop();
            solve.Push(Math.Pow(r1, r2));
            break;
          case "s":
            r1 = solve.Pop();
            solve.Push(Math.Sqrt(r1));
            break;
          case "l":
            r2 = solve.Pop();
            r1 = solve.Pop();
            solve.Push(Math.Log(r2, r1));
            break;
          case "!":
            // No factorial yet!
            break;
        }
      }
    }
    if (solve.Count > 1) Debug.Log("ERROR ----  More than 1 number left in the solution");
    return solve.Pop();
  }

  private void makeDebugString()
  {
    Stack rev = new Stack(output);
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
      string sub = "";
      int i = idx;
      while (isNumberChar(control[i]))
      {
        sub += control[i];
        i++;
        if (control.Length == i)
        {
          break;
        }
      }
      return sub;
    }
    else { 
      return "" + control[idx];
    }
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

