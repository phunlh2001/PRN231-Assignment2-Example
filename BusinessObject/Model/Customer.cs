﻿namespace BusinessObject.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public Gender Gender { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}
