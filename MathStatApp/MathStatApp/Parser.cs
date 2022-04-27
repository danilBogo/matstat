using System.Globalization;
using CsvHelper;

namespace MathStatApp;

public class Parser
{
    private readonly string _pathFile;
    public Parser(string pathFile)
    {
        _pathFile = pathFile;
    }

    public IEnumerable<T> GetData<T>()
    {
        using var reader = new StreamReader(_pathFile);
        using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
        var records = csv.GetRecords<T>();
        foreach (var data in records)
        {
            if (data is not null)
                yield return data;
        }
    }
}