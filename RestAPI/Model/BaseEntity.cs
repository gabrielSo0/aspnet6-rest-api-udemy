﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Model
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
