using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateStudent : System.Web.UI.Page
{
    public string Html = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseResponse baseResponse = new BaseResponse();
        if(string.IsNullOrEmpty(Request["cardno"]) || string.IsNullOrEmpty(Request["name"])|| string.IsNullOrEmpty(Request["rand"])|| string.IsNullOrEmpty(Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        if(!StudentEdu.Core.Common.IsVailidRequest(Request["cardno"] + Request["name"] + Request["rand"], Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        //StudentService 
        StudentEdu.Service.StudentService studentService = new StudentEdu.Service.StudentService();

        if (studentService.ExistsStudent(Request["cardno"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "身份证已存在";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        studentService.CreateStudent(new StudentEdu.Model.Student { CardNo = Request["cardno"], Name = Request["name"] });

        baseResponse.IsSuccess = true;
        baseResponse.Msg = "";
        Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
        Response.Write(Html);
        Response.End();
    }
}