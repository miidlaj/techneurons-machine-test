namespace Todo.Models.ViewModels
{

    class EmployeeViewModel
    {
        public List<Employee> EmployeeList {get; set;}

        public Employee Employee {get; set;}


         public int PageIndex { get; set; }
        
        public int PageSize { get; set; }
        
        public int TotalCount { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;
    }
}