using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja.Mocking
{
    public interface IEmployeeStorage
    {
        void deleteEmployee(int id);
    }

    public class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext _db;

        public EmployeeStorage()
        {
            _db = new EmployeeContext();
        }
        public void deleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null)
                return;
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}
