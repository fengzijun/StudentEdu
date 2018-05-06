using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddOrUpdateStudentVideoInfo : System.Web.UI.Page
{
    public string Html = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseResponse baseResponse = new BaseResponse();
        if (string.IsNullOrEmpty(Request["cardno"]) || string.IsNullOrEmpty(Request["vid"]) || string.IsNullOrEmpty(Request["time"])  || string.IsNullOrEmpty(Request["rand"]) || string.IsNullOrEmpty(Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }

        if (!StudentEdu.Core.Common.IsVailidRequest(Request["cardno"] + Request["vid"] + Request["time"] + Request["rand"], Request["sign"]))
        {
            baseResponse.IsSuccess = false;
            baseResponse.Msg = "参数非法";
            Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
            Response.Write(Html);
            Response.End();
        }
        int time = 0;
        int.TryParse(Request["time"], out time);
        //StudentService 
        StudentEdu.Service.StudentService studentService = new StudentEdu.Service.StudentService();

        if (studentService.ExistsStudentVideo(Request["cardno"], Request["vid"]))
        {
            studentService.UpdateStudentVideo(new StudentEdu.Model.StudentVideo { CardNo = Request["cardno"], Time = time, Vid = Request["vid"] });
        }
        else
        {
            studentService.AddStudentVideo(new StudentEdu.Model.StudentVideo { CardNo = Request["cardno"], Time = time, Vid = Request["vid"] });
        }


        baseResponse.IsSuccess = true;
        baseResponse.Msg = "";

        Html = Newtonsoft.Json.JsonConvert.SerializeObject(baseResponse);
        Response.Write(Html);
        Response.End();
    }
}