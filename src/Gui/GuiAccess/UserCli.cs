using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS;

namespace GuiAccess
{
    public class UserCli : IceNet
    {
        private UserManagementPrx remote = null;

        public UserCli()
        {
            ProxyKey = "UserAce";
        }

        public override void GetProxy()
        {
            remote = UserManagementPrxHelper.uncheckedCast(m_objectPrx);
        }

        public int Login(string sName, string sHash)
        {
            int nRet = -1;
            try
            { 
                nRet = remote.Login(sName, sHash);
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
                str = "";
            }
           
            return nRet;
        }

        public int Logout(int nSession)
        {
            int nRet = 0;
            try
            {
                remote.begin_Logout(nSession);
            }
            catch (System.Exception /*ex*/)
            {
            	
            }
            return nRet;
        }

        public int CreateUser(string user, string pass, int nRight, int session)
        {
            int nRet = -1;
            try
            {
                nRet = remote.CreateUser(user, pass, nRight, session);
            }
            catch (System.Exception /*ex*/)
            {
                nRet = -1;
            }
            return nRet;
        }

        public int DeleteUser(int nUID, int session)
        {
            int nRet = -1;
            try
            {
                nRet = remote.DeleteUser(nUID, session);
            }
            catch (System.Exception /*ex*/)
            {

            }
            return nRet;
        }

        public MCS.User[] GetUserList(int nBegin, int nCount, int nSession)
        {
            try
            {
                return remote.GetUserList(nBegin, nCount, nSession);
            }
            catch (System.Exception /*ex*/)
            {

            }
            return null;
        }
        public int GetUserCount(int nSession)
        {
            int nRet = -1;
            try
            {
                nRet = remote.GetUserCount(nSession);
            }
            catch (System.Exception /*ex*/)
            {

            }
            return nRet;
        }
        public int SetUserPW(int nUID, string pass, int session)
        {
            int nRet = -1;
            try
            {
                nRet = remote.SetUserPW(nUID, pass, session);
            }
            catch (System.Exception /*ex*/)
            {

            }
            return nRet;
        }
        public int SetUserRight(int nUID, int nRight, int session)
        {
            int nRet = -1;
            try
            {
                nRet = remote.SetUserRight(nUID, nRight, session);
            }
            catch (System.Exception /*ex*/)
            {

            }
            return nRet;
        }
    }
}
