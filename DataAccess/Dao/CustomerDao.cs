using BusinessObject.Data;
using BusinessObject.Model;

namespace DataAccess.Dao
{
    public class CustomerDao
    {
        //Singleton
        private static CustomerDao? instance;
        private static readonly object instanceLock = new object();
        public static CustomerDao GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDao();
                    }
                    return instance;
                }
            }
        }

        /** 
         * [List<Customer>]
         * Get All Customers
        */
        public IEnumerable<Customer> GetAll()
        {
            var cus = new List<Customer>();
            try
            {
                using var context = new ApplicationDbContext();
                cus = context.Customers.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cus;
        }

        /** 
         * [Customer]
         * Get Customer By Id
        */
        public Customer GetById(int id)
        {
            Customer? cus;
            try
            {
                using var context = new ApplicationDbContext();
                cus = context.Customers.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cus!;
        }

        /** 
         * [void]
         * Add A Customer
        */
        public void Add(Customer customer)
        {
            try
            {
                Customer cus = GetById(customer.Id);
                if (cus == null)
                {
                    using var context = new ApplicationDbContext();
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Customer is already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /** 
         * [void]
         * Update A Customer By Id
        */
        public void Update(Customer customer)
        {
            try
            {
                Customer cus = GetById(customer.Id);
                if (cus != null)
                {
                    using var context = new ApplicationDbContext();
                    context.Customers.Update(customer);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Customer does not already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /** 
         * [void]
         * Delete A Customer By Id
        */
        public void Delete(int id)
        {
            try
            {
                Customer cus = GetById(id);
                if (cus != null)
                {
                    using var context = new ApplicationDbContext();
                    context.Customers.Remove(cus);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Customer is already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
