using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ISystemAccountRepository
    {
        SystemAccount GetSystemAccountById(short accountId);
        List<SystemAccount> GetSystemAccounts();
        void UpdateSystemAccount(SystemAccount newSystemAccount);
        void DeleteSystemAccount(SystemAccount newSystemAccount);
        void SaveSystemAccount(SystemAccount systemAccount);
        SystemAccount GetSystemAccountByUsername(string accountEmail);
        List<SystemAccount> GetSystemAccountByName(string name);
    }
}
