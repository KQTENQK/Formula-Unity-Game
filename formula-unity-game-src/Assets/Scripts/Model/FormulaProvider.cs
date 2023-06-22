public static class FormulaProvider
{
    public static readonly string[,] FormulaPairs = new string[,]
    {
         { "sin² α + cos² α", "1" },
         { "tg α", "sin α / cos α" },
         { "ctg α", "cos α / sin α" },
         { "tg α × ctg α", "1" },
         { "1 / cos² α", "tg² α + 1" },
         { "1 / sin² α", "ctg² α + 1" }
    };

    public static int FormulasCount => FormulaPairs.GetLength(0);
    public static int FormulasPartCount => FormulaPairs.GetLength(1);
}
