# 2025-06-23    Day - 36    Web-Worker & Unit Testing

## Topics

- Web-workers

- Unit Testing
    - Service Testing
    - Component Testing

- SQL
    - string_to_array
    - array_append
    - setof
    - split_part
    - unnest

- DbContext
    - command

## Short Notes

``` c#
var connection = _context.Database.GetDbConnection();
await connection.OpenAsync();

using var command = connection.CreateCommand();
command.CommandText = "SELECT * FROM process_csv(:csv_input)";

command.CommandType = CommandType.Text;

var param = command.CreateParameter();
param.ParameterName = "csv_input";
param.Value = csvUploadDto.CsvContent;
command.Parameters.Add(param);

using var reader = await command.ExecuteReaderAsync();

var errorRows = new List<string>();
while (await reader.ReadAsync())
{
    errorRows.Add(reader.GetString(0));
}
```

## Links
- https://chatgpt.com/share/68593d9d-78d4-800a-a0ad-d05420aad2ca
- Unit Testing - https://www.youtube.com/playlist?list=PLoC8Q0moRTSiTBAKWBGiJjFUMpiFdaGdF
- Web-Worker - https://medium.com/codex/web-workers-in-angular-99fc4dac1d40
- Github : Web-Worker - https://github.com/gayat19/PresidioMay25/commit/a8989af4cf6213b863c4e2cf24e72dbdef473acc
- Github : Unit Testing (Service) - https://github.com/gayat19/PresidioMay25/commit/ee71eab9900a741edbf25191d60e043d2abcbd3b , https://github.com/gayat19/PresidioMay25/commit/c9ae708d770ee44845d1ea11b5873f7fb2e08999
- Github : Unit Testing (Component) - https://github.com/gayat19/PresidioMay25/commit/fbb25248f0394322479dc9544b5c032d35b08304