
using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ApiUser.Exceptions
{

    public static class CustomErrors
    {
       public static StandardError NotFound(string message, Microsoft.AspNetCore.Http.HttpRequest req){
          
          return new StandardError(DateTime.Now,HttpStatusCode.NotFound,"Resource not found.",message,req.Path);
       }

       public static StandardError BadRequest(string message, Microsoft.AspNetCore.Http.HttpRequest req){
          
          return new StandardError(DateTime.Now,HttpStatusCode.BadRequest,"Bad request",message,req.Path);
       }

        internal static StandardError InternalServerError(string message, Microsoft.AspNetCore.Http.HttpRequest req)
        {
          return  new StandardError(DateTime.Now,HttpStatusCode.InternalServerError,"Internal server error",message,req.Path);
        }
    }
}