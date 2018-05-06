using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class updatestudent : System.Web.UI.Page
{
    public string Html = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseResponse baseResponse = new BaseResponse();
        if (string.IsNullOrEmpty(Request["cardno"]) || string.IsNullOrEmpty(Request["rand"]) || string.IsNullOrEmpty(Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        string name = Request["name"] ?? string.Empty;
        string company = Request["company"] ?? string.Empty;
        string mobile = Request["mobile"] ?? string.Empty;
        string level = Request["level"] ?? string.Empty;
        string nation = Request["nation"] ?? string.Empty;
        string sex = Request["sex"] ?? string.Empty;
        int age = 0;
        int.TryParse(Request["age"] ?? string.Empty, out age);

        if (!StudentEdu.Core.Common.IsVailidRequest(Request["cardno"] + Request["name"]??string.Empty + Request["company"] ?? string.Empty + Request["mobile"] ?? string.Empty + Request["level"] ?? string.Empty + Request["nation"] ?? string.Empty + Request["sex"] ?? string.Empty + Request["age"] ?? string.Empty + Request["rand"], Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        //StudentService 
        StudentEdu.Service.StudentService studentService = new StudentEdu.Service.StudentService();

        if (!studentService.ExistsStudent(Request["cardno"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "身份证不存在";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        studentService.UpdateStudent(new StudentEdu.Model.Student { CardNo = Request["cardno"], Name = name, Age = age, Company = company, Edu = level, Mobile = mobile, Nation = nation, Sex = sex });

        baseResponse.IsSuccess = true;
        baseResponse.Msg = "";
        Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
        Response.Write(Html);
        Response.End();
    }
}