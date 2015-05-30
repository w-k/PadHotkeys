using CsvHelper.Configuration;

namespace GameController2Keys
{
    internal class RowMap : CsvClassMap<Row>
    {
        public RowMap()
        {
            Map(m => m.Trigger).Index(0);
            Map(m => m.Action).Index(1);
        }
    }
}