using UnityEngine;

public class FieldGenerator
{
    public Field Generate(Vector2Int fieldSize)
    {
        Cell[,] cells = new Cell[fieldSize.x, fieldSize.y];

        if (fieldSize.x * fieldSize.y != FormulaProvider.FormulasCount * FormulaProvider.FormulasPartCount)
            throw new UnityException($"Field size was not equal to formulas count in {GetType().Name}");

        const int FirstFormulaPartIndex = 0;
        const int SecondFormulaPartIndex = 1;

        int overallLength = fieldSize.x * fieldSize.y;

        int[] cellIndexes = new int[overallLength];

        for (int i = 0; i < overallLength; i++)
            cellIndexes[i] = i;

        const int ShuffleCount = 2;

        for (int i = 0; i < ShuffleCount; i++)
            Shuffle<int>(cellIndexes);

        for (int i = 0; i < overallLength; i++)
        {
            int currentFormulaIndex = (i / FormulaProvider.FormulasPartCount) % FormulaProvider.FormulasCount;

            Block firstBlock = new Block(FormulaProvider.FormulaPairs[currentFormulaIndex, FirstFormulaPartIndex],
                FormulaProvider.FormulaPairs[currentFormulaIndex, SecondFormulaPartIndex]);

            Block secondBlock = new Block(FormulaProvider.FormulaPairs[currentFormulaIndex, SecondFormulaPartIndex],
                FormulaProvider.FormulaPairs[currentFormulaIndex, FirstFormulaPartIndex]);

            int row = cellIndexes[i] / fieldSize.y;
            int column = cellIndexes[i] % fieldSize.y;

            Cell firstCell = new Cell(row, column, firstBlock);

            cells[row, column] = firstCell;
            i++;

            row = cellIndexes[i] / fieldSize.y;
            column = cellIndexes[i] % fieldSize.y;

            Cell secondCell = new Cell(row, column, secondBlock);

            cells[row, column] = secondCell;
        }

        return new Field(fieldSize, cells);
    }

    private void Shuffle<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int j = Random.Range(0, i + 1);

            T temp = array[j];
            array[j] = array[i];
            array[i] = temp;
        }
    }
}
