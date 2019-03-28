using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqRaportsApp
{
    public partial class MainForm
    {
        public GridViewData RaportChoiceSwitch()
        {
            string raportType = RaportsComboBox.SelectedItem.ToString();

            string[] cN = { };
            List<List<object>> rW = new List<List<object>>();
            GridViewData gridViewData = new GridViewData(cN, rW);

            if (RaportTypes.clientIdRaportsList.Contains(raportType))
            {
                string currentClientId = ClientIdComboBox.SelectedItem.ToString();

                switch (raportType)
                {
                    case RaportTypes.ReqQuantForClientType:
                        gridViewData = RapGens.ReqQuantForClient(currentClientId);
                        break;

                    case RaportTypes.ReqValueSumForClientType:
                        gridViewData = RapGens.ReqValueSumForClientId(currentClientId);
                        break;

                    case RaportTypes.AllReqsListForClientType:
                        gridViewData = RapGens.ReqsListForClientId(currentClientId);
                        break;

                    case RaportTypes.AverageReqValueForClientType:
                        gridViewData = RapGens.AverageReqValueForClientId(currentClientId);
                        break;

                    case RaportTypes.ReqQuantByProdNameForClientType:
                        gridViewData = RapGens.ReqQuantByNameForClientId(currentClientId);
                        break;
                }
            }
            else
            {
                switch (raportType)
                {
                    case RaportTypes.ReqQuantType:
                        gridViewData = RapGens.ReqQuant();
                        break;
                    
                    case RaportTypes.ReqValueSumType:
                        gridViewData = RapGens.ReqValueSum();
                        break;
                    
                    case RaportTypes.AllReqsListType:
                        gridViewData = RapGens.AllReqsList();
                        break;
                    
                    case RaportTypes.AverageReqValueType:
                        gridViewData = RapGens.AverageReqValue();
                        break;
                    
                    case RaportTypes.ReqQuantByProdNameType:
                        gridViewData = RapGens.ReqQuantByName();
                        break;
                    
                    case RaportTypes.ReqsInValueRangeType:
                        double minValue = Double.Parse(MinValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                        double maxValue = Double.Parse(MaxValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

                        gridViewData = RapGens.ReqsForValueRange(minValue, maxValue);
                        break;
                }
            }
            return gridViewData;
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

            if (RaportsDataGrid.ColumnCount != 0)
            {
                SaveRaportBtn.Enabled = true;
            }
        }
    }
}
