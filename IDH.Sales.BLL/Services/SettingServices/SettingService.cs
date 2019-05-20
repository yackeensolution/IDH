using IDH.Sales.BLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDH.Sales.DAL.Own;

namespace IDH.Sales.BLL.Services.SettingServices
{
    public class SettingService
    {
        private readonly UnitOfWork uow;
        public SettingService()
        {
            this.uow = new UnitOfWork();
        }

        public GenericRepository<Setting> Repo
        {
            get { return uow.Repo<Setting>(); }
        }

        public IQueryable<Setting> GetSettings(string _Module)
        {
            return Repo.GetByExression(w => w.Module == _Module);
        }

        public Setting GetSetting(string _Name)
        {
            return Repo.GetByExression(w => w.Name == _Name).FirstOrDefault();
        }

        public  bool UpdateSettingValue(string _SettingName,string _NewValue)
        {
            var set=Repo.GetByExression(w => w.Name == _SettingName).FirstOrDefault();
            if (set!=null)
            {
                set.Value = _NewValue;
                uow.Save();
            }
            return false;
        }
    }
}
