
using CsvDataAccess.CsvReading;
using CsvDataAccess.Interface;


namespace CsvDataAccess.NewSolution;

public class FastTableDataBuilder : ITableDataBuilder
{
    public ITableData Build(CsvData csvData)
    {
        var resultRows = new List<FastRow>();

        foreach (var row in csvData.Rows)
        {
            var newRow = new FastRow();

            for (int columnIndex = 0; columnIndex < csvData.Columns.Length; ++columnIndex)
            {
                var columnName = csvData.Columns[columnIndex];
                string valueAsString = row[columnIndex];

                if(string.IsNullOrEmpty(valueAsString))
                {
                    continue;
                }
                else if (valueAsString == "TRUE")
                {
                    newRow.AssignCells(columnName, true);
                }
                else if (valueAsString == "FALSE")
                {
                    newRow.AssignCells(columnName, false);

                }
                else if (valueAsString.Contains(".") && decimal.TryParse(valueAsString, out var valueAsDecimal))
                {
                    newRow.AssignCells(columnName, valueAsDecimal);

                }
                else if (int.TryParse(valueAsString, out var valueAsInt))
                {
                    newRow.AssignCells(columnName, valueAsInt);

                }
                else
                {
                    newRow.AssignCells(columnName, valueAsString);

                }

            }

            resultRows.Add(newRow);
        }

        return new FastTableData(csvData.Columns, resultRows);
    }

    
}
