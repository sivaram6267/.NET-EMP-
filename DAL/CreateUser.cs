using DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   
    public class Employeecls
    {
        private readonly Employee_ProfileEntities _entityObj = null;
        public Employeecls()
        {
            _entityObj = new Employee_ProfileEntities();
        }


        public int AddEmployee(Employee_details _empdetails)
        {
            _entityObj.Employee_details.Add(_empdetails);
           _entityObj.SaveChanges();
            return _empdetails.Emp_Id;
           
        }
    }
}
