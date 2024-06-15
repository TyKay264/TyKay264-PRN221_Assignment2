using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SystemAccountDAO
    {
        public static SystemAccount GetSystemAccountByUsername(string accountEmail)
        {
            using var context = new FunewsManagementDbContext();
            return context.SystemAccounts.FirstOrDefault(c => c.AccountEmail.Equals(accountEmail));
        }

        public static SystemAccount GetSystemAccountById(short accountId)
        {
            using var context = new FunewsManagementDbContext();
            return context.SystemAccounts.SingleOrDefault(c => c.AccountId.Equals(accountId));
        }

        public static List<SystemAccount> GetSystemAccounts()
        {
            var list = new List<SystemAccount>();
            try
            {
                using var context = new FunewsManagementDbContext();
                list = context.SystemAccounts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static void UpdateSystemAccount(SystemAccount systemAccount)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Entry<SystemAccount>(systemAccount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteSystemAccount(SystemAccount systemAccount)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                var p1 = context.SystemAccounts.SingleOrDefault(c => c.AccountId == systemAccount.AccountId);
                context.SystemAccounts.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SaveSystemAccount(SystemAccount systemAccount)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.SystemAccounts.Add(systemAccount);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<SystemAccount> GetSystemAccountByName(string name)
        {
            using var context = new FunewsManagementDbContext();
            return context.SystemAccounts.Where(c => c.AccountName.Contains(name)).ToList();
        }
    }
}
