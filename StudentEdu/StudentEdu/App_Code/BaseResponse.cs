using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// BaseResponse 的摘要说明
/// </summary>
public class BaseResponse
{
    public BaseResponse()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public bool IsSuccess { get; set; }

    public string Msg { get; set; }
}


public class BaseResponse<T> : BaseResponse
{
    public T Result { get; set; }
}