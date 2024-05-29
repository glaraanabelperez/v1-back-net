
namespace Utils.Exception
{
    public static class ExceptionHandler
    {
        public static ErrorResult CreateErrorResult(System.Exception exception)
        {
            ErrorResult errorResult = new ErrorResult();
            List<string> list = new List<string>();
            string key = "";
            ArgumentException ex = new ArgumentException();
            errorResult.type = ex.ToString();
            errorResult.title = "Application Error";
            if (exception.InnerException != null)
            {
                errorResult.traceId = exception.InnerException!.GetHashCode().ToString() ;
                errorResult.errors = new Dictionary<string, List<string>>
                                        {
                                            { "Message", new List<string> { exception.InnerException.Message } }
                                        };
            }
            else
            {
                errorResult.traceId = exception.GetHashCode().ToString();
                errorResult.errors = new Dictionary<string, List<string>>
                                        {
                                            { "Message", new List<string> { exception.Message } }
                                        };
            }
            string text = ((exception.InnerException != null) ? exception.InnerException!.Message : exception.Message);
            string text2 = text;
            if (text2 != null)
            {
                if (text2.Contains("Cannot insert duplicate key in object"))
                {
                    errorResult.status = "452";
                    try
                    {
                        key = text.Split('.')[0] + " " + text.Split('.')[2];
                    }
                    catch
                    {
                        key = text;
                    }

                    list.Add(text2);
                }
                else
                {
                    string text3 = text2;
                    if (text3.StartsWith("Invalid column"))
                    {
                        errorResult.status = "480";
                        key = "Invalid Column";
                        list.Add(text3);
                    }
                    else
                    {
                        string text4 = text2;
                        if (text4.Contains("is not supported in calendar "))
                        {
                            string text5 = text4.Substring(text4.IndexOf("'"), 11);
                            errorResult.status = "481";
                            key = "Formato de fecha no válido";
                            list.Add("Revisar valor: " + text5);
                        }
                    }
                }
            }

            errorResult.errors.Add(key, list);
            return errorResult;
        }
    }
}
