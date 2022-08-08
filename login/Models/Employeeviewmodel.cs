using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class Employeeviewmodel
    {
        public int Emp_Id { get; set; }
        public string Emp_Name { get; set; }
        public DateTime Joining_Date { get; set; }
        public DateTime Dob { get; set; }
        public decimal Salary { get; set; }
        public string Practice { get; set; }
        public string Designation_Lancesoft { get; set; }
        public string Desination_at_client { get; set; }
        public string Client_Name { get; set; }
        public decimal Billing { get; set; }
        public DateTime Po_start_Date { get; set; }
        public DateTime Po_end_date { get; set; }
        public int Emp_id_fk { get; set; }

        public  decimal Cubical_cost { get; set; }
        public  decimal Food_cost { get; set; }
        public  decimal Transport_cost { get; set; }
        public int Emp_Id_FK { get; set; }
    }
}