using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja
{
    public class Reservation
    {
        public User MadeBy { get; set; }

        public bool CanBeCancelledBy(User user)
        {
            if (user.isAdmin || MadeBy == user)
                return true;
            return false;
        }
    }

    public class User
    {
        public bool isAdmin { get; set; }
    }
}
