using AtDomain;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using AtTempleteWeb_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NETCore.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtTempleteWeb_API.AtLogic
{
    public class AtLoginLogic : AtBaseLogic
    {
        public AtLoginLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Hàm trả về Tuper Item =  AccountObject và Item 2: Danh sách Role</returns>
        public async Task<Tuple<AccountObject,List<string>>> Login(string username, string password)
        {
            var hashedPassword = EncryptProvider.Sha1(password);

            var accountObj = await _context.AccountObject.FirstOrDefaultAsync(c => c.UserName == username && c.PassWord == hashedPassword);
            if (accountObj == null)
            {
                return null;
            }

            var roles = await _context.Role_AccountObject
               .Where(h =>
                   h.FK_AccountObject == accountObj.Id
                   && h.AtRowStatus == (int)AtRowStatus.Normal
               )
               .Select(h => h.FK_RoleNavigation.Id)
               .ToListAsync().ConfigureAwait(false);


            await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Login " + accountObj.Id, UserID = accountObj.Id }, AtSerialNoConts.CODE_LOG_LOGIN);

            return new Tuple<AccountObject, List<string>>(accountObj , roles);
        }
    }
}
