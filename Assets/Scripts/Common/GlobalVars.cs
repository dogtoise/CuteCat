using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GlobalVars
{
    private static int totalKill;
    public static int TotalKill
    {
        get
        {
            return totalKill;
        }
        set
        {
            totalKill = value;
            StageUIManager.Instance.OnKill_Increased();
        }
    }

    private static float totalPlayTime_Sec = 0;
    public static float TotalPlayTime_Sec
    {
        get 
        {
            return totalPlayTime_Sec;
        }
        set 
        {
            totalPlayTime_Sec = value;
            if (totalPlayTime_Sec >= 60)
            {
                TotalPlayTime_Min++;
                totalPlayTime_Sec = 0;
            }

        }
    }
    private static float totalPlayTime_Min= 0;
    public static float TotalPlayTime_Min
    {
        get { return totalPlayTime_Min; }
        set { totalPlayTime_Min = value; }
    }
}
