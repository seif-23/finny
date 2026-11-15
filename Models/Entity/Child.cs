using Microsoft.SqlServer.Server;
using System.Diagnostics.CodeAnalysis;

namespace FinnyApp.Models.Entity
{
    public class Child
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        [AllowNull]
        public string Name { get; set; }
        public required string PIN  { get; set; }
        public int Finnies {  get; set; }
        public required Parent Parent { get; set; }
    }
}
