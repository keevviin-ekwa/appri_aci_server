using ApproACI.Models;
using System;
using System.Collections.Generic;

namespace ApproACI.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string DroitAcces { get; set; }
        public string MotDePasse { get; set; }
        public bool IsFirstConnection { get; set; }
        public string Email { get; set; }
        public string Fonction { get; set; }
        public string Tel { get; set; }
        public DateTime? DateDerniereMaj { get; set; }
        public int Status { get; set; }
    }

    public class LoginUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ChangePassWordUserDTO
    {
        public int userId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
