using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace StudyWPF.ViewModels
{
    // 性別
    public enum Gender
    {
        None,
        Men,
        Women
    }

    // DataGridに表示するデータ
    public class Person
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public bool AuthMember { get; set; }
    }

    public class DataGridViewModel
    {
        public ObservableCollection<Person> Persons { get; set; }

        public DataGridViewModel()
        {
            this.Persons = new ObservableCollection<Person>()
            {
                new Person()
                {
                    Name = "萩野 博之",
                    Gender = Gender.Men,
                    Age = 50,
                },
                new Person()
                {
                    Name = "萩野 雅之",
                    Gender = Gender.Men,
                    Age = 49,
                }
            };

        }
    }
}
