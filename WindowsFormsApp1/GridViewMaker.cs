using System.Collections.Generic;
using System.Linq;

namespace ReqRaportsApp
{
    public partial class Form1
    {
        public void GridViewPopulate(string[] colNames, List<string[]> rows)
        {
            raportsDataGrid.Rows.Clear();

            raportsDataGrid.ColumnCount = colNames.Count();

            int i = 0;
            foreach (string cn in colNames)
            {
                raportsDataGrid.Columns[i].Name = cn;
                i++;
            }

            foreach (string[] row in rows)
            {
                raportsDataGrid.Rows.Add(row);
            }
        }
    }
}
