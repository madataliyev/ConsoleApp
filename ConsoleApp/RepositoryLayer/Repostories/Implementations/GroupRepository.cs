using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repostories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace RepositoryLayer.Repostories.Implementations
{
    public class GroupRepository : IRepository<CourseGroup>
    {
        private readonly List<CourseGroup> _groups = new List<CourseGroup>();
        public void Create(CourseGroup data)
        {
            try
            {
                if (data is null) throw new NotFoundException("Data not found !");
                AppDbContext.Groups.Add(data);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(CourseGroup data) =>AppDbContext.Groups.Remove(data);


        public CourseGroup GetById(int id)
        {
            var group= AppDbContext.Groups.FirstOrDefault(x => x.Id == id);
            if (group != null) throw new NotFoundException($"Sorry, group with {id} number not found");
                return group;
        }       

        public void Uptade(CourseGroup data)
        {
            var existData=GetById(data.Id);
            if (existData!=null)
            {
                existData.Name = data.Name;
                existData.Teacher = data.Teacher;
                existData.Room = data.Room;
            }
        }

        public List<CourseGroup> GetAllGroupsByTeacher(string teacher)
        {
            var result = AppDbContext.Groups
                .Where(x=> x.Teacher.ToLower().Contains(teacher.ToLower()))
                .ToList();
            if (result.Count == 0) throw new NotFoundException($"Sorry, no groups found with {teacher}");
                    return  result;
                
        }
        public List<CourseGroup> GetAllGroupsByRoom(string  room )
        {
            var result = AppDbContext.Groups
                .Where(x => x.Room.ToLower().Contains(room.ToLower()))
                .ToList();
            if (result.Count == 0) throw new NotFoundException($"There are no lessons in room {room} for now.");
            return result;
        }
        public List<CourseGroup> GetAllGroups()
        {
            if(AppDbContext.Groups.Count ==0) throw new NotFoundException("No groups found in the system"); 
            return AppDbContext.Groups;
        }
        public List<CourseGroup> SearchMethodByName(string name)
        {
            var result = AppDbContext.Groups
               .Where(x => x.Name.ToLower().Contains(name.ToLower()))
               .ToList();
            if (result.Count == 0) throw new NotFoundException($"Sorry, no groups found with {name}");
            return result;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
