using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public partial class Form1
    {
        public void GridViewPopulate(string[] colNames, List<List<object>> rows)
        {
            raportsDataGrid.Rows.Clear();

            raportsDataGrid.ColumnCount = colNames.Count();

            int i = 0;
            foreach (string cn in colNames)
            {
                raportsDataGrid.Columns[i].Name = cn;
                i++;
            }

            int rowCount = 0;
            foreach (List<object> row in rows)
            {
                raportsDataGrid.Rows.Add();

                int cellCount = 0;
                foreach (object cellValue in row)
                {
                    raportsDataGrid.Rows[rowCount].Cells[cellCount].ValueType = cellValue.GetType();
                    raportsDataGrid.Rows[rowCount].Cells[cellCount].Value = cellValue;

                    cellCount++;
                }
                
                rowCount++;
            }
        }

        public List<string> GatherGridDataToCsv()
        {
            List<string> textData = new List<string>();

            string rowText = string.Empty;
            for (int col = 0; col < raportsDataGrid.ColumnCount; col++)
            {
                if (col != 0)
                {
                    rowText += ", ";
                }
                rowText += raportsDataGrid.Columns[col].Name;
            }
            textData.Add(rowText);

            for (int row = 0; row < raportsDataGrid.RowCount; row++)
            {
                rowText = string.Empty;
                for (int cell = 0; cell < raportsDataGrid.ColumnCount; cell++)
                {
                    if (cell != 0)
                    {
                        rowText += ", ";
                    }
                    rowText += raportsDataGrid.Rows[row].Cells[cell].Value.ToString();
                }
                textData.Add(rowText);
            }
            return textData;
        }
    }
}
