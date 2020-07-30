using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        List<double> classGrades = new List<double>();
        double aCutoff = 0.0;
        double bCutoff = 0.0;
        double cCutoff = 0.0;
        double dCutOff = 0.0;

        public RankedGradeBook(string name) : base(name)
        {
            this.Type = GradeBookType.Ranked;
        }

        public void FillAndStratifyGrades()
        {
            foreach (Student student in this.Students)
            {
                classGrades.Add(student.AverageGrade);
            }
            classGrades.Sort();

            double tempPercentile = 0.8;
            double tempIndex = 0.0;
            double tempCutoff = 0.0;
            
            while(tempPercentile > 0)
            {
                tempIndex = classGrades.Count() * tempPercentile;
                if(((int)tempIndex).Equals(tempIndex))
                {
                    tempCutoff = (classGrades[(int)tempIndex] + classGrades[(int)tempIndex + 1]) / 2;
                }
                else
                {
                    tempIndex += 1;
                    tempCutoff = (classGrades[(int)tempIndex]);
                }
                if(tempPercentile == 0.8)
                {
                    aCutoff = tempCutoff;
                }
                else if(tempPercentile == 0.6)
                {
                    bCutoff = tempCutoff;
                }
                else if(tempPercentile == 0.4)
                {
                    cCutoff = tempCutoff;
                }
                else if(tempPercentile == 0.2)
                {
                    dCutOff = tempCutoff;
                }
                tempPercentile -= 0.2;
            }
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (this.Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }
            if (classGrades.Count() == 0)
            {
                FillAndStratifyGrades();
            }
            
            if(averageGrade > aCutoff)
            {
                return 'A';
            }
            else if(averageGrade > bCutoff)
            {
                return 'B';
            }
            else if(averageGrade > cCutoff)
            {
                return 'C';
            }
            else if(averageGrade > dCutOff)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }

        }

    }
}
