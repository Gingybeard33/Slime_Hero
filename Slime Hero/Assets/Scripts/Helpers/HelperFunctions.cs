using UnityEngine;

public class HelperFunctions
{
    public static string GetLocationOctant(float x, float y)
    {
        string retVal = "NULL";

        if(x >= -0.5 * y && x < 0.5 * y)
        {
            // Up
            retVal = "UP";
        }
        else if(x >= 0.5 * y && x < 2 * y)
        {
            // Up Right
            retVal = "UP RIGHT";

        }
        else if(x >= 2 * y && x > -2 * y)
        {
            // Right
            retVal = "RIGHT";
        }
        else if(x <= -2 * y && x > -0.5 * y)
        {
            // Down Right
            retVal = "DOWN RIGHT";
        }
        else if(x <= -0.5 * y && x > 0.5 * y)
        {
            // Down
            retVal = "DOWN";
        }
        else if(x <= 0.5 * y && x > 2 * y)
        {
            // Down Left
            retVal = "DOWN LEFT";
        }
        else if(x <= 2 * y && x < -2 * y)
        {
            // Left
            retVal = "LEFT";
        }
        else if(x >= -2 * y && x < -0.5 * y)
        {
            // Up Left
            retVal = "UP LEFT";

        }

        return retVal;
    }
}