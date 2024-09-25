using System;

namespace MiniProject
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public string Category { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class Loan
    {
        public int CustomerId { get; set; }
        public string BookISBN { get; set; }
        public DateTime DueDate { get; set; }
    }
}
