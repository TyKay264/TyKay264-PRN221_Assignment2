using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository iSystemAccountRepository;
        private static short counter = 0;
        public SystemAccountService ()
        {
            iSystemAccountRepository = new SystemAccountRepository ();
        }

        public void DeleteSystemAccount(SystemAccount newSystemAccount)
        {
            iSystemAccountRepository.DeleteSystemAccount (newSystemAccount);
        }

        public SystemAccount GetSystemAccountById(short accountId)
        {
            return iSystemAccountRepository.GetSystemAccountById (accountId);
        }

        public List<SystemAccount> GetSystemAccounts()
        {
            return iSystemAccountRepository.GetSystemAccounts();
        }

        public void SaveSystemAccount(SystemAccount newSystemAccount)
        {
            short nextId = (short)(counter + 1);

            while (iSystemAccountRepository.GetSystemAccountById(nextId) != null)
            {
                nextId++;

                if (nextId == short.MaxValue)
                {
                    throw new InvalidOperationException("No available ID for SystemAccount");
                }
            }

            newSystemAccount.AccountId = nextId;
            iSystemAccountRepository.SaveSystemAccount(newSystemAccount);
        }


        public void UpdateSystemAccount(SystemAccount newSystemAccount)
        {
            iSystemAccountRepository.UpdateSystemAccount (newSystemAccount);
        }

        public SystemAccount GetSystemAccountByUsername(string accountEmail)
        {
            return iSystemAccountRepository.GetSystemAccountByUsername (accountEmail);
        }

        public List<SystemAccount> GetSystemAccountByName(string name)
        {
            return iSystemAccountRepository.GetSystemAccountByName(name);
        }
    }
}
