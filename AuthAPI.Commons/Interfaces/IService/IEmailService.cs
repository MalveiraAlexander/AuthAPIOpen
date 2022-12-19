using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Interfaces.IService
{
    public interface IEmailService
    {
        Task SendRecoveryAsync(string emailTo, string name, string lastname, string url, string subject);
    }
}
