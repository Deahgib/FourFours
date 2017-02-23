using UnityEngine;
using System.Collections;

public class GM_Target {
  private int target;

  public GM_Target()
  {
    // Look up level progress in a file
    // But for now set target to 0
    target = 0;
  }

  public int getTarget()
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
