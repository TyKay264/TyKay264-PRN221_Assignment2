using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public void DeleteSystemAccount(SystemAccount newSystemAccount)
        {
            SystemAccountDAO.DeleteSystemAccount(newSystemAccount);
        }

        public SystemAccount GetSystemAccountById(short accountId)
        {
            return SystemAccountDAO.GetSystemAccountById(accountId);
        }

        public SystemAccount GetSystemAccountByUsername(string accountEmail)
        {
            return SystemAccountDAO.GetSystemAccountByUsername(accountEmail);
        }

        public List<SystemAccount> GetSystemAccounts()
        {
            return SystemAccountDAO.GetSystemAccounts();
        }

        public void SaveSystemAccount(SystemAccount systemAccount)
        {
            SystemAccountDAO.SaveSystemAccount(systemAccount);
        }

        public void UpdateSystemAccount(SystemAccount newSystemAccount)
        {
            SystemAccountDAO.UpdateSystemAccount(newSystemAccount);
        }

        public List<SystemAccount> GetSystemAccountByName(string name)
        {
            return SystemAccountDAO.GetSystemAccountByName(name);
        }
    }
}
