﻿using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class CourseDto
    {
        [Required]
        public string CourseName { get; set; }

        public ChapterDto[] Chapters { get; set; }
    }
}