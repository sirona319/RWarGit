using UnityEngine;

public static class Lib
{

    public static bool RanKUp(ref string rank)
    {
        if (rank == "S")
            return false;
        else if (rank == "A")
            rank = "S";
        else if (rank == "B")
            rank = "A";
        else if (rank == "C")
            rank = "B";
        else
            Debug.Log("ランクエラー　:");

        return true;
    }

    public static bool RanKDown(ref string rank)
    {
        if (rank == "S")
            rank = "A";
        else if (rank == "A")
            rank = "B";
        else if (rank == "B")
            rank = "C";
        else if (rank == "C")
            return false;
        else
            Debug.Log("ランクエラー　:");

        return true;
    }
}
