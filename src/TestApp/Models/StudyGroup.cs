using System;
using System.Collections.Generic;

namespace TestApp
{
    public class StudyGroup
    {
        public StudyGroup(int studyGroupId, string name, Subject subject, DateTime createDate, List<User> users)
        {
            if (name.Length < 5 || name.Length > 30)
                throw new ArgumentException("Name must be between 5 and 30 characters.");

            StudyGroupId = studyGroupId;
            Name = name;
            Subject = subject;
            CreateDate = createDate;
            Users = users ?? new List<User>();
        }

        public int StudyGroupId { get; }
        public string Name { get; }
        public Subject Subject { get; }
        public DateTime CreateDate { get; }
        public List<User> Users { get; private set; }

        public void AddUser(User user) => Users.Add(user);
        public void RemoveUser(User user) => Users.Remove(user);
    }

    public enum Subject
    {
        Math,
        Chemistry,
        Physics
    }
}
