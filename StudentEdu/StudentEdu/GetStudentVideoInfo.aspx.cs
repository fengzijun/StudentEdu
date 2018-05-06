using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetStudentVideoInfo : System.Web.UI.Page
{
    public string Html = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetStudentVideoResponse baseResponse = new GetStudentVideoResponse();
        if (string.IsNullOrEmpty(Request["cardno"])  || string.IsNullOrEmpty(Request["rand"]) || string.IsNullOrEmpty(Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        if (!StudentEdu.Core.Common.IsVailidRequest(Request["cardno"] + Request["rand"], Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        //StudentService 
        StudentEdu.Service.StudentService studentService = new StudentEdu.Service.StudentService();

        var model = studentService.GetStudentVideo(Request["cardno"]);

        baseResponse.IsSuccess = true;
        baseResponse.Msg = "";
        baseResponse.Result = model;
        Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
        Response.Write(Html);
        Response.End();
    }
}