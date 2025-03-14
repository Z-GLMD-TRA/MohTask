using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohTask.DataTransferObject.Request
{
    /// <summary>
    /// used to send user information to the domain layer.
    /// </summary>
    public class UserCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
