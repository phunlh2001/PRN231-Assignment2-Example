using BusinessObject.Model;
using DataAccess.Dao;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer) => CustomerDao.GetInstance.Add(customer);

        public void DeleteCustomer(int id) => CustomerDao.GetInstance.Delete(id);

        public IEnumerable<Customer> GetAll() => CustomerDao.GetInstance.GetAll();

        public Customer GetById(int id) => CustomerDao.GetInstance.GetById(id);

        public void UpdateCustomer(Customer customer) => CustomerDao.GetInstance.Update(customer);
    }
}
