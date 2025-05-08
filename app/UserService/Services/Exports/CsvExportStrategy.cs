using System.Text;
using UserService.Domain.Contracts;
using UserService.Domain;

namespace UserService.Services.Exports
{
    public class CsvExportStrategy : IExportStrategy<List<User>>
    {
        public string Export(List<User> users)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Id,Name,Role,Phone");

            foreach (var user in users)
                builder.AppendLine($"{user.Id},{user.Name},{user.Role},{user.Phone}");

            return builder.ToString();
        }

        public string ContentType => "text/csv";
        public string FileExtension => ".csv";
    }

}
