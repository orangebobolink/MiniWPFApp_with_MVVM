﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain
{
    public abstract class EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
