﻿namespace MacroFit.Models
{
    public class Study  // Singular name for entity class 
    {
        public int Id { get; set; }  // PascalCase for property names
        public int StudyTopicId { get; set; }  // StudyTopicId instead of study_topic_id as foreign key name 
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }  // DateTime type for date 
        public string Abstract { get; set; }
        public string Link { get; set; }

        // Navigation properties

        public StudyTopic StudyTopic { get; set; }  // Singular name
    }
    }
