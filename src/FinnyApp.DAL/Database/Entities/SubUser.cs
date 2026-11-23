using Microsoft.SqlServer.Server;
using System.Diagnostics.CodeAnalysis;

namespace FinnyApp.DAL.Database.Entities
{
    public class SubUser
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        [AllowNull]
        public string Name { get; set; }
        public required string PIN  { get; set; }
        public int Finnies {  get; set; }
        public required User User { get; set; }
    }
}
