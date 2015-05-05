using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Project.Contract;
using Project.Website.ServiceAccess.Service;
namespace Project.Website.ServiceAccess
{
    public class BaseContainer
    {
        #region exception
        protected void AddOrEditExceptionHandle<T>(Action<Proxy, T> action, Proxy p, T v) where T : class
        {
            try
            {
                action.Invoke(p, v);
            }
            catch (Exception)
            {
                throw new Exception("fail to add or edit.");
            }
            finally
            {
                p.Close();
            }
        }
        protected void DeleteExceptionHandle(Action<Proxy, int> action, Proxy p, int id)
        {
            try
            {
                action.Invoke(p, id);
            }
            catch (Exception)
            {
                throw new Exception("fail to delete.");
            }
            finally
            {
                p.Close();
            }
        }
        protected T GetDataByConditionExceptionHandle<T>(Func<Proxy, T, T> func, Proxy p, T c) where T : class
        {
            try
            {
                return func.Invoke(p, c);
            }
            catch (FaultException<CustomizedException> ex)
            {
                throw new Exception(string.Format("Message:{0}; Datetime:{1}; From User:{2}", ex.Detail.Message, ex.Detail.When, ex.Detail.User));
            }
            finally
            {
                p.Close();
            }
        }
        protected List<T> GetDataExceptionHandle<T>(Func<Proxy, List<T>> func, Proxy p) where T : class
        {
            try
            {
                return func.Invoke(p);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                p.Close();
            }
        }
        #endregion
    }
}
