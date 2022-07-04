using PaymentTransaction.BusinessLogic;
using PaymentTransaction.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PaymentTransaction.Controllers
{
    public class FileUploadController : ApiController
    {
        [HttpPost]
        //[Route("api/fileupload/postformdata")]
        public HttpResponseMessage PostFormData()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            
            if (httpRequest.Files.Count > 0)
            {
                var filePath = HttpContext.Current.Server.MapPath("~/App_Data/"+AppCode.UPLOAD_FILE_PATH);
                try
                {
                    if (!Directory.Exists(filePath)) //const folder name htar pay par
                    {
                        Directory.CreateDirectory(filePath);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    AppLogger.WriteLog("Unauthorized error when creating upload file directory: " + ex.Message);
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }catch(Exception ex)
                {
                    AppLogger.WriteLog("Error creating upload file directory: " + ex.Message);
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
                    foreach (string file in httpRequest.Files)
                    {
                        try
                        {
                            var postedFile = httpRequest.Files[file];
                            if (postedFile.ContentLength > 1024 * 1024)
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Upload too large. Maximum file size is 1 MB");
                            }
                            var postedfilePath = filePath + "/" + postedFile.FileName;
                            string fileExtension = Path.GetExtension(postedFile.FileName);
                            string fileNamewithoutextension = Path.GetFileNameWithoutExtension(postedFile.FileName);
                            string serverFilePath = Path.Combine(filePath, fileNamewithoutextension +Guid .NewGuid ().ToString () + fileExtension);
                            AppLogger.WriteLog("Start Reading File: " + serverFilePath);
                            postedFile.SaveAs(serverFilePath);
                            FileUploadReader uploadReader = FileReaderHandler.GetFileReaderInstance(fileExtension);
                            string validationMessage = string.Empty;
                            PaymentTransactionHandler uploadHandler = new PaymentTransactionHandler();
                            List<PaymentTransModel> lstTrans = (List<PaymentTransModel>)uploadReader.ReadFile(serverFilePath, ref validationMessage);
                            AppLogger.WriteLog("Finish file reading. Read count: " + lstTrans.Count.ToString() + ".");
                            if (lstTrans != null && validationMessage==Messages.VALID)
                            {
                                int writeCount = 0;
                                foreach (var item in lstTrans)
                                {
                                    string returnMessage=uploadHandler.SavePaymentTransaction(item);
                                    if (returnMessage == Messages.SAVESUCCESS)
                                    {
                                        writeCount++;
                                    }
                                }
                                AppLogger.WriteLog("File Save successfully. Write count: " + writeCount.ToString() + ".");
                                result = Request.CreateResponse(HttpStatusCode.OK, "File Uploaded Successfully");
                            }
                            else
                            {
                                if(!string.IsNullOrEmpty(validationMessage))
                                {
                                    AppLogger.WriteLog("Validation error at file: " + validationMessage);
                                    result = Request.CreateResponse(HttpStatusCode.BadRequest, validationMessage);
                                }
                                else
                                {
                                    result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                                }
                            }
                            
                        }catch(UnauthorizedAccessException ex)
                        {
                        AppLogger.WriteLog("Unauthorized error when saving file: " + ex.Message);
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }
                        catch (Exception ex)
                        {
                            AppLogger.WriteLog("Error: " + ex.Message+".");
                            result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                        }

                    }
            }
            else
            {
                AppLogger.WriteLog("Invalid File.");
                result = Request.CreateResponse(HttpStatusCode.BadRequest,"Invalid File.");
            }
            return result;
        }

    }
}
