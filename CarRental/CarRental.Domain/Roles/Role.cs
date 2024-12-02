﻿using CarRental.CarRental.Domain.Users;

namespace CarRental.CarRental.Domain.Roles
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>();
    }

}
