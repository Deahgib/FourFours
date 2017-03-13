using UnityEngine;
using System.Collections;

public class GM_Target {
  private uint target;
  private char mode;
  public GM_Target()
  {
    mode = 'c';
    // Look up level progress in a file
    // But for now set target to 0
    target = 0;
  }

  public char getGameMode()
  {
    return mode;
  }

  public uint getTarget()
  {
    return target;
  }

  public bool checkTarget(int ans)
  {
    return target == ans;
  }

  public void nextLevel()
  {
    target++;
  }
}
