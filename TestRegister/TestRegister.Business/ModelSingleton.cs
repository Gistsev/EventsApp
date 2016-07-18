using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRegister.DAL;

namespace TestRegister.Business
{
    public sealed class SessionModelSingleton
    {
        static SessionModelSingleton myInstance = null;
        public int userId;
        public bool isAdmin;
        public IRepository repo;
        public string serverBasePath;

        private SessionModelSingleton()
        {

        }

        public static SessionModelSingleton MyInstance
        {
            get
            {
                if (myInstance == null)
                {
                    myInstance = new SessionModelSingleton();
                }
                return myInstance;
            }
        }
    }
}
