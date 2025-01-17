using Newtonsoft.Json;

public class NasdaqResponse
{
    [JsonProperty("datatable")]
    public DataTable DataTable { get; set; }
}

public class DataTable
{
    [JsonProperty("data")]
    public List<List<object>> Data { get; set; } // Raw data rows

    [JsonProperty("columns")]
    public List<Column> Columns { get; set; } // Column metadata
}

public class Column
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}
