using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public partial class GridViewHandler
    {
        DataGridView RaportsDataGrid { get; set; }

        public GridViewHandler(DataGridView rDataGrid)
        {
            RaportsDataGrid = rDataGrid;
        }

        public void GridViewPopulate(string[] colNames, List<List<object>> rows)
        {
            RaportsDataGrid.Rows.Clear();

            RaportsDataGrid.ColumnCount = colNames.Count();

            int i = 0;
            foreach (string cn in colNames)
            {
                RaportsDataGrid.Columns[i].Name = cn;
                i++;
            }

            int rowCount = 0;
            foreach (List<object> row in rows)
            {
                RaportsDataGrid.Rows.Add();

                int cellCount = 0;
                foreach (object cellValue in row)
                {
                    RaportsDataGrid.Rows[rowCount].Cells[cellCount].ValueType = cellValue.GetType();
                    RaportsDataGrid.Rows[rowCount].Cells[cellCount].Value = cellValue;

                    cellCount++;
                }
                
                rowCount++;
            }
        }

        public List<string> GatherGridDataToCsv()
        {
            List<string> textData = new List<string>();

            string rowText = string.Empty;
            for (int col = 0; col < RaportsDataGrid.ColumnCount; col++)
            {
                if (col != 0)
                {
                    rowText += ",";
                }
                rowText += RaportsDataGrid.Columns[col].Name;
            }
            textData.Add(rowText);

            for (int row = 0; row < RaportsDataGrid.RowCount; row++)
            {
                rowText = string.Empty;
                for (int cell = 0; cell < RaportsDataGrid.ColumnCount; cell++)
                {
                    if (cell != 0)
                    {
                        rowText += ",";
                    }
                    rowText += RaportsDataGrid.Rows[row].Cells[cell].Value.ToString();
                }
                textData.Add(rowText);
            }
            return textData;
        }
    }
}
