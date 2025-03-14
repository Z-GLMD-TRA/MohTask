using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohTask.DataTransferObject.Response
{
    /// <summary>
    /// used to send user information to the presentation layer.
    /// </summary>
    public class UserResult
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public bool Status { get; set; }
    }
}
