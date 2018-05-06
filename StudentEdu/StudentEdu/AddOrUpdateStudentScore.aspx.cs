using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddOrUpdateStudentScore : System.Web.UI.Page
{
    public string Html = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseResponse baseResponse = new BaseResponse();
        if (string.IsNullOrEmpty(Request["cardno"]) || string.IsNullOrEmpty(Request["score"]) || string.IsNullOrEmpty(Request["examid"]) || string.IsNullOrEmpty(Request["rand"]) || string.IsNullOrEmpty(Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        if (!StudentEdu.Core.Common.IsVailidRequest(Request["cardno"] + Request["examid"] + Request["score"] + Request["rand"], Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        long score = 0;
        long.TryParse(Request["score"], out score);
        //StudentService 
        StudentEdu.Service.StudentService studentService = new StudentEdu.Service.StudentService();

        if (studentService.ExistsStudentScore(Request["cardno"], Request["examid"]))
        {
            studentService.UpdateStudentScore(new StudentEdu.Model.StudentScore { CardNo = Request["cardno"], Score = score, Id = Request["examid"] });
        }
        else
        {
            studentService.AddStudentScore(new StudentEdu.Model.StudentScore { CardNo = Request["cardno"], Score = score, Id = Request["examid"] });
        }

        //var model = studentService.GetStudentScore(Request["examid"], Request["cardno"]);

        baseResponse.IsSuccess = true;
        baseResponse.Msg = "";

        Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
        Response.Write(Html);
        Response.End();
    }
}