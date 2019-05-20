﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GamerRater.Model
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
        public UserGroup Group { get; set; }
        public ICollection<Rating> Ratings { get; set; }

    }
}
