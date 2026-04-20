using Microsoft.AspNetCore.Http;
using evoNaplo.DTO;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace evoNaplo.Services;

public class CsvImportService : ICsvImportService
{
    public List<EvoCampusApplication> ProcessCvsFile(IFormFile file)
    {
        var list = new List<EvoCampusApplication>();
        
        using (var stream = file.OpenReadStream())
        using (var parser = new TextFieldParser(stream))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            if (!parser.EndOfData)
            {
                parser.ReadFields();
            }

            while (!parser.EndOfData)
            {
                string[] values = parser.ReadFields();
                if (values == null) continue;
                
                list.Add(new EvoCampusApplication
                {
                    Timestamp = values.Length > 0 ? values[0] : "",
                    Name = values.Length > 0 ? values[1] : "",
                    Email = values.Length > 0 ? values[2] : "",
                    PhoneNumber = values.Length > 0 ? values[3] : "",
                    Major = values.Length > 0 ? values[4] : "",
                    IsFirstTime = values.Length > 0 ? values[5] : "",
                    Goals = values.Length > 0 ? values[6] : "",
                    //StayInTeam = values.Length > 0 ? values[7] : "",
                    OtherComments = values.Length > 0 ? values[7] : ""
                });
            }
        }
        return list;
    }
}