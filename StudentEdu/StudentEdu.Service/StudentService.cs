using StudentEdu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using SmsData;

namespace StudentEdu.Service
{
    public class StudentService: BaseService
    {
        public void CreateStudent(Student student)
        {
            string sql = @"INSERT INTO [dbo].[学员基本信息表]
           ([姓名]
           ,[性别]
           ,[年龄]
           ,[文化程度]
           ,[民族]
           ,[身份证号]
           ,[单位]
           ,[联系电话]
           ,[登陆ip])
     VALUES
           (@name
           ,@sex
           ,@age
           ,@edu
           ,@nation
           ,@cardno
           ,@company
           ,@mobile
           ,@ip)";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("name",student.Name),
                new SqlParameter("sex",student.Sex),
                new SqlParameter("age",student.Age),
                new SqlParameter("edu",student.Edu),
                new SqlParameter("nation",student.Nation),
                new SqlParameter("cardno",student.CardNo),
                new SqlParameter("company",student.Company),
                new SqlParameter("mobile",student.Mobile),
                new SqlParameter("ip",student.Ip),
            };
          
            SqlHelper.ExecuteNonQuery(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters);
        }

        public bool ExistsStudent(string cardno)
        {
            string sql = @"select count(1) from [dbo].[学员基本信息表] where [身份证号] = @cardno";

            SqlParameter[] sqlParameters = new SqlParameter[] {
           
                new SqlParameter("cardno",cardno),
            
            };

            object result = SqlHelper.ExecuteScalar(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters);
            if (Convert.ToInt32(result) > 0)
                return true;
            return false;
        }


        public void UpdateStudent(Student student)
        {
            string sql = @"UPDATE [dbo].[学员基本信息表]
   SET [姓名] = @name
      ,[性别] = @sex
      ,[年龄] = @age
      ,[文化程度] = @edu
      ,[民族] = @nation
      ,[单位] = @company
      ,[联系电话] = @mobile
      ,[登陆ip] = @ip
 WHERE [身份证号] = @cardno";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("name",student.Name),
                new SqlParameter("sex",student.Sex),
                new SqlParameter("age",student.Age),
                new SqlParameter("edu",student.Edu),
                new SqlParameter("nation",student.Nation),
                new SqlParameter("cardno",student.CardNo),
                new SqlParameter("company",student.Company),
                new SqlParameter("mobile",student.Mobile),
                new SqlParameter("ip",student.Ip),
            };
       
            SqlHelper.ExecuteNonQuery(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters);
        }


        public StudentScore GetStudentScore(string examid,string cardno)
        {
            string sql = @"SELECT [编码]
      ,[身份证号]
      ,[三级目录编码]
      ,[取得分数]
      ,[取得时间]
      ,[题目时间]
  FROM [dbo].[学习成绩]
   WHERE [身份证号] = @cardno and [编码] = @examid";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("examid",ToGuid(examid)),
                new SqlParameter("cardno",cardno),
            
            };


            using (SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters))
            {
                if (sqlDataReader.Read())
                {
                    return new StudentScore { CardNo = cardno, Id = ToGuid(examid), Score = sqlDataReader["取得分数"] == null ? 0 : long.Parse(sqlDataReader["取得分数"].ToString()), Createtime = sqlDataReader["取得时间"] == null ? DateTime.MinValue : DateTime.Parse(sqlDataReader["取得时间"].ToString()) };
                }
            }

            return null;

        }

        public void AddStudentScore(StudentScore studentScore)
        {
            string sql = @"INSERT INTO [dbo].[学习成绩]
           ([编码]
           ,[身份证号]
           ,[三级目录编码]
           ,[取得分数]
           ,[取得时间]
           ,[题目时间])
     VALUES
           (@examid
           ,@cardno
           ,''
           ,@score
           ,getdate()
           ,getdate())";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("examid",ToGuid(studentScore.Id)),
                new SqlParameter("cardno",studentScore.CardNo),
                new SqlParameter("score",studentScore.Score),
            };


            SqlHelper.ExecuteNonQuery(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters);

        }


        public bool ExistsStudentScore(string cardno, string examid)
        {
            string sql = @"select count(1) FROM [dbo].[学习成绩] WHERE [身份证号] = @cardno and [编码] = @examid";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("examid",ToGuid(examid)),
                new SqlParameter("cardno",cardno),

            };

            object result = SqlHelper.ExecuteScalar(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters);
            if (Convert.ToInt32(result) > 0)
                return true;
            return false;
        }


        public void UpdateStudentScore(StudentScore studentScore)
        {
            string sql = @"UPDATE [dbo].[学习成绩]
   SET [取得分数] = @score
    
     WHERE [身份证号] = @cardno and [编码] = @examid";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("examid",ToGuid(studentScore.Id)),
                new SqlParameter("cardno",studentScore.CardNo),
                new SqlParameter("score",studentScore.Score),
            };


            SqlHelper.ExecuteNonQuery(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters);
        }


        public StudentVideo GetStudentVideo( string cardno)
        {
            string sql = @"SELECT [信息编号]
      ,[身份证号]
      ,[合计时间]
  FROM [hongshizihui].[dbo].[学习时间合计表]
   WHERE [身份证号] = @cardno";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("cardno",cardno),

            };


            using (SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters))
            {
                if (sqlDataReader.Read())
                {
                    return new StudentVideo { CardNo = cardno, Vid = sqlDataReader["信息编号"] == null ? string.Empty : sqlDataReader["信息编号"].ToString(),  Time = sqlDataReader["合计时间"] == null ? 0 : int.Parse(sqlDataReader["合计时间"].ToString()) };
                }
            }

            return null;

        }

        public void AddStudentVideo(StudentVideo studentScore)
        {
            string sql = @"INSERT INTO [dbo].[学习时间合计表]
           ([信息编号]
           ,[身份证号]
           ,[合计时间])
     VALUES
           (@id
           ,@cardno
           ,@time)";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("id",studentScore.Vid),
                new SqlParameter("cardno",studentScore.CardNo),
                new SqlParameter("time",studentScore.Time),
            };


            SqlHelper.ExecuteNonQuery(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters);

        }


        public bool ExistsStudentVideo(string id , string cardno)
        {
            string sql = @"SELECT count(1)
  FROM [hongshizihui].[dbo].[学习时间合计表]
   WHERE [身份证号] = @cardno and [信息编号] = @id";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("id",id),
                new SqlParameter("cardno",cardno),

            };

            object result = SqlHelper.ExecuteScalar(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters);
            if (Convert.ToInt32(result) > 0)
                return true;
            return false;
        }


        public void UpdateStudentVideo(StudentVideo studentScore)
        {
            string sql = @"UPDATE [hongshizihui].[dbo].[学习时间合计表]
   SET [合计时间] = @time
    
     WHERE [身份证号] = @cardno and [信息编号] = @id";

            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("id",studentScore.Vid),
                new SqlParameter("cardno",studentScore.CardNo),
                 new SqlParameter("time",studentScore.Time),
            };


            SqlHelper.ExecuteNonQuery(Connectionstring, System.Data.CommandType.Text, sql, sqlParameters);
        }
    }
}
