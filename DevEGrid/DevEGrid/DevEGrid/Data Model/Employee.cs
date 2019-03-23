using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DevEGrid
{
    #region #Employee
    public enum AccessLevel
    {
        Admin,
        User
    }
    public class Employee : ModelObject
    {
        string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (Photo == null)
                {
                    var val = value.Replace(" ",string.Empty);
                    Photo = ImageSource.FromResource(val);
                    
                }
                    
            }
        }

        public Employee(string name)
        {
            this.Name = name;
        }

        public ImageSource Photo { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public AccessLevel Access { get; set; }
        public bool OnVacation { get; set; }
    }
    #endregion #Employee
}
