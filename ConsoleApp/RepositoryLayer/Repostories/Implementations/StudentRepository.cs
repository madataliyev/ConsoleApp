using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repostories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RepositoryLayer.Repostories.Implementations
{
    public  class StudentRepository : IRepository<Student>
    {
        private readonly List<Student> _students = new List<Student>(); 

       
        public void Create(Student data)
        {
            try
            {
                if (data != null) throw new NotFoundException("Data not found!");
                AppDbContext.Students.Add(data);
            }
            catch (Exception ex)
            {

                Console.WriteLine( ex.Message);
            }
        }

        public void Delete(Student data)=> AppDbContext.Students.Remove(data);


        public Student GetById(int id) => AppDbContext.Students.FirstOrDefault(s => s.Id == id);
      

        public void Uptade(Student data)
        {
            var existData=GetById(data.Id);
            if (existData!=null)
            {
                existData.Name = data.Name;
                existData.Surname = data.Surname;
                existData.Age = data.Age;
                existData.GroupId=data.GroupId;
                
            }
        }

        public List<Student> GetStudentsByAge(int age)
        {
           var result= AppDbContext.Students.Where(s=>s.Age== age)
                                            .ToList();
            if (result.Count == 0) throw new NotFoundException($"Sorry, student with {age} age not found");
            return result;
        }
        public List<Student> GetStudentsByGroupId(int groupId)
        {
            var result = AppDbContext.Students.Where(s => s.GroupId.Id== groupId)
                                              .ToList();
            if (result.Count == 0) throw new NotFoundException($"No students have registered in the group with ID {groupId} yet");
            return result;
        }
        public List<Student> SearchMetodStudentsByNameOrSurname(string text )
        {
            var result = AppDbContext.Students.Where(s => s.Name.ToLower().Contains(text.ToLower()) ||
                                                        s.Surname.ToLower().Contains(text.ToLower()))
                                                .ToList();
            if (result.Count == 0) throw new NotFoundException($"No student found with name or surname matching '{text}'");
            return result;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
